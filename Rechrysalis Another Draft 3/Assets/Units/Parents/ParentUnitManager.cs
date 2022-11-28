using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using System;

namespace Rechrysalis.Unit
{
    public class ParentUnitManager : MonoBehaviour
    {
        private int _parentIndex;
        [SerializeField] private int _controllerIndex;
        [SerializeField] private GameObject[] _subUnits;
        public GameObject[] SubUnits {get {return _subUnits;}set {_subUnits = value;}}
        [SerializeField] private GameObject[] _subChrysalii;
        public GameObject[] SubChrysalii {get{return _subChrysalii;}set {_subChrysalii = value;}}
        // private List<HatchEffectManager> _hatchEffectManagersToDamage;
        private HatchEffectSO[] _subHatchEffects;        
        private PlayerUnitsSO _theseUnits;
        private GameObject _currentSubUnit;
        private RotateParentUnit _rotateParentUnit;
        private ParentHealth _parentHealth;
        private ParentUnitHatchEffects _pUHE;
        private float _manaAmount;
        public float ManaAmount {set{_manaAmount = value;}}
        public Action<GameObject, int, int, bool> _addHatchEffect;
        public Action<GameObject, int, bool> _removeHatchEffect;
        public Action<float> _parentDealsDamage;
        public Action<float> _subtractMana;

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

        public void Initialize(int _controllerIndex, int _parentUnitIndex, CompSO _unitComp, PlayerUnitsSO _theseUnits, Transform _controllertransform, HatchEffectSO[] _subHatchEffects)
        {
            // _hatchEffectManagersToDamage = new List<HatchEffectManager>();
            // _hatchEffectManagersToDamage.Clear();
            this._parentIndex = _parentUnitIndex;
            this._subHatchEffects = _subHatchEffects;
            this._controllerIndex = _controllerIndex;
            this._theseUnits = _theseUnits;
            _parentHealth = GetComponent<ParentHealth>();
            // AddChrysalisAndUnitActions();
            _rotateParentUnit = GetComponent<RotateParentUnit>();
            _rotateParentUnit?.Initialize(_controllertransform);
            _pUHE = GetComponent<ParentUnitHatchEffects>();
            GetComponent<ParentClickManager>()?.Initialize(_controllerIndex);
        }
        public void Tick()
        {
            _rotateParentUnit?.Tick();
        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
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
                    _chryslisTimer._startUnit -= ActivateUnit;
                    _chryslisTimer._startUnit += ActivateUnit;
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
                    _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= ActivateUnit;
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
            Debug.Log($"parent deals damage " + _damage);
            _parentDealsDamage?.Invoke(_damage);
        }
        public void ActivateInitialUnit()
        {
            // Debug.Log($"set health" + _subUnits[0].GetComponent<UnitManager>().UnitStats.HealthMax);
            _parentHealth.SetMaxHealth(_subUnits[0].GetComponent<UnitManager>().UnitStats.HealthMax);
            ActivateUnit(0);            
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

                _parentHealth.SetMaxHealth(_subUnits[_chrysalisIndex].GetComponent<UnitManager>().UnitStats.HealthMax);
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
        }
        public void ActivateUnit(int _unitIndex)
        {
            // Debug.Log($"activating");
            for (int _indexInSubUnits=0; _indexInSubUnits<_subUnits.Length; _indexInSubUnits++)
            {
                if (_indexInSubUnits == _unitIndex)  
                {
                    _currentSubUnit = _subUnits[_unitIndex];
                    _subUnits[_unitIndex].SetActive(true);
                    _parentHealth.CurrentUnit = _subUnits[_unitIndex].GetComponent<UnitManager>();
                    _parentHealth.SetChrysalis(false);
                    UnitManager _unitManager = _subUnits[_unitIndex].GetComponent<UnitManager>();
                    int _tier = _unitManager.UnitStats.TierMultiplier.Tier - 1;
                    HatchEffectSO _hatchEffectSO = _subHatchEffects[_unitIndex];
                    _subUnits[_unitIndex].GetComponent<UnitManager>()?.RestartUnit();
                    if (!_theseUnits.ActiveUnits.Contains(_subUnits[_indexInSubUnits]))
                    {
                        _theseUnits.ActiveUnits.Add(_subUnits[_unitIndex]);
                    }
                    if (_hatchEffectSO != null)
                    {
                        CreateHatchEffect(_unitManager.HatchEffectPrefab, _tier, _parentIndex, _unitIndex, _hatchEffectSO.AffectAll[_tier]);
                    }
                }
                DeactivateChrysalis(_indexInSubUnits);    
            }
        }
        private void DeactivateChrysalis(int _chryslisIndex)
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
        private void CreateHatchEffect(GameObject _hatchEffectPrefab, int _unitTier, int _parentIndex, int _unitIndex, bool _affectAll)
        {
            // Debug.Log($""+ _hatchEffectPrefab.name + "tier " + _unitTier + " parent " + _parentIndex + " unit " + _unitIndex);
            if ((_hatchEffectPrefab != null) && (_subHatchEffects[_unitIndex] != null))
            {
                GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
                HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
                // Debug.Log($"creating hatch effect unit index " +_unitIndex);
                _hatchEffectManager?.Initialize(_subHatchEffects[_unitIndex], _unitTier, _parentIndex, _unitIndex, _affectAll);
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
        }
    }
}
