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
            _controllerHatchEffect = GetComponent<ControllerFreeUnitHatchEffectManager>();
            _controllerHatchEffect.InitializeUnitsArray(18);
            ParentUnitHatchEffects[] _parentUnitHatchEffects = new ParentUnitHatchEffects[_unitComp.ParentUnitCount];
            // foreach (GameObject _unit in _parentUnits)
            for (int _parentUnitIndex = 0; _parentUnitIndex < _unitComp.ParentUnitCount; _parentUnitIndex++)
            {       
                float _radToOffset = Mathf.Deg2Rad * (((360f / _unitComp.ParentUnitCount) * _parentUnitIndex) + _unitRingAngle);  
                Vector3 _unitOffset = new Vector3 (Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, 0f);
                // Debug.Log($"radtooffset" + _radToOffset + "vector 3 " + _unitOffset);
                GameObject parentUnitGO = Instantiate(_parentUnitPrefab, _unitRing.transform);
                parentUnitGO.transform.localPosition = _unitOffset;
                _parentUnits[_parentUnitIndex] = parentUnitGO;
                parentUnitGO.name = "Parent Unit " + _parentUnitIndex.ToString();
                ParentUnitManager _pum = parentUnitGO.GetComponent<ParentUnitManager>();
                _pum?.Initialize(_controllerIndex, _parentUnitIndex, _unitComp, _compsAndUnits.PlayerUnits[_controllerIndex], transform);                        
                _pum.SubUnits = new GameObject[_unitComp.UpgradeCountArray[_parentUnitIndex]];
                _pum.SubChrysalii = new GameObject[_unitComp.UpgradeCountArray[_parentUnitIndex]];
                _parentUnitHatchEffects[_parentUnitIndex] = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                for (int _childUnitIndex = 0; _childUnitIndex < _unitComp.UpgradeCountArray[_parentUnitIndex]; _childUnitIndex++)
                {
                    GameObject childUnitGo = Instantiate(_childUnitPrefab, parentUnitGO.transform);
                    UnitStatsSO _unitStats = _unitComp.UnitSOArray[(_parentUnitIndex * 3) + (_childUnitIndex)];
                    // _unitStats.Initialize();
                    childUnitGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _parentUnitIndex);
                    _pum.SubUnits[_childUnitIndex] = childUnitGo;
                    childUnitGo.name = $"Child Unit " + _childUnitIndex;
                    _allUnits.Add(childUnitGo);
                    _controllerHatchEffect.SetUnitsArray(childUnitGo, ((_parentUnitIndex * 6) + (_childUnitIndex * 2)));
                    // _theseUnits.ActiveUnits.Add(childUnitGo);
                    childUnitGo.SetActive(false);
                    GameObject chrysalisGo = Instantiate(_chrysalisPrefab, parentUnitGO.transform);
                    chrysalisGo.name = $"Chrysalis " + _childUnitIndex;
                    // chrysalisGo.GetComponent<ChrysalisManager>()?.Initialize(_unitStats.ChrysalisTimerMax, childUnitGo);
                    chrysalisGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _compsAndUnits.Chrysalis, _compsAndUnits, _parentUnitIndex);
                    chrysalisGo.GetComponent<ChrysalisTimer>()?.Initialize(_unitStats.ChrysalisTimerMax, _childUnitIndex);
                    _pum.SubChrysalii[_childUnitIndex] = chrysalisGo;                    
                    _allUnits.Add(chrysalisGo);
                    _controllerHatchEffect.SetUnitsArray(chrysalisGo, ((_parentUnitIndex * 6) + (_childUnitIndex * 2) +1));                 
                    chrysalisGo.SetActive(false);  
                }
                ParentUnitHatchEffects _pUHE = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                _pUHE?.Initialize(_pum.SubUnits, _pum.SubChrysalii);
                _pum.AddChrysalisAndUnitActions();                
            }
            _hilightRingManager?.Initialize(_unitRingManager);
            _unitRingManager?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ParentUnitCount, _parentUnits, _unitRingAngle, _hilightRingManager.transform);  
            _upgradeRingManager?.Initialize(_unitRingAngle, _unitComp, _ringDistFromCentre);
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
        public void ActivateInitialUnits()
        {
            for (int _parentUnitIndex = 0; _parentUnitIndex < _unitComp.ParentUnitCount; _parentUnitIndex++)
            {
                _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>()?.ActivateUnit(0);
            }
        }
        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
    }
}
