using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using System;
using TMPro;

namespace Rechrysalis.Unit
{
    public class ParentUnitManager : MonoBehaviour
    {
        [SerializeField] private ParentUnitClass _parentUnitClass;
        public ParentUnitClass ParentUnitClass => _parentUnitClass;
        private int _parentIndex;
        public int ParentIndex => _parentIndex;
        [SerializeField] private int _controllerIndex;
        [SerializeField] private GameObject[] _subUnits;
        public GameObject[] SubUnits {get {return _subUnits;}set {_subUnits = value;}}
        [SerializeField] private GameObject[] _subChrysalii;
        public GameObject[] SubChrysalii {get{return _subChrysalii;}set {_subChrysalii = value;}}
        // private List<HatchEffectManager> _hatchEffectManagersToDamage;
        private HatchEffectSO[] _subHatchEffects; 
        public HatchEffectSO[] SubHatchEffects {get { return _subHatchEffects;}}       
        private PlayerUnitsSO _theseUnits;
        public PlayerUnitsSO TheseUnits {get {return _theseUnits;}}
        private GameObject _currentSubUnit;
        public GameObject CurrentSubUnit {get { return _currentSubUnit;} set { _currentSubUnit = value;} }
        private RotateParentUnit _rotateParentUnit;
        private ParentHealth _parentHealth;
        private ParentUnitHatchEffects _pUHE;
        private UnitActivation _unitActivation;
        private TargetScoreValue _targetScoreValue;
        private float _manaAmount;
        public float ManaAmount {set{_manaAmount = value;}}
        public Action<GameObject, int, int, bool> _addHatchEffect;
        public Action<GameObject, int, bool> _removeHatchEffect;
        public Action<float> _parentDealsDamage;
        public Action<float> _subtractMana;
        [SerializeField] private TMP_Text _manaText;

        private bool _isStopped;
        public bool IsStopped 
        {
            set{
                _isStopped = value;
                foreach(GameObject _unit in _subUnits)
                {
                    _unit.GetComponent<UnitManager>().IsStopped = _isStopped;
                }
            }
         }

        public void Initialize(int _controllerIndex, int _parentUnitIndex, CompSO unitComp, PlayerUnitsSO _theseUnits, Transform _controllertransform, HatchEffectSO[] _subHatchEffects)
        {
            // _hatchEffectManagersToDamage = new List<HatchEffectManager>();
            // _hatchEffectManagersToDamage.Clear();
            _unitActivation = GetComponent<UnitActivation>();
            _unitActivation?.Initialize(this);
            this._parentIndex = _parentUnitIndex;
            this._subHatchEffects = _subHatchEffects;
            this._controllerIndex = _controllerIndex;
            this._theseUnits = _theseUnits;
            _parentUnitClass = unitComp.ParentUnitClassList[_parentUnitIndex];
            _parentHealth = GetComponent<ParentHealth>();
            // AddChrysalisAndUnitActions();
            _rotateParentUnit = GetComponent<RotateParentUnit>();
            _rotateParentUnit?.Initialize(_controllertransform);
            _pUHE = GetComponent<ParentUnitHatchEffects>();
            GetComponent<ParentClickManager>()?.Initialize(_controllerIndex);
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _targetScoreValue?.Initialize();
        }
        public void Tick()
        {
            _rotateParentUnit?.Tick();
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
            _parentHealth._unitDies -= ActivateChrysalis;
            _parentHealth._unitDies += ActivateChrysalis;
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
        public void ActivateInitialUnit()
        {
            // Debug.Log($"set health" + _subUnits[0].GetComponent<UnitManager>().UnitStats.HealthMax);
            _parentHealth.SetMaxHealth(_subUnits[0].GetComponent<UnitManager>().UnitClass.HPMax);
            _unitActivation.ActivateUnit(0);            
        }
        public void UpgradeUnit(int _chrysalisIndex)
        {
            if ((_chrysalisIndex == 0) && (_currentSubUnit != _subUnits[0])) return;
            if (_currentSubUnit == _subChrysalii[_chrysalisIndex]) return;
            if (_chrysalisIndex == 0) return;
            if (!CheckIfEnoughMana(_chrysalisIndex)) return;
            SubtractMana(_chrysalisIndex);
            ActivateChrysalis(_chrysalisIndex);            
        }
        private bool CheckIfEnoughMana(int _chrysalisIndex)
        {
            if ((_subUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost <= _manaAmount))
            {
                return true;
            }
            return false;
        }
        private void SubtractMana(int _chrysalisIndex)
        {
            _subtractMana?.Invoke(_subUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost);
        }
        public void ActivateChrysalis(int _chrysalisIndex)
        {
            if (_subChrysalii[_chrysalisIndex] == null) return;
            // if ((_chrysalisIndex == 0) && (_currentSubUnit != _subUnits[0])) return;
            // if (_currentSubUnit != _subChrysalii[_chrysalisIndex])
            {
                _parentHealth.SetChrysalis(true);

                _parentHealth.SetMaxHealth(_subUnits[_chrysalisIndex].GetComponent<UnitManager>().UnitClass.HPMax);
            float _timeToKeep = 0;
            ChrysalisTimer _chrysalisTimer = _currentSubUnit.GetComponent<ChrysalisTimer>();
            if (_chrysalisTimer != null)
            {
                _timeToKeep = _chrysalisTimer.TimerCurrent;
            }
            for (int _indexInSubChrysalis=0; _indexInSubChrysalis<_subChrysalii.Length; _indexInSubChrysalis++)
            {
                if (_indexInSubChrysalis == _chrysalisIndex)
                {
                    _currentSubUnit = _subChrysalii[_chrysalisIndex];
                    Debug.Log($"activating chrysalis" + _chrysalisIndex);
                    _subChrysalii[_chrysalisIndex].SetActive(true);
                    _parentHealth.CurrentUnit = _subChrysalii[_chrysalisIndex].GetComponent<UnitManager>();
                    _subChrysalii[_chrysalisIndex].GetComponent<ChrysalisTimer>()?.StartThisChrysalis(_timeToKeep);
                    if (!_theseUnits.ActiveUnits.Contains(_subChrysalii[_chrysalisIndex]))
                    {
                        _theseUnits.ActiveUnits.Add(_subChrysalii[_chrysalisIndex]);
                    }                    
                }
                else DeactivateChrysalis(_indexInSubChrysalis);
            }
            for (int _unitIndex = 0; _unitIndex < _subUnits.Length; _unitIndex ++)
            {
                DeactivateUnit(_unitIndex);
            }
            }
            _targetScoreValue.SetEgg(true);
        }
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
        public void DeactivateChrysalis(int _chryslisIndex)
        {
            if (_subChrysalii[_chryslisIndex] != null) {
                if (_subChrysalii[_chryslisIndex].activeInHierarchy == true)
                {
                    _subChrysalii[_chryslisIndex].SetActive(false);
                }
            }
            if (_theseUnits.ActiveUnits.Contains(_subChrysalii[_chryslisIndex]))
            {
                _theseUnits.ActiveUnits.Remove(_subChrysalii[_chryslisIndex]);
            }
        }
        private void DeactivateUnit(int _unitIndex)
        {
            if (_subUnits[_unitIndex] != null) {
                if (_subUnits[_unitIndex].activeInHierarchy == true)
                {
                    _subUnits[_unitIndex].SetActive(false);
                }
            }
            if (_theseUnits.ActiveUnits.Contains(_subUnits[_unitIndex]))
            {
                _theseUnits.ActiveUnits.Remove(_subUnits[_unitIndex]);
            }
        }
        public void CreateHatchEffect(GameObject _hatchEffectPrefab, int _parentIndex, int _unitIndex, bool _affectAll)
        {
            // Debug.Log($""+ _hatchEffectPrefab.name + "tier " + _unitTier + " parent " + _parentIndex + " unit " + _unitIndex);
            if ((_hatchEffectPrefab != null) && (_subHatchEffects[_unitIndex] != null))
            {
                GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
                HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
                // Debug.Log($"creating hatch effect unit index " +_unitIndex);
                _hatchEffectManager?.Initialize(_subHatchEffects[_unitIndex], _parentIndex, _unitIndex, _affectAll, _parentUnitClass.AdvUnitClass.HatchEffectMult);
                // HETimer _hETimer = _hatchEffect.GetComponent<HETimer>();
                // _hETimer?.Initialize(_unitIndex);
                // foreach (GameObject _subUnit in _subUnits)
                // {
                //     _subUnit.GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                // }
                // foreach (GameObject _chrysalis in _subChrysalii)
                // {
                //     _chrysalis.GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                // }
                // Debug.Log($"Action Hatch effect " + _hatchEffect.name);
                _addHatchEffect?.Invoke(_hatchEffect, _parentIndex, _unitIndex, _hatchEffectManager.AffectAll);
            }
        }
        public void ReserveChrysalis(int _parentIndex, int _childIndex)
        {
            Rechrysalize _rechrysalize = _currentSubUnit.GetComponent<Rechrysalize>();
            if (_rechrysalize!= null)
            {
                Debug.Log($"reserve chrysalis");
                _rechrysalize.SetNextEvolved(_childIndex);
                return;
            }
            ActivateChrysalis(_childIndex);
        }
        public void RemoveHatchEffect (GameObject _hatchEffect)
        {
            // HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
            // if (_hatchEffectManager != null)
            // {
            //     if (_hatchEffectManagersToDamage.Contains(_hatchEffectManager))
            //     {
            //         _hatchEffectManagersToDamage.Remove(_hatchEffectManager);
            //     }
            // }
            // _pUHE?.RemoveHatchEffect(_hatchEffect);
            if (_subUnits.Length > 0)
            {
                for (int _childIndex =0; _childIndex < _subUnits.Length; _childIndex ++)
                {
                    if (_subUnits[_childIndex] != null)
                    {                    
                        _subUnits[_childIndex].GetComponent<UnitManager>()?.RemoveHatchEffect(_hatchEffect);
                    }                    
                }
                for (int _childIndex = 0; _childIndex< _subChrysalii.Length; _childIndex ++)
                {
                    if (_subChrysalii[_childIndex] != null)
                    {
                         _subChrysalii[_childIndex].GetComponent<UnitManager>()?.RemoveHatchEffect(_hatchEffect);
                    }
                }
            }
            CheckToModifyParentDefencesFromHEChanges(_hatchEffect);
        }
        public void AddHatchEffect (GameObject _hatchEffect)
        {
            // _hatchEffectManagersToDamage.Add(_hatchEffect.GetComponent<HatchEffectManager>());
            // _pUHE?.AddHatchEffect(_hatchEffect);
            if (_subUnits.Length >0)
            {
                for (int _childIndex = 0; _childIndex < _subUnits.Length; _childIndex ++)
                {
                    if (_subUnits[_childIndex] != null)
                    {
                        _subUnits[_childIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                    }
                }
                for (int _childIndex = 0; _childIndex < _subChrysalii.Length; _childIndex ++)
                {
                    if (_subChrysalii[_childIndex] != null)
                    {
                        _subChrysalii[_childIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                    }
                }
            }      
            CheckToModifyParentDefencesFromHEChanges(_hatchEffect);  
        }
        private void CheckToModifyParentDefencesFromHEChanges(GameObject hatchEffect)
        {
            if (hatchEffect == null) return;
            if (hatchEffect.GetComponent<HEIncreaseDefence>() == null) return;
            List<HEIncreaseDefence> hEIncraseDefenceList = new List<HEIncreaseDefence>();
            foreach(GameObject hatchEffectToLoop in _subUnits[0].GetComponent<UnitManager>().CurrentHatchEffects)
            {
                if (hatchEffectToLoop != null)
                {
                    if (hatchEffectToLoop.GetComponent<HEIncreaseDefence>() != null)
                    {
                        hEIncraseDefenceList.Add(hatchEffectToLoop.GetComponent<HEIncreaseDefence>());
                    }
                }
            }
            _parentHealth.ReCalculateIncomingDamageModifier(hEIncraseDefenceList);
        }
        public void SetManaText(string manaText)
        {
            _manaText.text = manaText;
        }
    }
}
