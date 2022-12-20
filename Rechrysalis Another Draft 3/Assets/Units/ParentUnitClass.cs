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
        [SerializeField] private float _hpMax;
        public float HPMax => _hpMax;
        [SerializeField] private float _buildTime;
        public float BuildTime => _buildTime;
        [SerializeField] private float _range;
        public float Range => _range;
        [SerializeField] private float _dps;
        public float DPS => _dps;
        [SerializeField] private float _attackChargeUp;
        public float AttackChargeUp => _attackChargeUp;
        [SerializeField] private float _attackWindDown;
        public float AttackWindDown => _attackWindDown; 
        [SerializeField] private float _damage;
        public float Damamge => _damage;
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
        }
        public UpgradeTypeClass GetReplacedUTCBasicUnit()
        {
            return _replacedUTCBasicUnit;
        }
        public void SetUTCReplacedBacsicUnitToNull()
        {
            _replacedUTCBasicUnit = null;
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
            }
        }
        public UpgradeTypeClass GetReplacedUTCHatchEffect()
        {
            return _replaceUTCHatchEffect;
        }
        public void SetUTCReplacedHatchEffectToNull()
        {
            _replaceUTCHatchEffect = null;
        }
        public void AddUTCAdvanced(UpgradeTypeClass advancedToAdd)
        {
            if (advancedToAdd != null)
            {
                _advancedUpgradesUTCList.Add(advancedToAdd);
            }
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
        }
    }
}
