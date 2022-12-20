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
            SetStats();
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
            SetStats();
        }
        public UpgradeTypeClass GetReplacedUTCBasicUnit()
        {
            return _replacedUTCBasicUnit;
        }
        public void SetUTCReplacedBacsicUnitToNull()
        {
            _replacedUTCBasicUnit = null;
            SetStats();
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
                SetStats();
            }
        }
        public UpgradeTypeClass GetReplacedUTCHatchEffect()
        {
            return _replaceUTCHatchEffect;
        }
        public void SetUTCReplacedHatchEffectToNull()
        {
            _replaceUTCHatchEffect = null;
            SetStats();
        }
        public void AddUTCAdvanced(UpgradeTypeClass advancedToAdd)
        {
            if (advancedToAdd != null)
            {
                _advancedUpgradesUTCList.Add(advancedToAdd);
            }
            SetStats();
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
            SetStats();
        }
        private void OnValidate()
        {
            SetStats();
        }
        public void SetStats()
        {
            Debug.Log($"set stats");
            if (_utcBasicUnit != null)
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
    }
}
