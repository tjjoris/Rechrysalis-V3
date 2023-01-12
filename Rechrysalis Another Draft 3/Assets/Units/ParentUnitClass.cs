using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    public class ParentUnitClass
    {
        private bool debugBool = true;
        [SerializeField] private List<UpgradeTypeClass> _advancedUpgradesUTCList;
        public List<UpgradeTypeClass> AdvancedUpgradesUTCList { get{ return _advancedUpgradesUTCList; } set{ _advancedUpgradesUTCList = value; } }
        [SerializeField] private UpgradeTypeClass _utcBasicUnit;
        public UpgradeTypeClass UTCBasicUnit { get{ return _utcBasicUnit; } set{ _utcBasicUnit = value; } }
        [SerializeField] private UpgradeTypeClass _utcHatchEffect;
        public UpgradeTypeClass UTCHatchEffect { get{ return _utcHatchEffect; } set{ _utcHatchEffect = value; } }
        private UpgradeTypeClass _replacedUTCBasicUnit;
        private UpgradeTypeClass _replaceUTCHatchEffect;
        [SerializeField] private UnitClass _basicUnitClass;
        public UnitClass BasicUnitClass => _basicUnitClass;
        [SerializeField] private UnitClass _advUnitClass;
        public UnitClass AdvUnitClass => _advUnitClass;
        // [SerializeField] private float _manaCost;
        // public float ManaCost => _manaCost;
        // [SerializeField] private float _hpMaxBasic;
        // public float HPMaxBasic => _hpMaxBasic;
        // [SerializeField] private float _hpMaxAdvanced;
        // public float HPMaxAdvanced => _hpMaxAdvanced;
        // [SerializeField] private float _buildTimeBasic;
        // public float BuildTimeBasic => _buildTimeBasic;
        // [SerializeField] private float _buildTimeAdv;
        // public float BuildTimeAdv => _buildTimeAdv;
        // [SerializeField] private float _rangeBasic;
        // public float RangeBasic => _rangeBasic;
        // [SerializeField] private float _rangeAdv;
        // public float RangeAdv => _rangeAdv;
        // [SerializeField] private float _dpsBasic;
        // public float DPSBasic => _dpsBasic;
        // [SerializeField] private float _dpsAdv;
        // public float DPSAdv => _dpsAdv;
        // [SerializeField] private float _attackChargeUpBasic;
        // public float AttackChargeUpBasic => _attackChargeUpBasic;
        // [SerializeField] private float _attackChargeUpAdv;
        // public float AttackChargeUpAdv => _attackChargeUpAdv;
        // [SerializeField] private float _attackWindDownBasic;
        // public float AttackWindDownBasic => _attackWindDownBasic; 
        // [SerializeField] private float _attackWindDownAdv;
        // public float AttackWindDownAdv => _attackWindDownAdv;
        // [SerializeField] private float _damageBasic;
        // public float DamamgeBasic => _damageBasic;
        // [SerializeField] private float _damageAdv;
        // public float DamageAdv => _damageAdv;
        // [SerializeField] private GameObject _hatchEffectPrefab;
        // public GameObject HatchEffectPrefab => _hatchEffectPrefab;
        // [SerializeField] private float _hatchEffectMult;
        // public float HatchEffectMult => _hatchEffectMult;
        
        public void ClearAllUpgrades()
        {
            _advancedUpgradesUTCList = new List<UpgradeTypeClass>();
            _advancedUpgradesUTCList.Clear();
            _utcBasicUnit = null;
            _utcHatchEffect = null;
            if (debugBool)
            Debug.Log($"clear all stats");
            // SetAllStats();
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
            if (debugBool)
            Debug.Log($"SET BASIC UNIT");
            // SetAllStats();
        }
        public UpgradeTypeClass GetReplacedUTCBasicUnit()
        {
            return _replacedUTCBasicUnit;
        }
        public void SetUTCReplacedBacsicUnitToNull()
        {
            _replacedUTCBasicUnit = null;
            if (debugBool) Debug.Log($"set replaced basic unit to null");
            // SetAllStats();
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
                if (debugBool) Debug.Log($"set hatch effect");
                // SetAllStats();
            }
        }
        public UpgradeTypeClass GetReplacedUTCHatchEffect()
        {
            return _replaceUTCHatchEffect;
        }
        public void SetUTCReplacedHatchEffectToNull()
        {
            _replaceUTCHatchEffect = null;
            if (debugBool) Debug.Log($"replace hatch effect to null");
            // SetAllStats();
        }
        public void AddUTCAdvanced(UpgradeTypeClass advancedToAdd)
        {
            if (advancedToAdd != null)
            {
                _advancedUpgradesUTCList.Add(advancedToAdd);
            }
            if (debugBool) Debug.Log($"add advanced");
            // SetAllStats();
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
            if (debugBool) Debug.Log($"remove advanced");
            // SetAllStats();
        }
        private void OnValidate()
        {
            if (debugBool) Debug.Log($"validate");
            // SetAllStats();
        }
        public void SetAllStats()
        {
            if (_utcBasicUnit != null)
            {
                if (_utcBasicUnit.GetUnitStatsSO() != null)
                {
                    SetBasicClass();
                    SetAdvClass();
                    SetAdvWhenAdvUpgrades();
                    CheckToSetHatchEffect();
                    // CalculateAdvDamage();
                    _advUnitClass?.CalculateDamge();
                }
            }
        }
        public void SetBasicClass()
        {
            Debug.Log($"set stats" + _utcBasicUnit.GetUnitStatsSO().UnitName);
            // if (_utcBasicUnit != null)
            {
                _basicUnitClass = new UnitClass();
                _basicUnitClass.ManaCost = _utcBasicUnit.GetUnitStatsSO().Mana;
                _basicUnitClass.HPMax = _utcBasicUnit.GetUnitStatsSO().HealthMaxBasic;
                _basicUnitClass.BuildTime = _utcBasicUnit.GetUnitStatsSO().BuildTimeBasic;
                _basicUnitClass.Range = _utcBasicUnit.GetUnitStatsSO().BaseRangeBasic;
                _basicUnitClass.DPS = _utcBasicUnit.GetUnitStatsSO().BaseDPSBasic;
                _basicUnitClass.AttackChargeUp = _utcBasicUnit.GetUnitStatsSO().AttackChargeUpBasic;
                _basicUnitClass.AttackWindDown = _utcBasicUnit.GetUnitStatsSO().AttackWindDownBasic;
                _basicUnitClass.UnitSprite = _utcBasicUnit.GetUnitStatsSO().UnitSprite;
                _basicUnitClass.AmountToPool = _utcBasicUnit.GetUnitStatsSO().AmountToPool;
                _basicUnitClass.ProjectileSpeed = _utcBasicUnit.GetUnitStatsSO().ProjectileSpeed;
                _basicUnitClass.ProjectileSprite = _utcBasicUnit.GetUnitStatsSO().ProjectileSprite;
                _basicUnitClass.UnitName = _utcBasicUnit.GetUnitStatsSO().UnitName;
                _basicUnitClass.ChrysalisSprite = _utcBasicUnit.GetUnitStatsSO().ChrysalisSprite;
                _basicUnitClass.CalculateDamge();
            }
        }
        private void SetAdvClass()
        {
            if (_utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO != null)//.GetAdvUnitModifierSO() != null)
            {
                Debug.Log($"set base adv stats");
                _advUnitClass = new UnitClass();
                _advUnitClass.ManaCost = (_utcBasicUnit.GetUnitStatsSO().TypeMultiplier.ManaMultiplier * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.ManaMult);
                _advUnitClass.ManaCost += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.ManaAdd;
                _advUnitClass.HPMax = _basicUnitClass.HPMax * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.HPMaxMult;
                _advUnitClass.HPMax += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.HPMaxAdd;
                _advUnitClass.BuildTime = _basicUnitClass.BuildTime * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.BuildTimeMult;
                _advUnitClass.BuildTime += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.BuildTimeAdd;
                _advUnitClass.Range = _basicUnitClass.Range + _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.RangeAdd;
                _advUnitClass.DPS = _basicUnitClass.DPS * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.DPSMult;
                _advUnitClass.DPS += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.DPSAdd;
                _advUnitClass.AttackChargeUp = _basicUnitClass.AttackChargeUp * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.AttackChargeUpMult;
                _advUnitClass.AttackChargeUp += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.AttackChargeUpAdd;
                _advUnitClass.AttackWindDown = _basicUnitClass.AttackWindDown * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.AttackWindDownMult;
                _advUnitClass.AttackWindDown += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.AttackWindDownAdd;
                _advUnitClass.HatchEffectMult = _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.HatchEffectMultiplierAdd;
                _advUnitClass.UnitSprite = _utcBasicUnit.GetUnitStatsSO().UnitSprite;
                _advUnitClass.AmountToPool = _utcBasicUnit.GetUnitStatsSO().AmountToPool;
                _advUnitClass.ProjectileSpeed = _utcBasicUnit.GetUnitStatsSO().ProjectileSpeed;
                _advUnitClass.ProjectileSprite = _utcBasicUnit.GetUnitStatsSO().ProjectileSprite;
                _advUnitClass.UnitName = $"Adv " + _basicUnitClass.UnitName;
                _advUnitClass.ChrysalisSprite = _utcBasicUnit.GetUnitStatsSO().ChrysalisSprite;
                // _damageAdv = _dpsAdv / (_attackChargeUpAdv + _attackWindDownAdv);

            }
        }
        private void SetAdvWhenAdvUpgrades()
        {
            if (debugBool) Debug.Log($"_advUpgradesUTCList.Count " + _advancedUpgradesUTCList.Count);
            if (_advancedUpgradesUTCList.Count > 0)
            {
                
                for (int i=0; i< _advancedUpgradesUTCList.Count; i++)
                {
                    if (_advancedUpgradesUTCList[i] != null)
                    {
                        if (_advancedUpgradesUTCList[i].GetAdvUnitModifierSO() != null)
                        {
                            if (debugBool)
                            Debug.Log($"set adv upgrade stats for " + i);
                            SetStatsForThisAdvUpgrade(_advancedUpgradesUTCList[i].GetAdvUnitModifierSO());
                        }
                    }
                }
            }
        }
        private void SetStatsForThisAdvUpgrade(AdvUnitModifierSO advUnitModifierSO)
        {
            // _manaCost *= _utcBasicUnit.GetAdvUnitModifierSO().ManaMult;
            _advUnitClass.ManaCost += advUnitModifierSO.ManaAdd;
            // _hpMaxAdvanced = _hpMaxBasic * _utcBasicUnit.GetAdvUnitModifierSO().HPMaxMult;
            _advUnitClass.HPMax += advUnitModifierSO.HPMaxAdd;
            // _buildTimeAdv = _buildTimeBasic * _utcBasicUnit.GetAdvUnitModifierSO().BuildTimeMult;
            _advUnitClass.BuildTime += advUnitModifierSO.BuildTimeAdd;
            _advUnitClass.Range = _advUnitClass.Range + advUnitModifierSO.RangeAdd;
            if (debugBool) Debug.Log($"increase range " + advUnitModifierSO.RangeAdd);
            // _dpsAdv = _dpsBasic * _utcBasicUnit.GetAdvUnitModifierSO().DPSMult;
            _advUnitClass.DPS += advUnitModifierSO.DPSAdd;
            // _attackChargeUpAdv = _attackChargeUpBasic * _utcBasicUnit.GetAdvUnitModifierSO().AttackChargeUpMult;
            _advUnitClass.AttackChargeUp += advUnitModifierSO.AttackChargeUpAdd;
            // _attackWindDownAdv = _attackWindDownBasic * _utcBasicUnit.GetAdvUnitModifierSO().AttackWindDownMult;
            _advUnitClass.AttackWindDown += advUnitModifierSO.AttackWindDownAdd;
            _advUnitClass.HatchEffectMult += advUnitModifierSO.HatchEffectMultiplierAdd;
            // _damageAdv = _dpsAdv / (_attackChargeUpAdv + _attackWindDownAdv);
        }
        // private void CalculateAdvDamage()
        // {
        //     if ((_dpsAdv != 0) && (_attackChargeUpAdv != 0) && (_attackWindDownAdv != 0))
        //     {
        //         _damageAdv = _dpsAdv / (_attackChargeUpAdv + _attackWindDownAdv);
        //     }
        // }
        private void CheckToSetHatchEffect()
        {
            if (_utcHatchEffect != null)
            {
                if (_utcHatchEffect.GetHatchEffectSO() != null)
                {
                    _advUnitClass.HatchEffectPrefab = _utcHatchEffect.GetHatchEffectSO().HatchEffectPrefab;
                    _advUnitClass.ManaCost += _utcHatchEffect.GetHatchEffectSO().AddedManaCost;
                    _advUnitClass.BuildTime += _utcHatchEffect.GetHatchEffectSO().BuildTimeAdd;
                }             
            }
        }
        private void SetHatchEffect()
        {
            if (debugBool)
            Debug.Log($"set hatch effect");
            // if (_utcHatchEffect.GetHatchEffectSO() != null)
            _advUnitClass.HatchEffectPrefab = _utcHatchEffect.GetHatchEffectSO().HatchEffectPrefab;
        }
    }
}
