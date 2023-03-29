using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ChrysalisActivation : MonoBehaviour
    {
        private bool _debugBool = false;
        private ParentUnitManager _parentUnitManager;
        private UnitActivation _unitActivation;
        private ParentHealth _parentHealth;
        private TargetScoreValue _targetScoreValue;
        private ProgressBarManager _progressBarManager;
        [SerializeField] private HilightRingParentManager _hilightRingParentManager;
        public HilightRingParentManager HilightRingParentManager { get => _hilightRingParentManager; set => _hilightRingParentManager = value; }
        private FreeChrysalisStoresHealth _freeChrysalisStoresHealth;

        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _unitActivation = GetComponent<UnitActivation>();
            _parentHealth = GetComponent<ParentHealth>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _progressBarManager = GetComponent<ProgressBarManager>();
            _freeChrysalisStoresHealth = GetComponent<FreeChrysalisStoresHealth>();
        }
        public void DeactivateChrysalis(int _chryslisIndex)
        {
            if (_freeChrysalisStoresHealth != null)
            {
                _freeChrysalisStoresHealth.SetStoredHealth(_parentHealth.CurrentHealth);
            }
            if (_parentUnitManager.ChildChrysaliiUnitManagers.Count > _chryslisIndex) 
            {
                if (_parentUnitManager.ChildChrysaliiUnitManagers[_chryslisIndex].gameObject != null)
                {
                    if (_parentUnitManager.ChildChrysaliiUnitManagers[_chryslisIndex].gameObject.activeInHierarchy == true)
                    {
                        _parentUnitManager.ChildChrysaliiUnitManagers[_chryslisIndex].gameObject.SetActive(false);
                    }
                }
                if (_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.ChildChrysaliiUnitManagers[_chryslisIndex].gameObject))
                {
                    _parentUnitManager.TheseUnits.ActiveUnits.Remove(_parentUnitManager.ChildChrysaliiUnitManagers[_chryslisIndex].gameObject);
                }
            }
        }
        public void ActivateChrysalis(int chrysalisIndex)
        {
            if (_parentUnitManager.CurrentSubUnit == null)
            {
                _parentUnitManager.CurrentSubUnit = _parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject;
            }
            if (_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject == null) return;
            // if ((_chrysalisIndex == 0) && (_parentUnitManager.CurrentSubUnit != _parentUnitManager._subUnits[0])) return;
            // if (_parentUnitManager.CurrentSubUnit != _parentUnitManager.SubChrysalii[_chrysalisIndex])
            _parentHealth.SetChrysalis(true);
            _parentHealth.SetMaxHealth(_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].UnitClass.HPMax);
            if (_freeChrysalisStoresHealth != null)
            {
                _parentHealth.SetCurrentHealth(_freeChrysalisStoresHealth.StoredHealth);
            }
            float _timeToKeep = 0;
            ChrysalisTimer _chrysalisTimer = _parentUnitManager.CurrentSubUnit.GetComponent<ChrysalisTimer>();
            if (_chrysalisTimer != null)
            {
                _timeToKeep = _chrysalisTimer.TimerCurrent;
            }
            for (int _indexInSubChrysalis = 0; _indexInSubChrysalis < _parentUnitManager.ChildChrysaliiUnitManagers.Count; _indexInSubChrysalis++)
            {
                if (_indexInSubChrysalis == chrysalisIndex)
                {
                    _parentUnitManager.CurrentSubUnit = _parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject;
                    if (_debugBool)
                    {
                        Debug.Log($"activating chrysalis" + chrysalisIndex);
                    }
                    _parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject.SetActive(true);
                    _parentHealth.CurrentUnit = _parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex];
                    _parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].GetComponent<ChrysalisTimer>()?.StartThisChrysalis(_timeToKeep);
                    if (!_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject))
                    {
                        _parentUnitManager.TheseUnits.ActiveUnits.Add(_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject);
                    }
                }
                else DeactivateChrysalis(_indexInSubChrysalis);
            }
            for (int _unitIndex = 0; _unitIndex < _parentUnitManager.ChildUnitManagers.Count; _unitIndex++)
            {
                _unitActivation.DeactivateUnit(_unitIndex);
            }
            _progressBarManager?.TintChrysalis();
            _hilightRingParentManager?.ActivateChrysalis(chrysalisIndex);
            _targetScoreValue.SetEgg(true);
        }
    }
}
