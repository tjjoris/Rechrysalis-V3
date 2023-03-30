using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Controller;
using Rechrysalis.Movement;

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
        private FreeUnitChrysalisMovementStop _freeUnitChrysalisMovementStop;
        [SerializeField] private HilightRingParentManager _hilightRingParentManager;
        public HilightRingParentManager HilightRingParentManager { get => _hilightRingParentManager; set => _hilightRingParentManager = value; }

        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _unitActivation = GetComponent<UnitActivation>();
            _parentHealth = GetComponent<ParentHealth>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _progressBarManager = GetComponent<ProgressBarManager>(); 
            _freeUnitChrysalisMovementStop = GetComponent<FreeUnitChrysalisMovementStop>();           
        }
        public void DeactivateChrysalis(int chrysalisIndex)
        {
            if (_parentUnitManager.ChildChrysaliiUnitManagers.Count > chrysalisIndex) 
            {

                FreeChrysalisStoresHealth freeChrysalisStoresHealth = _parentUnitManager.GetComponent<FreeChrysalisStoresHealth>();
                if (freeChrysalisStoresHealth != null)
                {
                    freeChrysalisStoresHealth.SetStoredHealth(_parentHealth.CurrentHealth);
                }
                if (_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject != null)
                {
                    if (_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject.activeInHierarchy == true)
                    {
                        _parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject.SetActive(false);
                    }
                }
                if (_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject))
                {
                    _parentUnitManager.TheseUnits.ActiveUnits.Remove(_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].gameObject);
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
            _parentHealth.SetMaxHealth(_parentUnitManager.ChildChrysaliiUnitManagers[chrysalisIndex].UnitClass.ChrysalisHPMax);
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

            FreeChrysalisStoresHealth freeChrysalisStoresHealth = _parentUnitManager.GetComponent<FreeChrysalisStoresHealth>();
            if (freeChrysalisStoresHealth != null)
            {
                _parentHealth.SetCurrentHealth(freeChrysalisStoresHealth.StoredHealth);
            }
            _freeUnitChrysalisMovementStop?.StopMovement();
            _targetScoreValue.SetEgg(true);
        }
    }
}
