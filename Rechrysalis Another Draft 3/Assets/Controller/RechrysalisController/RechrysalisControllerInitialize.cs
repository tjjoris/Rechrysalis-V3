using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Controller
{
    public class RechrysalisControllerInitialize : MonoBehaviour
    {
        [SerializeField] private GameObject _parentUnitPrefab;
        [SerializeField] private GameObject _childUnitPrefab;
        [SerializeField] private GameObject _chrysalisPrefab;
        [SerializeField] private GameObject _unitRing;
        [SerializeField] private float _ringDistFromCentre = 2f;
        [SerializeField] private GameObject[] _parentUnits;
        private ControllerFreeUnitHatchEffectManager _controllerHatchEffect;
        public GameObject[] ParentUnits {get{return _parentUnits;}}
        private List<GameObject> _allUnits;    
        private PlayerUnitsSO _theseUnits;   
        private CompSO _unitComp; 
        private float _unitRingOutRadius;
        private float _unitRingAngle = 90f;
        public void Initialize(int _controllerIndex, CompSO _unitComp, CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRingManager, HilightRingManager _hilightRingManager, UpgradeRingManager _upgradeRingManager, float _unitRingOuterRadius)
        {
            this._unitComp = _unitComp;
            _allUnits = new List<GameObject>();
            _allUnits.Clear();                    
            _parentUnits = new GameObject[_unitComp.ParentUnitCount];
            _theseUnits = _compsAndUnits.PlayerUnits[_controllerIndex];
            _theseUnits.ActiveUnits = new List<GameObject>();
            _theseUnits.ActiveUnits.Clear();
            _theseUnits.ParentUnits = new List<GameObject>();
            _theseUnits.ParentUnits.Clear();
            _controllerHatchEffect = GetComponent<ControllerFreeUnitHatchEffectManager>();
            _controllerHatchEffect.InitializeUnitsArray(18);
            ParentUnitHatchEffects[] _parentUnitHatchEffects = new ParentUnitHatchEffects[_unitComp.ParentUnitCount];
            // foreach (GameObject _unit in _parentUnits)
            for (int _parentUnitIndex = 0; _parentUnitIndex < _unitComp.ParentUnitCount; _parentUnitIndex++)
            {       
                // if (CheckIfParentUnitShouldExist(_unitComp, _parentUnitIndex))
                if (_unitComp.DoesParentExist(_parentUnitIndex))
                {
                    float _radToOffset = Mathf.Deg2Rad * (((360f / _unitComp.ParentUnitCount) * _parentUnitIndex) + _unitRingAngle);  
                    Vector3 _unitOffset = new Vector3 (Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, 0f);
                    // Debug.Log($"radtooffset" + _radToOffset + "vector 3 " + _unitOffset);
                    GameObject parentUnitGO = Instantiate(_parentUnitPrefab, _unitRing.transform);
                    _theseUnits.ParentUnits.Add(parentUnitGO);
                    parentUnitGO.transform.localPosition = _unitOffset;
                    _parentUnits[_parentUnitIndex] = parentUnitGO;
                    parentUnitGO.name = "Parent Unit " + _parentUnitIndex.ToString();
                    ParentUnitManager _pum = parentUnitGO.GetComponent<ParentUnitManager>();
                    HatchEffectSO[] _hatchEffectSOs = SetHatchEffectSOs(_parentUnitIndex);
                    _pum?.Initialize(_controllerIndex, _parentUnitIndex, _unitComp, _compsAndUnits.PlayerUnits[_controllerIndex], transform, _hatchEffectSOs);                        
                    _pum.SubUnits = new GameObject[_unitComp.UpgradeCountArray[_parentUnitIndex]];
                    _pum.SubChrysalii = new GameObject[_unitComp.UpgradeCountArray[_parentUnitIndex]];
                    _parentUnitHatchEffects[_parentUnitIndex] = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                    for (int _childUnitIndex = 0; _childUnitIndex < _unitComp.UpgradeCountArray[_parentUnitIndex]; _childUnitIndex++)
                    {
                        if ((_childUnitIndex == 0) || (CheckIfChildUnitShouldExist(_unitComp, _parentUnitIndex, _childUnitIndex)))
                        {
                            GameObject childUnitGo = Instantiate(_childUnitPrefab, parentUnitGO.transform);
                            UnitStatsSO _unitStats = _unitComp.UnitSOArray[(_parentUnitIndex * 3) + (_childUnitIndex)];
                            // _unitStats.Initialize();
                            UnitManager _childUnitManager = childUnitGo.GetComponent<UnitManager>();                            
                            childUnitGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _parentUnitIndex, _hatchEffectSOs[_childUnitIndex]);
                            _childUnitManager.SetUnitName(_unitStats.UnitName);
                            _pum.SubUnits[_childUnitIndex] = childUnitGo;
                            childUnitGo.name = $"Child Unit " + _childUnitIndex;
                            _allUnits.Add(childUnitGo);
                            _controllerHatchEffect.SetUnitsArray(childUnitGo, ((_parentUnitIndex * 6) + (_childUnitIndex * 2)));
                            // _theseUnits.ActiveUnits.Add(childUnitGo);
                            childUnitGo.SetActive(false);
                            GameObject chrysalisGo = Instantiate(_chrysalisPrefab, parentUnitGO.transform);
                            chrysalisGo.name = $"Chrysalis " + _childUnitIndex;
                            // chrysalisGo.GetComponent<ChrysalisManager>()?.Initialize(_unitStats.ChrysalisTimerMax, childUnitGo);
                            UnitManager _chrysalisManager = chrysalisGo.GetComponent<UnitManager>();
                            chrysalisGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _compsAndUnits.Chrysalis, _compsAndUnits, _parentUnitIndex, null);
                            _chrysalisManager.SetUnitName(_unitStats.UnitName);
                            chrysalisGo.GetComponent<ChrysalisTimer>()?.Initialize(_unitStats.ChrysalisTimerMax, _childUnitIndex);
                            _pum.SubChrysalii[_childUnitIndex] = chrysalisGo;                    
                            _allUnits.Add(chrysalisGo);
                            _controllerHatchEffect.SetUnitsArray(chrysalisGo, ((_parentUnitIndex * 6) + (_childUnitIndex * 2) +1));                 
                            chrysalisGo.SetActive(false);  
                        }
                    }
                    ParentUnitHatchEffects _pUHE = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                    _pUHE?.Initialize(_pum.SubUnits, _pum.SubChrysalii);
                    _pum.AddChrysalisAndUnitActions();  
                }              
            }
            _hilightRingManager?.Initialize(_unitRingManager);
            _unitRingManager?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ParentUnitCount, _parentUnits, _unitRingAngle, _hilightRingManager.transform);  
            _upgradeRingManager?.Initialize(_unitRingAngle, _unitComp, _ringDistFromCentre, _parentUnits);
            FreeUnitHatchEffect[] _freeHatches = new FreeUnitHatchEffect[_allUnits.Count];
            for (int _unitCount = 0; _unitCount < _allUnits.Count; _unitCount ++)
            {
                _freeHatches[_unitCount] = _allUnits[_unitCount].GetComponent<FreeUnitHatchEffect>();
            }
            _controllerHatchEffect?.SetFreeHatches(_freeHatches);
            _controllerHatchEffect?.SetParentUnitHatchEffects(_parentUnitHatchEffects);
            _controllerHatchEffect?.SubscribeToUnits();
            _upgradeRingManager?.SetActiveUpgradeRing(-1);
        }
        private HatchEffectSO[] SetHatchEffectSOs (int _parentUnitIndex)
        {
            HatchEffectSO[] _hatchEffectSOs = new HatchEffectSO[_unitComp.ChildUnitCount];
            for (int _childIndex = 0; _childIndex < _unitComp.ChildUnitCount; _childIndex ++)
            {
                _hatchEffectSOs[_childIndex] = _unitComp.HatchEffectSOArray[(_parentUnitIndex * _unitComp.ChildUnitCount) + _childIndex];
                // if (_hatchEffectSOs[_childIndex] != null)
                // Debug.Log($"hatch effect SOs " + _hatchEffectSOs[_childIndex].HatchEffectName);
            }
            return _hatchEffectSOs;
        }
        private bool CheckIfParentUnitShouldExist(CompSO _comp, int _parentIndex)
        {
            for (int _childIndex = 0; _childIndex < _comp.ChildUnitCount; _childIndex ++)
            {
                if (CheckIfChildUnitShouldExist(_comp, _parentIndex, _childIndex))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckIfChildUnitShouldExist(CompSO _comp, int _parentIndex, int _childIndex)
        {
            int _index = (_parentIndex * _comp.ParentUnitCount) + _childIndex;
            if (_comp.UnitSOArray[_index] == null)
            {
                return false;
            }
            if (_comp.UnitSOArray[(_parentIndex * _comp.ParentUnitCount) + _childIndex].UnitName != "Empty")
            {
                return true;
            }
            if ((_childIndex > 0) && (_comp.HatchEffectSOArray[(_parentIndex * 3) + _childIndex] != null))
            {
                return true;
            }
            return false;
        }
        public void ActivateInitialUnits()
        {
            for (int _parentUnitIndex = 0; _parentUnitIndex < _unitComp.ParentUnitCount; _parentUnitIndex++)
            {
                if (_parentUnits[_parentUnitIndex] != null){
                //     ParentUnitManager _parentUnitManager = _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>();
                // _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>()?.ActivateUnit(0);
                // _parentUnits[_parentUnitIndex].GetComponent<ParentHealth>()?.SetMaxHealth(_parentUnitManager.SubUnits[0].GetComponent<UnitManager>().UnitStats.HealthMax);
                _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>().ActivateInitialUnit();
                }
            }
        }
        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
    }
}
