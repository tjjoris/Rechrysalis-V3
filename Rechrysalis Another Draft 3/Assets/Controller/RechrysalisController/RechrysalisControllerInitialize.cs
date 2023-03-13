using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Controller
{
    public class RechrysalisControllerInitialize : MonoBehaviour
    {
        private bool _debugBool;
        private ControllerManager _controllerManager;
        private HilightRingManager _hilightRingManager;
        private HilightRingParentCreator _hilightRingParentCreator;
        private HilightRingParentManager _hilightRingParentManager;
        private ManaGenerator _manaGenerator;
        [SerializeField] private int _controllerIndex;
        public int ControllerIndex { get => _controllerIndex; set => _controllerIndex = value; }
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
        public void Initialize(int controllerIndex, CompSO unitComp, CompsAndUnitsSO compsAndUnits, UnitRingManager unitRingManager, HilightRingManager hilightRingManager, UpgradeRingManager upgradeRingManager, float unitRingOuterRadius)
        {
           _controllerManager = GetComponent<ControllerManager>(); 
           _hilightRingManager = _controllerManager.HilightRingManager;
           _hilightRingParentCreator = _controllerManager.HilightRingManager.GetComponent<HilightRingParentCreator>();
            hilightRingManager?.Initialize(unitRingManager);
           _manaGenerator = GetComponent<ManaGenerator>();
            _controllerIndex = controllerIndex;
            this._unitComp = unitComp;
            _allUnits = new List<GameObject>();
            _allUnits.Clear();                    
            _parentUnits = new GameObject[unitComp.ParentUnitCount];
            _theseUnits = compsAndUnits.PlayerUnits[controllerIndex];
            _theseUnits.ActiveUnits = new List<GameObject>();
            _theseUnits.ActiveUnits.Clear();
            _theseUnits.ParentUnits = new List<GameObject>();
            _theseUnits.ParentUnits.Clear();
            _controllerHatchEffect = GetComponent<ControllerFreeUnitHatchEffectManager>();
            _controllerHatchEffect.InitializeUnitsArray(18);
            ParentUnitHatchEffects[] _parentUnitHatchEffects = new ParentUnitHatchEffects[unitComp.ParentUnitCount];
            
            // foreach (GameObject _unit in _parentUnits)
            for (int parentUnitIndex = 0; parentUnitIndex < unitComp.ParentUnitCount; parentUnitIndex++)
            {       
                // if (CheckIfParentUnitShouldExist(_unitComp, _parentUnitIndex))

                if (unitComp.DoesParentExist(parentUnitIndex))
                {
                    if (_debugBool)
                    {
                        Debug.Log($"parent exists " + parentUnitIndex);
                    }
                    Vector2 unitOffset = AnglesMath.GetOffsetPosForParentInRing(parentUnitIndex, unitComp.ParentUnitCount, _unitRingAngle, _ringDistFromCentre);
                    // float _radToOffset = Mathf.Deg2Rad * (((360f / unitComp.ParentUnitCount) * parentUnitIndex) + _unitRingAngle);  
                    // Vector3 _unitOffset = new Vector3 (Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, 0f);
                    // Debug.Log($"radtooffset" + _radToOffset + "vector 3 " + _unitOffset);
                    Vector3 goPosition = unitOffset;
                    goPosition += _unitRing.transform.position;
                    GameObject parentUnitGO = Instantiate(_parentUnitPrefab, goPosition, Quaternion.identity, _unitRing.transform);
                    _theseUnits.ParentUnits.Add(parentUnitGO);
                    // parentUnitGO.transform.localPosition = unitOffset;
                    _parentUnits[parentUnitIndex] = parentUnitGO;
                    parentUnitGO.name = "Parent Unit " + parentUnitIndex.ToString();
                    ParentUnitManager pum = parentUnitGO.GetComponent<ParentUnitManager>();
                    _controllerManager.ParentUnitManagers.Add(pum);
                    HatchEffectSO[] _hatchEffectSOs = SetHatchEffectSOs(parentUnitIndex);
                    pum?.Initialize(controllerIndex, parentUnitIndex, unitComp, compsAndUnits.PlayerUnits[controllerIndex], transform, _hatchEffectSOs, unitComp.ParentUnitClassList[parentUnitIndex]);
                    _hilightRingParentCreator?.CreateHilightRingParent(parentUnitIndex, unitComp.ParentUnitCount, unitOffset);
                    pum.HilightRingParentManager = _hilightRingParentCreator?.GetLastCreatedHilightRingParentManager();
                    pum.HilightRingParentManager.GetComponent<RotateParentUnit>()?.Initialize(_controllerManager.transform);
                    pum.UnitActivation.HilightRingParentManager = pum.HilightRingParentManager;
                    pum.ChrysalilsActivation.HilightRingParentManager = pum.HilightRingParentManager;
                    // pum?.SetManaText(unitComp.ParentUnitClassList[parentUnitIndex].AdvUnitClass.ManaCost.ToString());
                    UnitManaCostText manaText = parentUnitGO.GetComponent<UnitManaCostText>();
                    manaText?.SetManaText(unitComp.ParentUnitClassList[parentUnitIndex].AdvUnitClass.ManaCost.ToString());
                    pum.SubUnits = new GameObject[unitComp.UpgradeCountArray[parentUnitIndex]];
                    pum.SubChrysalii = new GameObject[unitComp.UpgradeCountArray[parentUnitIndex]];
                    _parentUnitHatchEffects[parentUnitIndex] = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                    // for (int _childUnitIndex = 0; _childUnitIndex < unitComp.UpgradeCountArray[parentUnitIndex]; _childUnitIndex++)
                    {
                        // if ((_childUnitIndex == 0) || (CheckIfChildUnitShouldExist(unitComp, parentUnitIndex, _childUnitIndex)))
                        {
                            int _childUnitIndex = 0;
                            UnitClass unitClass = unitComp.ParentUnitClassList[parentUnitIndex].BasicUnitClass;
                            CreateChildUnitAndChrysalis(unitClass, _childUnitIndex, pum, parentUnitIndex, compsAndUnits, false);
                            _childUnitIndex = 1;
                            unitClass = unitComp.ParentUnitClassList[parentUnitIndex].AdvUnitClass;
                            CreateChildUnitAndChrysalis(unitClass, _childUnitIndex, pum, parentUnitIndex, compsAndUnits, true);
                            // GameObject childUnitGo = Instantiate(_childUnitPrefab, parentUnitGO.transform);
                            // UnitStatsSO _unitStats = _unitComp.UnitSOArray[(_parentUnitIndex * 3) + (_childUnitIndex)];
                            // // _unitStats.Initialize();
                            // UnitManager _childUnitManager = childUnitGo.GetComponent<UnitManager>();                            
                            // // childUnitGo.GetComponent<UnitManager>()?.InitializeOld(_controllerIndex, _unitStats, _compsAndUnits, _parentUnitIndex, _hatchEffectSOs[_childUnitIndex]);
                            // UnitClass unitClass = _unitComp.ParentUnitClassList[_parentUnitIndex].BasicUnitClass;
                            // childUnitGo.GetComponent<UnitManager>()?.Initialize(controllerIndex, unitClass, _parentUnitIndex);
                            // _childUnitManager.SetUnitName(unitClass.UnitName);
                            // _pum.SubUnits[_childUnitIndex] = childUnitGo;
                            // childUnitGo.name = $"Child Unit " + _childUnitIndex;
                            // _allUnits.Add(childUnitGo);
                            // _controllerHatchEffect.SetUnitsArray(childUnitGo, ((_parentUnitIndex * 6) + (_childUnitIndex * 2)));
                            // // _theseUnits.ActiveUnits.Add(childUnitGo);
                            // childUnitGo.SetActive(false);
                            // GameObject chrysalisGo = Instantiate(_chrysalisPrefab, parentUnitGO.transform);
                            // chrysalisGo.name = $"Chrysalis " + _childUnitIndex;
                            // // chrysalisGo.GetComponent<ChrysalisManager>()?.Initialize(_unitStats.ChrysalisTimerMax, childUnitGo);
                            // UnitManager _chrysalisManager = chrysalisGo.GetComponent<UnitManager>();
                            // chrysalisGo.GetComponent<UnitManager>()?.InitializeOld(controllerIndex, _compsAndUnits.Chrysalis, _compsAndUnits, _parentUnitIndex, null);
                            
                            // _chrysalisManager.SetUnitName(_unitStats.UnitName);
                            // chrysalisGo.GetComponent<ChrysalisTimer>()?.Initialize(_unitStats.ChrysalisTimerMax, _childUnitIndex);
                            // _pum.SubChrysalii[_childUnitIndex] = chrysalisGo;                    
                            // _allUnits.Add(chrysalisGo);
                            // _controllerHatchEffect.SetUnitsArray(chrysalisGo, ((_parentUnitIndex * 6) + (_childUnitIndex * 2) +1));                 
                            // chrysalisGo.SetActive(false);  
                        }
                    }
                    ParentUnitHatchEffects _pUHE = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                    _pUHE?.Initialize(pum.SubUnits, pum.SubChrysalii);
                    pum.AddChrysalisAndUnitActions();   
                }              
            }

            unitRingManager?.Initialize(compsAndUnits.CompsSO[controllerIndex].ParentUnitCount, _parentUnits, _unitRingAngle, hilightRingManager.transform);  
            upgradeRingManager?.Initialize(_unitRingAngle, unitComp, _ringDistFromCentre, _parentUnits, transform);
            FreeUnitHatchEffect[] _freeHatches = new FreeUnitHatchEffect[_allUnits.Count];
            for (int _unitCount = 0; _unitCount < _allUnits.Count; _unitCount ++)
            {
                _freeHatches[_unitCount] = _allUnits[_unitCount].GetComponent<FreeUnitHatchEffect>();
            }
            _controllerHatchEffect?.SetFreeHatches(_freeHatches);
            _controllerHatchEffect?.SetParentUnitHatchEffects(_parentUnitHatchEffects);
            _controllerHatchEffect?.SubscribeToUnits();
            upgradeRingManager?.SetActiveUpgradeRing(-1);
            AddToStartingMana(unitComp);
        }
        private void CreateChildUnitAndChrysalis(UnitClass unitClass, int childUnitIndex, ParentUnitManager pum, int parentUnitIndex, CompsAndUnitsSO compsAndUnits, bool isAdvUnit)
        {
            GameObject childUnitGo = Instantiate(_childUnitPrefab, pum.transform);
            UnitStatsSO _unitStats = _unitComp.UnitSOArray[(parentUnitIndex * 3) + (childUnitIndex)];
            // _unitStats.Initialize();
            UnitManager _childUnitManager = childUnitGo.GetComponent<UnitManager>();
            pum.ChildUnitManagers.Add(_childUnitManager);
            // childUnitGo.GetComponent<UnitManager>()?.InitializeOld(_controllerIndex, _unitStats, _compsAndUnits, _parentUnitIndex, _hatchEffectSOs[_childUnitIndex]);
            // UnitClass unitClass = _unitComp.ParentUnitClassList[parentUnitIndex].BasicUnitClass;
            childUnitGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, unitClass, parentUnitIndex, compsAndUnits);
            childUnitGo.GetComponent<UnitManager>()?.SetUnitSPrite(unitClass.UnitSprite);
            _childUnitManager.SetUnitName(unitClass.UnitName);
            pum.SubUnits[childUnitIndex] = childUnitGo;
            childUnitGo.name = $"Child Unit " + childUnitIndex;
            _allUnits.Add(childUnitGo);
            _controllerHatchEffect.SetUnitsArray(childUnitGo, ((parentUnitIndex * 6) + (childUnitIndex * 2)));
            // _theseUnits.ActiveUnits.Add(childUnitGo);
            childUnitGo.SetActive(false);
            pum.HilightRingParentManager.CreateHilightRingUnit(unitClass.UnitSprite);
            GameObject chrysalisGo = Instantiate(_chrysalisPrefab, pum.transform);
            chrysalisGo.name = $"Chrysalis " + childUnitIndex;
            // chrysalisGo.GetComponent<ChrysalisManager>()?.Initialize(_unitStats.ChrysalisTimerMax, childUnitGo);
            UnitManager _chrysalisManager = chrysalisGo.GetComponent<UnitManager>();
            pum.ChildChrysaliiUnitManagers.Add(_chrysalisManager);
            // chrysalisGo.GetComponent<UnitManager>()?.InitializeOld(_controllerIndex, compsAndUnits.Chrysalis, compsAndUnits, parentUnitIndex, null);
            UnitManager chrysalisUnitManager = chrysalisGo.GetComponent<UnitManager>();
            chrysalisGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, unitClass, parentUnitIndex, compsAndUnits);
            chrysalisGo.GetComponent<UnitManager>()?.SetUnitSPrite(unitClass.ChrysalisSprite);            
            // _chrysalisManager.SetUnitName(_unitStats.UnitName);
            chrysalisGo.GetComponent<ChrysalisTimer>()?.Initialize(unitClass.BuildTime, childUnitIndex, pum.GetComponent<ProgressBarManager>());
            pum.SubChrysalii[childUnitIndex] = chrysalisGo;
            _allUnits.Add(chrysalisGo);
            _controllerHatchEffect.SetUnitsArray(chrysalisGo, ((parentUnitIndex * 6) + (childUnitIndex * 2) + 1));
            chrysalisGo.SetActive(false);
            pum.HilightRingParentManager.CreateHilightRingChrysalis(unitClass.ChrysalisSprite);
            if (isAdvUnit)
            {
                chrysalisUnitManager?.ControllerUnitSpriteHandler?.TintSpriteMagenta();
            }
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
                if (_parentUnits[_parentUnitIndex] != null)
                {
                    ParentUnitManager thisParentUnit = _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>();
                    if (thisParentUnit!= null){
                        //     ParentUnitManager _parentUnitManager = _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>();
                        // _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>()?.ActivateUnit(0);
                        // _parentUnits[_parentUnitIndex].GetComponent<ParentHealth>()?.SetMaxHealth(_parentUnitManager.SubUnits[0].GetComponent<UnitManager>().UnitStats.HealthMax);
                        
                        // _parentUnits[_parentUnitIndex].GetComponent<ParentUnitManager>().ActivateInitialUnit();
                        thisParentUnit.GetComponent<ParentHealth>().SetMaxHealth(thisParentUnit.SubUnits[0].GetComponent<UnitManager>().UnitClass.HPMax);
                        thisParentUnit.GetComponent<UnitActivation>().ActivateUnit(0);
                    }
                }
            }
        }
        private void AddToStartingMana(CompSO compSO)
        {
            foreach (ParentUnitClass parentUnitClass in compSO.ParentUnitClassList)
            {
                if (parentUnitClass != null)
                {
                    if (parentUnitClass.AdvancedUpgradesUTCList.Count > 0)
                    {                    
                        _manaGenerator.AddToStartingMana(parentUnitClass.AdvancedUpgradesUTCList.Count);

                    }
                    if (parentUnitClass.UTCHatchEffect != null)
                    {
                        _manaGenerator.AddToStartingMana(1);
                    }
                }
            }
        }
        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
    }
}
