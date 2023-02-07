using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;
using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class UnitActivation : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private ParentHealth _parentHealth;
        private TargetScoreValue _targetScoreValue;
        private ChrysalisActivation _chrysalisActivation;
        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _parentHealth = GetComponent<ParentHealth>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
        }
        
        public void ActivateUnit(int _unitIndex)
        {
            // Debug.Log($"activating");
            for (int _indexInSubUnits = 0; _indexInSubUnits < _parentUnitManager.SubUnits.Length; _indexInSubUnits++)
            {
                if (_indexInSubUnits == _unitIndex)
                {
                    _parentUnitManager.CurrentSubUnit = _parentUnitManager.SubUnits[_unitIndex];
                    _parentUnitManager.SubUnits[_unitIndex].SetActive(true);
                    _parentHealth.CurrentUnit = _parentUnitManager.SubUnits[_unitIndex].GetComponent<UnitManager>();
                    _parentHealth.SetChrysalis(false);
                    UnitManager _unitManager = _parentUnitManager.SubUnits[_unitIndex].GetComponent<UnitManager>();
                    // int _tier = _unitManager.UnitStats.TierMultiplier.Tier - 1;
                    HatchEffectSO _hatchEffectSO = _parentUnitManager.SubHatchEffects[_unitIndex];
                    _parentUnitManager.SubUnits[_unitIndex].GetComponent<UnitManager>()?.RestartUnit();
                    if (!_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.SubUnits[_indexInSubUnits]))
                    {
                        _parentUnitManager.TheseUnits.ActiveUnits.Add(_parentUnitManager.SubUnits[_unitIndex]);
                    }
                    // if (_hatchEffectSO != null)
                    if (_unitIndex == 1)
                    {
                        _parentUnitManager.CreateHatchEffect(_parentUnitManager.ParentUnitClass.AdvUnitClass.HatchEffectPrefab, _parentUnitManager.ParentIndex, _unitIndex, true);
                    }
                }
                _chrysalisActivation.DeactivateChrysalis(_indexInSubUnits);
            }
            _targetScoreValue.SetCurrentUnit(_parentUnitManager.CurrentSubUnit.GetComponent<Attack>());
        }

        
    }
}
