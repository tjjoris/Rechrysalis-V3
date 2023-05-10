using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using Rechrysalis.UI;

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

        private void Awake()
        {

            _controllerManager = GetComponent<ControllerManager>();
            _controllerHatchEffect = GetComponent<ControllerFreeUnitHatchEffectManager>();
        }
        public void Initialize(int controllerIndex, CompSO unitComp, CompsAndUnitsSO compsAndUnits, UnitRingManager unitRingManager, HilightRingManager hilightRingManager, UpgradeRingManager upgradeRingManager, float unitRingOuterRadius, MainManager mainManager)
        {
           _hilightRingManager = _controllerManager.HilightRingManager;
           _hilightRingParentCreator = _controllerManager.HilightRingManager.GetComponent<HilightRingParentCreator>();
            hilightRingManager?.Initialize(unitRingManager);           
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
            _controllerHatchEffect.InitializeUnitsArray(18);
            ParentUnitHatchEffects[] _parentUnitHatchEffects = new ParentUnitHatchEffects[unitComp.ParentUnitCount];
            
            for (int parentUnitIndex = 0; parentUnitIndex < unitComp.ParentUnitCount; parentUnitIndex++)
            {       
                if (unitComp.DoesParentExist(parentUnitIndex))
                {
                    if (_debugBool)
                    {
                        Debug.Log($"parent exists " + parentUnitIndex);
                    }
                    Vector2 unitOffset = AnglesMath.GetOffsetPosForParentInRing(parentUnitIndex, unitComp.ParentUnitCount, _unitRingAngle, _ringDistFromCentre);
                    Vector3 goPosition = unitOffset;
                    goPosition += _unitRing.transform.position;
                    GameObject parentUnitGO = Instantiate(_parentUnitPrefab, goPosition, Quaternion.identity, _unitRing.transform);
                    AddLifeToBuildTime(parentUnitGO);
                    _theseUnits.ParentUnits.Add(parentUnitGO);
                    _parentUnits[parentUnitIndex] = parentUnitGO;
                    parentUnitGO.name = "Parent Unit " + parentUnitIndex.ToString();
                    ParentUnitManager pum = parentUnitGO.GetComponent<ParentUnitManager>();
                    _controllerManager.ParentUnitManagers.Add(pum);
                    if (pum.GetComponent<ParentHealth>() != null)
                    {
                        _controllerManager.ParentHealths.Add(pum.GetComponent<ParentHealth>());
                    }
                    pum?.Initialize(controllerIndex, parentUnitIndex, compsAndUnits.PlayerUnits[controllerIndex], transform, null, unitComp.ParentUnitClassList[parentUnitIndex], mainManager);
                    _hilightRingParentCreator?.CreateHilightRingParent(parentUnitIndex, unitComp.ParentUnitCount, unitOffset);
                    pum.HilightRingParentManager = _hilightRingParentCreator?.GetLastCreatedHilightRingParentManager();
                    pum.HilightRingParentManager.GetComponent<RotateParentUnit>()?.Initialize(_controllerManager.transform);
                    pum.UnitActivation.HilightRingParentManager = pum.HilightRingParentManager;
                    pum.ChrysalilsActivation.HilightRingParentManager = pum.HilightRingParentManager;
                    UnitManaCostText manaText = parentUnitGO.GetComponent<UnitManaCostText>();
                    manaText?.SetManaText(unitComp.ParentUnitClassList[parentUnitIndex].AdvUnitClass.ManaCost.ToString());
                    pum.SubUnits = new GameObject[unitComp.UpgradeCountArray[parentUnitIndex]];
                    pum.SubChrysalii = new GameObject[unitComp.UpgradeCountArray[parentUnitIndex]];
                    _parentUnitHatchEffects[parentUnitIndex] = parentUnitGO.GetComponent<ParentUnitHatchEffects>();
                    {
                        {
                            int _childUnitIndex = 0;
                            UnitClass unitClass = unitComp.ParentUnitClassList[parentUnitIndex].BasicUnitClass;
                            CreateChildUnitAndChrysalis(unitClass, _childUnitIndex, pum, parentUnitIndex, compsAndUnits, false);
                            _childUnitIndex = 1;
                            unitClass = unitComp.ParentUnitClassList[parentUnitIndex].AdvUnitClass;
                            CreateChildUnitAndChrysalis(unitClass, _childUnitIndex, pum, parentUnitIndex, compsAndUnits, true);
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
            childUnitGo.SetActive(false);
            UnitStatsSO _unitStats = _unitComp.UnitSOArray[(parentUnitIndex * 3) + (childUnitIndex)];
            UnitManager _childUnitManager = childUnitGo.GetComponent<UnitManager>();
            pum.ChildUnitManagers.Add(_childUnitManager);
            childUnitGo.GetComponent<UnitManager>()?.Initialize(_controllerManager, _controllerIndex, unitClass, parentUnitIndex, childUnitIndex, compsAndUnits, false);
            childUnitGo.GetComponent<UnitManager>()?.SetUnitSPrite(unitClass.UnitSprite);
            _childUnitManager.SetUnitName(unitClass.UnitName);
            pum.SubUnits[childUnitIndex] = childUnitGo;
            childUnitGo.name = $"Child Unit " + childUnitIndex;
            _allUnits.Add(childUnitGo);
            _controllerHatchEffect.SetUnitsArray(childUnitGo, ((parentUnitIndex * 6) + (childUnitIndex * 2)));
            childUnitGo.SetActive(false);
            pum.HilightRingParentManager.CreateHilightRingUnit(unitClass.UnitSprite);
            _childUnitManager.Hatch = childUnitGo.AddComponent<Hatch>();
            _childUnitManager.Hatch?.Initialize(_controllerManager, pum);

            GameObject chrysalisGo = Instantiate(_chrysalisPrefab, pum.transform);
            chrysalisGo.SetActive(false);
            chrysalisGo.name = $"Chrysalis " + childUnitIndex;
            UnitManager _chrysalisManager = chrysalisGo.GetComponent<UnitManager>();
            pum.ChildChrysaliiUnitManagers.Add(_chrysalisManager);
            UnitManager chrysalisUnitManager = chrysalisGo.GetComponent<UnitManager>();
            chrysalisGo.GetComponent<UnitManager>()?.Initialize(_controllerManager, _controllerIndex, unitClass, parentUnitIndex, childUnitIndex, compsAndUnits, true);
            chrysalisGo.GetComponent<UnitManager>()?.SetUnitSPrite(unitClass.ChrysalisSprite);            
            chrysalisGo.GetComponent<ChrysalisTimer>()?.Initialize(unitClass.BuildTime, childUnitIndex, pum.GetComponent<ProgressBarManager>());
            pum.SubChrysalii[childUnitIndex] = chrysalisGo;
            _allUnits.Add(chrysalisGo);
            _controllerHatchEffect.SetUnitsArray(chrysalisGo, ((parentUnitIndex * 6) + (childUnitIndex * 2) + 1));
            pum.HilightRingParentManager.CreateHilightRingChrysalis(unitClass.ChrysalisSprite);
            if (isAdvUnit)
            {
                chrysalisUnitManager?.ControllerUnitSpriteHandler?.TintSpriteMagenta();
            }
        }
        private void AddLifeToBuildTime(GameObject parentUnit)
        {
            if (PlayerPrefsInteract.GetHealthToBuildTime() == 2)
            {
                parentUnit.AddComponent<BuildTimeFasterWithHigherHP>();
                return;
            }
            if (PlayerPrefsInteract.GetHealthToBuildTime() == 1)
            {
                parentUnit.AddComponent<HealthToBuildTimeLinear>();
                return;
            }
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
                        int childIndex = 0;
                        if (!PlayerPrefsInteract.GetHasBasicUnit())
                        {
                            childIndex = 1;
                        }
                        thisParentUnit.GetComponent<UnitActivation>().ActivateUnit(childIndex);
                        thisParentUnit.CurrentSubUnit = thisParentUnit.ChildUnitManagers[childIndex].gameObject;

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
                        _manaGenerator?.AddToStartingMana(parentUnitClass.AdvancedUpgradesUTCList.Count);

                    }
                    if (parentUnitClass.UTCHatchEffects != null)
                    {
                        _manaGenerator?.AddToStartingMana(1);
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
