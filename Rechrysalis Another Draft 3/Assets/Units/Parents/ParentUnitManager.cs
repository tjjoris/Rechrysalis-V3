using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using System;
using TMPro;
using Rechrysalis.Movement;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ParentUnitManager : MonoBehaviour
    {
        [SerializeField] private ParentUnitClass _parentUnitClass;
        public ParentUnitClass ParentUnitClass {get => _parentUnitClass; set => _parentUnitClass = value; }
        private int _parentIndex;
        public int ParentIndex => _parentIndex;
        [SerializeField] private int _controllerIndex;
        [SerializeField] private GameObject[] _subUnits;
        public GameObject[] SubUnits {get {return _subUnits;}set {_subUnits = value;}}
        [SerializeField] private GameObject[] _subChrysalii;
        [SerializeField] private List<UnitManager> _childUnitManagers;
        public List<UnitManager> ChildUnitManagers { get => _childUnitManagers; set => _childUnitManagers = value; }
        
        public GameObject[] SubChrysalii {get{return _subChrysalii;}set {_subChrysalii = value;}}
        // private List<HatchEffectManager> _hatchEffectManagersToDamage;
        [SerializeField] private List<UnitManager> _childChrysaliiUnitManagers;
        public List<UnitManager> ChildChrysaliiUnitManagers { get => _childChrysaliiUnitManagers; set => _childChrysaliiUnitManagers = value; }
        
        private HatchEffectSO[] _subHatchEffects; 
        public HatchEffectSO[] SubHatchEffects {get { return _subHatchEffects;}}       
        private PlayerUnitsSO _theseUnits;
        public PlayerUnitsSO TheseUnits {get {return _theseUnits;}}
        private GameObject _currentSubUnit;
        public GameObject CurrentSubUnit {get { return _currentSubUnit;} set { _currentSubUnit = value;} }
        private RotateParentUnit _rotateParentUnit;
        private ParentHealth _parentHealth;
        private ParentUnitHatchEffects _pUHE;
        private ParentHatchEffectAddRemove _parentHatchEffectAddRemove;
        private UnitActivation _unitActivation;
        private ChrysalisActivation _chrysalisActivation;
        private UpgradeUnit _upgradeUnit;
        private TargetScoreValue _targetScoreValue;
        private ParentFreeEnemyManager _parentFreeEnemyManager;
        private AIFlawedUpdate _aiFlawedUpdate;
        private AIAlwaysPreferClosest _aiAlwaysPreferClosest;
        private FreeEnemyKiteMaxRange _freeEnemyKiteMaxRange;
        private FreeEnemyApproach _freeApproach;
        private bool _aiCanMove = true;
        public bool AICanMove => _aiCanMove;
        private Mover _parentUnitMover;
        public Mover ParentUnitMover => _parentUnitMover;
        private float _manaAmount;
        public float ManaAmount {set{_manaAmount = value;} get {return _manaAmount;} }
        public Action<GameObject, int, int, bool> _addHatchEffect;
        public Action<GameObject, int, bool> _removeHatchEffect;
        public Action<float> _parentDealsDamage;
        public Action<float> _subtractMana;
        // [SerializeField] private TMP_Text _manaText;

        private bool _isStopped;
        public bool IsStopped 
        {
            set{
                _isStopped = value;
                // foreach(GameObject _unit in _subUnits)
                // {
                //     _unit.GetComponent<UnitManager>().IsStopped = _isStopped;
                // }
            }
            get {return _isStopped;}
         }

        public void Initialize(int _controllerIndex, int _parentUnitIndex, CompSO unitComp, PlayerUnitsSO _theseUnits, Transform _controllertransform, HatchEffectSO[] _subHatchEffects, ParentUnitClass parentUnitClass)
        {
            _childUnitManagers = new List<UnitManager>();
            _childChrysaliiUnitManagers = new List<UnitManager>();
            // _hatchEffectManagersToDamage = new List<HatchEffectManager>();
            // _hatchEffectManagersToDamage.Clear();
            _unitActivation = GetComponent<UnitActivation>();
            _unitActivation?.Initialize(this);
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
            _chrysalisActivation?.Initialize(this);
            _upgradeUnit = GetComponent<UpgradeUnit>();
            _upgradeUnit?.Initialize(this);
            this._parentIndex = _parentUnitIndex;
            this._subHatchEffects = _subHatchEffects;
            this._controllerIndex = _controllerIndex;
            this._theseUnits = _theseUnits;
            // _parentUnitClass = unitComp.ParentUnitClassList[_parentUnitIndex];
            _parentUnitClass = parentUnitClass;
            _parentHealth = GetComponent<ParentHealth>();
            // AddChrysalisAndUnitActions();
            _rotateParentUnit = GetComponent<RotateParentUnit>();
            _rotateParentUnit?.Initialize(_controllertransform);
            _pUHE = GetComponent<ParentUnitHatchEffects>();
            _parentHatchEffectAddRemove = GetComponent<ParentHatchEffectAddRemove>();
            _parentHatchEffectAddRemove?.Initialzie(this);
            GetComponent<ParentClickManager>()?.Initialize(_controllerIndex);
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _targetScoreValue?.Initialize();
            _aiFlawedUpdate = GetComponent<AIFlawedUpdate>();
            _aiAlwaysPreferClosest = GetComponent<AIAlwaysPreferClosest>();
            _aiAlwaysPreferClosest?.Initialize();
            _freeEnemyKiteMaxRange = GetComponent<FreeEnemyKiteMaxRange>();
            _freeEnemyKiteMaxRange?.Initialize();
            _freeApproach = GetComponent<FreeEnemyApproach>();
            _freeApproach.Initialize(_theseUnits, _controllertransform.GetComponent<ControllerManager>().EnemyController.GetComponent<Mover>());
            _parentFreeEnemyManager = GetComponent<ParentFreeEnemyManager>();
            _parentUnitMover = GetComponent<Mover>();
            _parentUnitMover.Initialize(_controllerIndex);
        }
        public void Tick(float timeAmount)
        {
            _aiFlawedUpdate?.Tick(timeAmount);
            bool _isRetreating = false;
            _aiAlwaysPreferClosest.CheckIfTargetInRange();
            if (_freeEnemyKiteMaxRange != null)
            {
                _freeEnemyKiteMaxRange.Tick(_aiCanMove);
                _isRetreating = _freeEnemyKiteMaxRange.GetRetreating();
            }
            if (_freeApproach != null)
            {
                _freeApproach?.Tick(_isRetreating, _aiCanMove);
                if ((!_isRetreating) && (!_freeApproach.GetIsApproaching()))
                {
                    _parentUnitMover.SetDirection(Vector2.zero);
                }
            }

            _rotateParentUnit?.Tick();
            _parentFreeEnemyManager?.Tick(timeAmount);
            TickChildUnits(timeAmount);
        }
        private void TickChildUnits(float timeAmount)
        {
            foreach (UnitManager childUnit in _childUnitManagers)
            {
                if (childUnit.gameObject.activeInHierarchy)
                {
                    childUnit.Tick(timeAmount);
                }
            }
            foreach (UnitManager childChrysalii in _childChrysaliiUnitManagers)
            {
                if (childChrysalii.gameObject.activeInHierarchy)
                {
                    childChrysalii.Tick(timeAmount);
                }
            }
        }
        private void Awake()
        {
            _unitActivation = GetComponent<UnitActivation>();
        }
        private void OnEnable()
        {
            // _unitActivation = GetComponent<UnitActivation>();
            AddChrysalisAndUnitActions();
        }
        public void AddChrysalisAndUnitActions()
        {
            // foreach (GameObject _chrysalis in _subChrysalii)
            // {
            //     _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= ActivateUnit;
            //     _chrysalis.GetComponent<ChrysalisTimer>()._startUnit += ActivateUnit;
            // }
            // foreach (GameObject _unit in _subUnits)
            // {
            //     _unit.GetComponent<Rechrysalize>()._startChrysalis -= ActivateChrysalis;
            //     _unit.GetComponent<Rechrysalize>()._startChrysalis += ActivateChrysalis; 
            // }
            foreach (Transform _child in transform)
            {                
                ChrysalisTimer _chryslisTimer = _child.GetComponent<ChrysalisTimer>();
                if (_chryslisTimer != null)
                {
                    // Debug.Log($"chryslis timer " + _child);
                    _chryslisTimer._startUnit -= _unitActivation.ActivateUnit;
                    _chryslisTimer._startUnit += _unitActivation.ActivateUnit;
                }
                // Rechrysalize _rechrysalize = _child.GetComponent<Rechrysalize>();
                // if (_rechrysalize != null)
                // {
                //     // Debug.Log($"rechrysalize " + _child);
                //     _rechrysalize._startChrysalis -= ActivateChrysalis;
                //     _rechrysalize._startChrysalis += ActivateChrysalis;
                // }
            }
            _parentHealth = GetComponent<ParentHealth>();
            if (_chrysalisActivation != null)
            {
                _parentHealth._unitDies -= _chrysalisActivation.ActivateChrysalis;
                _parentHealth._unitDies += _chrysalisActivation.ActivateChrysalis;
            }
            if ((_subUnits != null) && (_subUnits.Length>0))
            {
                for (int _subIndex = 0; _subIndex < _subUnits.Length; _subIndex ++)
                {
                    if (_subUnits[_subIndex] != null)
                    {
                        _subUnits[_subIndex].GetComponent<UnitManager>()._unitDealsDamage -= ParentDealsDamage;
                        _subUnits[_subIndex].GetComponent<UnitManager>()._unitDealsDamage += ParentDealsDamage;
                    }
                }
            }
        }
        private void OnDisable()
        {
            foreach (GameObject _chrysalis in _subChrysalii)
            {
                if (_chrysalis != null)
                {
                    _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= _unitActivation.ActivateUnit;
                }
            }
            // foreach (GameObject _unit in _subUnits)
            // {
            //     if (_unit != null)
            //     {
            //         _unit.GetComponent<Rechrysalize>()._startChrysalis -= ActivateChrysalis;
            //     }
            // }
            if (_chrysalisActivation != null)
            {
                _parentHealth._unitDies -= _chrysalisActivation.ActivateChrysalis;
            }
            if ((_subUnits != null) && (_subUnits.Length > 0))
            {
                for (int _subIndex = 0; _subIndex < _subUnits.Length; _subIndex++)
                {
                    if (_subUnits[_subIndex] != null)
                    {
                        _subUnits[_subIndex].GetComponent<UnitManager>()._unitDealsDamage -= ParentDealsDamage;
                    }
                }
            }      
        }
        private void ParentDealsDamage(float _damage)
        {
            // Debug.Log($"parent deals damage " + _damage);
            _parentDealsDamage?.Invoke(_damage);
        }
        // public void ActivateInitialUnit()
        // {
        //     // Debug.Log($"set health" + _subUnits[0].GetComponent<UnitManager>().UnitStats.HealthMax);
        //     _parentHealth.SetMaxHealth(_subUnits[0].GetComponent<UnitManager>().UnitClass.HPMax);
        //     _unitActivation.ActivateUnit(0);            
        // }
        // public void UpgradeUnit(int _chrysalisIndex)
        // {
        //     if ((_chrysalisIndex == 0) && (_currentSubUnit != _subUnits[0])) return;
        //     if (_currentSubUnit == _subChrysalii[_chrysalisIndex]) return;
        //     if (_chrysalisIndex == 0) return;
        //     if (!CheckIfEnoughMana(_chrysalisIndex)) return;
        //     SubtractMana(_chrysalisIndex);
        //     _chrysalisActivation.ActivateChrysalis(_chrysalisIndex);            
        // }
        // private bool CheckIfEnoughMana(int _chrysalisIndex)
        // {
        //     if ((_subUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost <= _manaAmount))
        //     {
        //         return true;
        //     }
        //     return false;
        // }
        // private void SubtractMana(int _chrysalisIndex)
        // {
        //     _subtractMana?.Invoke(_subUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost);
        // }
        // public void ActivateChrysalis(int _chrysalisIndex)
        // {
        //     if (_subChrysalii[_chrysalisIndex] == null) return;
        //     // if ((_chrysalisIndex == 0) && (_currentSubUnit != _subUnits[0])) return;
        //     // if (_currentSubUnit != _subChrysalii[_chrysalisIndex])
        //     {
        //         _parentHealth.SetChrysalis(true);

        //         _parentHealth.SetMaxHealth(_subUnits[_chrysalisIndex].GetComponent<UnitManager>().UnitClass.HPMax);
        //     float _timeToKeep = 0;
        //     ChrysalisTimer _chrysalisTimer = _currentSubUnit.GetComponent<ChrysalisTimer>();
        //     if (_chrysalisTimer != null)
        //     {
        //         _timeToKeep = _chrysalisTimer.TimerCurrent;
        //     }
        //     for (int _indexInSubChrysalis=0; _indexInSubChrysalis<_subChrysalii.Length; _indexInSubChrysalis++)
        //     {
        //         if (_indexInSubChrysalis == _chrysalisIndex)
        //         {
        //             _currentSubUnit = _subChrysalii[_chrysalisIndex];
        //             Debug.Log($"activating chrysalis" + _chrysalisIndex);
        //             _subChrysalii[_chrysalisIndex].SetActive(true);
        //             _parentHealth.CurrentUnit = _subChrysalii[_chrysalisIndex].GetComponent<UnitManager>();
        //             _subChrysalii[_chrysalisIndex].GetComponent<ChrysalisTimer>()?.StartThisChrysalis(_timeToKeep);
        //             if (!_theseUnits.ActiveUnits.Contains(_subChrysalii[_chrysalisIndex]))
        //             {
        //                 _theseUnits.ActiveUnits.Add(_subChrysalii[_chrysalisIndex]);
        //             }                    
        //         }
        //         else _chrysalisActivation.DeactivateChrysalis(_indexInSubChrysalis);
        //     }
        //     for (int _unitIndex = 0; _unitIndex < _subUnits.Length; _unitIndex ++)
        //     {
        //         DeactivateUnit(_unitIndex);
        //     }
        //     }
        //     _targetScoreValue.SetEgg(true);
        // }
        // public void ActivateUnit(int childIndex)
        // {
        //     for (int _indexInSubUnits = 0; _indexInSubUnits < _subUnits.Length; _indexInSubUnits++)
        //     {
        //         if (_indexInSubUnits == childIndex)
        //         {
        //             _currentSubUnit = _subUnits[childIndex];
        //             _subUnits[childIndex].SetActive(true);
        //             _parentHealth.CurrentUnit = _subUnits[childIndex].GetComponent<UnitManager>();
        //             _parentHealth.SetChrysalis(false);

        //         }
        //     }
        // }
        // public void ActivateUnit(int _unitIndex)
        // {
        //     // Debug.Log($"activating");
        //     for (int _indexInSubUnits=0; _indexInSubUnits<_subUnits.Length; _indexInSubUnits++)
        //     {
        //         if (_indexInSubUnits == _unitIndex)  
        //         {
        //             _currentSubUnit = _subUnits[_unitIndex];
        //             _subUnits[_unitIndex].SetActive(true);
        //             _parentHealth.CurrentUnit = _subUnits[_unitIndex].GetComponent<UnitManager>();
        //             _parentHealth.SetChrysalis(false);
        //             UnitManager _unitManager = _subUnits[_unitIndex].GetComponent<UnitManager>();
        //             // int _tier = _unitManager.UnitStats.TierMultiplier.Tier - 1;
        //             HatchEffectSO _hatchEffectSO = _subHatchEffects[_unitIndex];
        //             _subUnits[_unitIndex].GetComponent<UnitManager>()?.RestartUnit();
        //             if (!_theseUnits.ActiveUnits.Contains(_subUnits[_indexInSubUnits]))
        //             {
        //                 _theseUnits.ActiveUnits.Add(_subUnits[_unitIndex]);
        //             }
        //             // if (_hatchEffectSO != null)
        //             if (_unitIndex == 1)
        //             {
        //                 CreateHatchEffect(_parentUnitClass.AdvUnitClass.HatchEffectPrefab, _parentIndex, _unitIndex, true);
        //             }
        //         }
        //         DeactivateChrysalis(_indexInSubUnits);    
        //     }
        //     _targetScoreValue.SetCurrentUnit(_currentSubUnit.GetComponent<Attack>());
        // }
        // public void DeactivateChrysalis(int _chryslisIndex)
        // {
        //     if (_subChrysalii[_chryslisIndex] != null) {
        //         if (_subChrysalii[_chryslisIndex].activeInHierarchy == true)
        //         {
        //             _subChrysalii[_chryslisIndex].SetActive(false);
        //         }
        //     }
        //     if (_theseUnits.ActiveUnits.Contains(_subChrysalii[_chryslisIndex]))
        //     {
        //         _theseUnits.ActiveUnits.Remove(_subChrysalii[_chryslisIndex]);
        //     }
        // }
        // public void DeactivateUnit(int _unitIndex)
        // {
        //     if (_subUnits[_unitIndex] != null) {
        //         if (_subUnits[_unitIndex].activeInHierarchy == true)
        //         {
        //             _subUnits[_unitIndex].SetActive(false);
        //         }
        //     }
        //     if (_theseUnits.ActiveUnits.Contains(_subUnits[_unitIndex]))
        //     {
        //         _theseUnits.ActiveUnits.Remove(_subUnits[_unitIndex]);
        //     }
        // }
        // public void CreateHatchEffect(GameObject _hatchEffectPrefab, int _parentIndex, int _unitIndex, bool _affectAll)
        // {
        //     // Debug.Log($""+ _hatchEffectPrefab.name + "tier " + _unitTier + " parent " + _parentIndex + " unit " + _unitIndex);
        //     if ((_hatchEffectPrefab != null) && (_subHatchEffects[_unitIndex] != null))
        //     {
        //         GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
        //         HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
        //         // Debug.Log($"creating hatch effect unit index " +_unitIndex);
        //         _hatchEffectManager?.Initialize(_subHatchEffects[_unitIndex], _parentIndex, _unitIndex, _affectAll, _parentUnitClass.AdvUnitClass.HatchEffectMult);
        //         // HETimer _hETimer = _hatchEffect.GetComponent<HETimer>();
        //         // _hETimer?.Initialize(_unitIndex);
        //         // foreach (GameObject _subUnit in _subUnits)
        //         // {
        //         //     _subUnit.GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //         // }
        //         // foreach (GameObject _chrysalis in _subChrysalii)
        //         // {
        //         //     _chrysalis.GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //         // }
        //         // Debug.Log($"Action Hatch effect " + _hatchEffect.name);
        //         _addHatchEffect?.Invoke(_hatchEffect, _parentIndex, _unitIndex, _hatchEffectManager.AffectAll);
        //     }
        // }
        // public void ReserveChrysalis(int _parentIndex, int _childIndex)
        // {
        //     Rechrysalize _rechrysalize = _currentSubUnit.GetComponent<Rechrysalize>();
        //     if (_rechrysalize!= null)
        //     {
        //         Debug.Log($"reserve chrysalis");
        //         _rechrysalize.SetNextEvolved(_childIndex);
        //         return;
        //     }
        //     _chrysalisActivation.ActivateChrysalis(_childIndex);
        // }
        // public void RemoveHatchEffect (GameObject _hatchEffect)
        // {
        //     // HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
        //     // if (_hatchEffectManager != null)
        //     // {
        //     //     if (_hatchEffectManagersToDamage.Contains(_hatchEffectManager))
        //     //     {
        //     //         _hatchEffectManagersToDamage.Remove(_hatchEffectManager);
        //     //     }
        //     // }
        //     // _pUHE?.RemoveHatchEffect(_hatchEffect);
        //     if (_subUnits.Length > 0)
        //     {
        //         for (int _childIndex =0; _childIndex < _subUnits.Length; _childIndex ++)
        //         {
        //             if (_subUnits[_childIndex] != null)
        //             {                    
        //                 _subUnits[_childIndex].GetComponent<UnitManager>()?.RemoveHatchEffect(_hatchEffect);
        //             }                    
        //         }
        //         for (int _childIndex = 0; _childIndex< _subChrysalii.Length; _childIndex ++)
        //         {
        //             if (_subChrysalii[_childIndex] != null)
        //             {
        //                  _subChrysalii[_childIndex].GetComponent<UnitManager>()?.RemoveHatchEffect(_hatchEffect);
        //             }
        //         }
        //     }
        //     CheckToModifyParentDefencesFromHEChanges(_hatchEffect);
        // }
        // public void AddHatchEffect (GameObject _hatchEffect)
        // {
        //     // _hatchEffectManagersToDamage.Add(_hatchEffect.GetComponent<HatchEffectManager>());
        //     // _pUHE?.AddHatchEffect(_hatchEffect);
        //     if (_subUnits.Length >0)
        //     {
        //         for (int _childIndex = 0; _childIndex < _subUnits.Length; _childIndex ++)
        //         {
        //             if (_subUnits[_childIndex] != null)
        //             {
        //                 _subUnits[_childIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //             }
        //         }
        //         for (int _childIndex = 0; _childIndex < _subChrysalii.Length; _childIndex ++)
        //         {
        //             if (_subChrysalii[_childIndex] != null)
        //             {
        //                 _subChrysalii[_childIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //             }
        //         }
        //     }      
        //     // CheckToModifyParentDefencesFromHEChanges(_hatchEffect);  
        // }
        // private void CheckToModifyParentDefencesFromHEChanges(GameObject hatchEffect)
        // {
        //     if (hatchEffect == null) return;
        //     if (hatchEffect.GetComponent<HEIncreaseDefence>() == null) return;
        //     List<HEIncreaseDefence> hEIncraseDefenceList = new List<HEIncreaseDefence>();
        //     foreach(GameObject hatchEffectToLoop in _subUnits[0].GetComponent<UnitManager>().CurrentHatchEffects)
        //     {
        //         if (hatchEffectToLoop != null)
        //         {
        //             if (hatchEffectToLoop.GetComponent<HEIncreaseDefence>() != null)
        //             {
        //                 hEIncraseDefenceList.Add(hatchEffectToLoop.GetComponent<HEIncreaseDefence>());
        //             }
        //         }
        //     }
        //     _parentHealth.ReCalculateIncomingDamageModifier(hEIncraseDefenceList);
        // }
        // public void SetManaText(string manaText)
        // {
        //     if (_manaText != null)
        //     {
        //         _manaText.text = manaText;
        //     }
        // }
    }
}
