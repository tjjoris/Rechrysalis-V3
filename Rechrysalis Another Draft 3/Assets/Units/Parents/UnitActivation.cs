using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;
using Rechrysalis.Attacking;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class UnitActivation : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private ParentHealth _parentHealth;
        private TargetScoreValue _targetScoreValue;
        private ChrysalisActivation _chrysalisActivation;
        // private FreeChrysalisStoresHealth _freeChrysalisStoresHealth;
        private ParentUnitHatchEffects _parentUnitHatchEffects;
        private ProgressBarManager _progressBarManager;
        private HilightRingParentManager _hilightRingParentManager;
        public HilightRingParentManager HilightRingParentManager {get { return _hilightRingParentManager;} set {_hilightRingParentManager = value;}}
        [SerializeField] private GameObject _particleEffectPrefab;
        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _parentHealth = GetComponent<ParentHealth>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
            _parentUnitHatchEffects = GetComponent<ParentUnitHatchEffects>();
            _progressBarManager = GetComponent<ProgressBarManager>();
            // _freeChrysalisStoresHealth = GetComponent<FreeChrysalisStoresHealth>();
        }
        
        public void ActivateUnit(int unitIndex)
        {
            // Debug.Log($"activating");
            for (int _indexInSubUnits = 0; _indexInSubUnits < _parentUnitManager.ChildUnitManagers.Count; _indexInSubUnits++)
            {
                if (_indexInSubUnits == unitIndex)
                {
                    _parentUnitManager.CurrentSubUnit = _parentUnitManager.ChildUnitManagers[unitIndex].gameObject;
                    // _parentUnitManager.SubUnits[unitIndex].SetActive(true);
                    _parentUnitManager.ChildUnitManagers[unitIndex].gameObject.SetActive(true);
                    _parentHealth.CurrentUnit = _parentUnitManager.ChildUnitManagers[unitIndex];
                    _parentHealth.SetChrysalis(false);
                    UnitManager _unitManager = _parentUnitManager.ChildUnitManagers[unitIndex];
                    // int _tier = _unitManager.UnitStats.TierMultiplier.Tier - 1;
                    // if ((_parentUnitManager.SubHatchEffects != null) && (_parentUnitManager.SubHatchEffects.Length > unitIndex))
                    // {
                    // HatchEffectSO _hatchEffectSO = _parentUnitManager.SubHatchEffects[unitIndex];
                    // }
                    _parentUnitManager.ChildUnitManagers[unitIndex]?.RestartUnit();
                    if (!_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.ChildUnitManagers[_indexInSubUnits].gameObject))
                    {
                        _parentUnitManager.TheseUnits.ActiveUnits.Add(_parentUnitManager.ChildUnitManagers[unitIndex].gameObject);
                    }
                    // if (_hatchEffectSO != null)
                    if (unitIndex == 1)
                    {
                        if (_parentUnitManager.ParentUnitClass.AdvUnitClass.HatchEffectPrefab.Count > 0)
                        {
                            foreach (HatchEffectClass hatchEffectClass in _parentUnitManager.ParentUnitClass.AdvUnitClass.HatchEffectClasses)
                            {
                                if (hatchEffectClass != null)
                                {
                                    _parentUnitHatchEffects.CreateHatchEffect(hatchEffectClass.HatchEffectPrefab, _parentUnitManager.ParentIndex, unitIndex, true, hatchEffectClass.HatchEffectHealth);
                                }
                            }
                        }
                    }
                    // _progressBarManager?.TintChargeUp();
                }
                _chrysalisActivation.DeactivateChrysalis(_indexInSubUnits);
            }
            Instantiate(_particleEffectPrefab, transform.position, Quaternion.identity, transform.parent);
            _parentHealth.SetMaxHealth(_parentUnitManager.ChildUnitManagers[unitIndex].UnitClass.HPMax);
            _targetScoreValue.SetCurrentUnit(_parentUnitManager.CurrentSubUnit.GetComponent<Attack>());
            _hilightRingParentManager?.ActivateUnit(unitIndex);
            _parentUnitManager.CurrentSubUnit?.GetComponent<Attack>()?.ResetUnitAttack();
        }
        public void DeactivateUnit(int _unitIndex)
        {
            if (_parentUnitManager.ChildUnitManagers[_unitIndex].gameObject != null)
            {
                if (_parentUnitManager.ChildUnitManagers[_unitIndex].gameObject.activeInHierarchy == true)
                {
                    _parentUnitManager.ChildUnitManagers[_unitIndex].gameObject.SetActive(false);
                }
            }
            if (_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.ChildUnitManagers[_unitIndex].gameObject))
            {
                _parentUnitManager.TheseUnits.ActiveUnits.Remove(_parentUnitManager.ChildUnitManagers[_unitIndex].gameObject);
            }
        }        
    }
}
