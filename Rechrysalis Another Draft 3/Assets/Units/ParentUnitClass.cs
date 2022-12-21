using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    public class ParentUnitClass
    {
        [SerializeField] private List<UpgradeTypeClass> _advancedUpgradesUTCList;
        public List<UpgradeTypeClass> AdvancedUpgradesUTCList { get{ return _advancedUpgradesUTCList; } set{ _advancedUpgradesUTCList = value; } }
        [SerializeField] private UpgradeTypeClass _utcBasicUnit;
        public UpgradeTypeClass UTCBasicUnit { get{ return _utcBasicUnit; } set{ _utcBasicUnit = value; } }
        [SerializeField] private UpgradeTypeClass _utcHatchEffect;
        public UpgradeTypeClass UTCHatchEffect { get{ return _utcHatchEffect; } set{ _utcHatchEffect = value; } }
        private UpgradeTypeClass _replacedUTCBasicUnit;
        private UpgradeTypeClass _replaceUTCHatchEffect;
        [SerializeField] private float _manaCost;
        public float ManaCost => _manaCost;
        [SerializeField] private float _hpMaxBasic;
        public float HPMaxBasic => _hpMaxBasic;
        [SerializeField] private float _hpMaxAdvanced;
        public float HPMaxAdvanced => _hpMaxAdvanced;
        [SerializeField] private float _buildTimeBasic;
        public float BuildTimeBasic => _buildTimeBasic;
        [SerializeField] private float _buildTimeAdv;
        public float BuildTimeAdv => _buildTimeAdv;
        private float _rangeBasic;
        public float RangeBasic => _rangeBasic;
        [SerializeField] private float _rangeAdv;
        public float RangeAdv => _rangeAdv;
        [SerializeField] private float _dpsBasic;
        public float DPSBasic => _dpsBasic;
        [SerializeField] private float _dpsAdv;
        public float DPSAdv => _dpsAdv;
        [SerializeField] private float _attackChargeUpBasic;
        public float AttackChargeUpBasic => _attackChargeUpBasic;
        [SerializeField] private float _attackChargeUpAdv;
        public float AttackChargeUpAdv => _attackChargeUpAdv;
        [SerializeField] private float _attackWindDownBasic;
        public float AttackWindDownBasic => _attackWindDownBasic; 
        [SerializeField] private float _attackWindDownAdv;
        public float AttackWindDownAdv => _attackWindDownAdv;
        [SerializeField] private float _damageBasic;
        public float DamamgeBasic => _damageBasic;
        [SerializeField] private float _damageAdv;
        public float DamageAdv => _damageAdv;
        [SerializeField] private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab => _hatchEffectPrefab;
        [SerializeField] private float _hatchEffectMult;
        public float HatchEffectMult => _hatchEffectMult;
        
        public void ClearAllUpgrades()
        {
            _advancedUpgradesUTCList = new List<UpgradeTypeClass>();
            _advancedUpgradesUTCList.Clear();
            _utcBasicUnit = null;
            _utcHatchEffect = null;
            SetBasicStats();
        }
        public void SetUTCBasicUnit(UpgradeTypeClass utcBasicUnit)
        {
            if (utcBasicUnit != null)
            {
                if (_utcBasicUnit != null)
                {
                    _replacedUTCBasicUnit = _utcBasicUnit;
                }   
                _utcBasicUnit = utcBasicUnit;
            }
            SetBasicStats();
        }
        public UpgradeTypeClass GetReplacedUTCBasicUnit()
        {
            return _replacedUTCBasicUnit;
        }
        public void SetUTCReplacedBacsicUnitToNull()
        {
            _replacedUTCBasicUnit = null;
            SetBasicStats();
        }
        public void SetUTCHatchEffect(UpgradeTypeClass utcHatchEffect)
        {
            if (utcHatchEffect != null)
            {
                if (_utcHatchEffect != null)
                {
                    _replaceUTCHatchEffect = _utcHatchEffect;
                }
                _utcHatchEffect = utcHatchEffect;
                SetBasicStats();
            }
        }
        public UpgradeTypeClass GetReplacedUTCHatchEffect()
        {
            return _replaceUTCHatchEffect;
        }
        public void SetUTCReplacedHatchEffectToNull()
        {
            _replaceUTCHatchEffect = null;
            SetBasicStats();
        }
        public void AddUTCAdvanced(UpgradeTypeClass advancedToAdd)
        {
            if (advancedToAdd != null)
            {
                _advancedUpgradesUTCList.Add(advancedToAdd);
            }
            SetBasicStats();
        }
        public void RemoveUTCAdvanced(UpgradeTypeClass advancedToRemove)
        {
            if (advancedToRemove != null)
            {
                if (_advancedUpgradesUTCList.Contains(advancedToRemove))
                {                    
                    _advancedUpgradesUTCList.Remove(advancedToRemove);
                }
            }
            SetBasicStats();
        }
        private void OnValidate()
        {
            if (_utcBasicUnit != null)
            SetBasicStats();
            SetAdvStats();
            SetAdvWhenAdvUpgrades();
            CalculateAdvDamage();
        }
        public void SetBasicStats()
        {
            Debug.Log($"set stats");
            // if (_utcBasicUnit != null)
            {
                _manaCost = _utcBasicUnit.GetUnitStatsSO().Mana;
                _hpMaxBasic = _utcBasicUnit.GetUnitStatsSO().HealthMaxBasic;
                _buildTimeBasic = _utcBasicUnit.GetUnitStatsSO().BuildTimeBasic;
                _rangeBasic = _utcBasicUnit.GetUnitStatsSO().BaseRangeBasic;
                _dpsBasic = _utcBasicUnit.GetUnitStatsSO().BaseDPSBasic;
                _attackChargeUpBasic = _utcBasicUnit.GetUnitStatsSO().AttackChargeUpBasic;
                _attackWindDownBasic = _utcBasicUnit.GetUnitStatsSO().AttackWindDownBasic;
                _damageBasic = _dpsBasic / (_attackChargeUpBasic + _attackWindDownBasic);
            }
        }
        private void SetAdvStats()
        {
            if (_utcBasicUnit.GetAdvUnitModifierSO() != null)
            {
                _manaCost *= _utcBasicUnit.GetAdvUnitModifierSO().ManaMult;
                _manaCost += _utcBasicUnit.GetAdvUnitModifierSO().ManaAdd;
                _hpMaxAdvanced = _hpMaxBasic * _utcBasicUnit.GetAdvUnitModifierSO().HPMaxMult;
                _hpMaxAdvanced += _utcBasicUnit.GetAdvUnitModifierSO().HPMaxAdd;
                _buildTimeAdv = _buildTimeBasic * _utcBasicUnit.GetAdvUnitModifierSO().BuildTimeMult;
                _buildTimeAdv += _utcBasicUnit.GetAdvUnitModifierSO().BuildTimeAdd;
                _rangeAdv = _rangeBasic + _utcBasicUnit.GetAdvUnitModifierSO().RangeAdd;
                _dpsAdv = _dpsBasic * _utcBasicUnit.GetAdvUnitModifierSO().DPSMult;
                _dpsAdv += _utcBasicUnit.GetAdvUnitModifierSO().DPSAdd;
                _attackChargeUpAdv = _attackChargeUpBasic * _utcBasicUnit.GetAdvUnitModifierSO().AttackChargeUpMult;
                _attackChargeUpAdv += _utcBasicUnit.GetAdvUnitModifierSO().AttackChargeUpAdd;
                _attackWindDownAdv = _attackWindDownBasic * _utcBasicUnit.GetAdvUnitModifierSO().AttackWindDownMult;
                _attackWindDownAdv += _utcBasicUnit.GetAdvUnitModifierSO().AttackWindDownAdd;
                _hatchEffectMult = _utcBasicUnit.GetAdvUnitModifierSO().HatchEffectMultiplierAdd;
                // _damageAdv = _dpsAdv / (_attackChargeUpAdv + _attackWindDownAdv);

            }
        }
        private void SetAdvWhenAdvUpgrades()
        {
            if (_advancedUpgradesUTCList.Count > 0)
            {
                for (int i=0; i< _advancedUpgradesUTCList.Count; i++)
                {
                    if (_advancedUpgradesUTCList[i] != null)
                    {
                        if (_advancedUpgradesUTCList[i].GetAdvUnitModifierSO() != null)
                        {
                            SetStatsForThisAdvUpgrade(_advancedUpgradesUTCList[i].GetAdvUnitModifierSO());
                        }
                    }
                }
            }
        }
        private void SetStatsForThisAdvUpgrade(AdvUnitModifierSO advUnitModifierSO)
        {
            // _manaCost *= _utcBasicUnit.GetAdvUnitModifierSO().ManaMult;
            _manaCost += advUnitModifierSO.ManaAdd;
            // _hpMaxAdvanced = _hpMaxBasic * _utcBasicUnit.GetAdvUnitModifierSO().HPMaxMult;
            _hpMaxAdvanced += advUnitModifierSO.HPMaxAdd;
            // _buildTimeAdv = _buildTimeBasic * _utcBasicUnit.GetAdvUnitModifierSO().BuildTimeMult;
            _buildTimeAdv += advUnitModifierSO.BuildTimeAdd;
            _rangeAdv = _rangeAdv + advUnitModifierSO.RangeAdd;
            // _dpsAdv = _dpsBasic * _utcBasicUnit.GetAdvUnitModifierSO().DPSMult;
            _dpsAdv += advUnitModifierSO.DPSAdd;
            // _attackChargeUpAdv = _attackChargeUpBasic * _utcBasicUnit.GetAdvUnitModifierSO().AttackChargeUpMult;
            _attackChargeUpAdv += advUnitModifierSO.AttackChargeUpAdd;
            // _attackWindDownAdv = _attackWindDownBasic * _utcBasicUnit.GetAdvUnitModifierSO().AttackWindDownMult;
            _attackWindDownAdv += advUnitModifierSO.AttackWindDownAdd;
            _hatchEffectMult += advUnitModifierSO.HatchEffectMultiplierAdd;
            // _damageAdv = _dpsAdv / (_attackChargeUpAdv + _attackWindDownAdv);
        }
        private void CalculateAdvDamage()
        {
            _damageAdv = _dpsAdv / (_attackChargeUpAdv + _attackWindDownAdv);
        }
        private void SetHatchEffect()
        {
            if (_utcHatchEffect != null)
            {
                _hatchEffectPrefab = _utcHatchEffect.GetHatchEffectSO().HatchEffectPrefab;
                
            }
        }
    }
}
