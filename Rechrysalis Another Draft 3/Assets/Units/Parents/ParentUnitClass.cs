using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    public class ParentUnitClass
    {
        private bool _debugBool = false;
        private CompsAndUnitsSO _compsAndUnitsSO;
        [SerializeField] private List<UpgradeTypeClass> _advancedUpgradesUTCList;
        public List<UpgradeTypeClass> AdvancedUpgradesUTCList { get{ return _advancedUpgradesUTCList; } set{ _advancedUpgradesUTCList = value; } }
        [SerializeField] private UpgradeTypeClass _utcBasicUnit;
        public UpgradeTypeClass UTCBasicUnit { get{ return _utcBasicUnit; } set{ _utcBasicUnit = value; } }
        [SerializeField] private List<GameObject> _hatchEffectPrefabs;
        public List<GameObject> HatchEffectPrefabs => _hatchEffectPrefabs;
        [SerializeField] private List<UpgradeTypeClass> _utcHatchEffect = new List<UpgradeTypeClass>();
        public List<UpgradeTypeClass> UTCHatchEffects { get{ return _utcHatchEffect; } set{ _utcHatchEffect = value; } }
        private UpgradeTypeClass _replacedUTCBasicUnit;
        // private UpgradeTypeClass _replaceUTCHatchEffect;
        [SerializeField] private UnitClass _basicUnitClass;
        public UnitClass BasicUnitClass => _basicUnitClass;
        [SerializeField] private UnitClass _advUnitClass;
        public UnitClass AdvUnitClass => _advUnitClass;
        
        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
        }
        public void ClearAllUpgrades()
        {
            _advancedUpgradesUTCList = new List<UpgradeTypeClass>();
            _advancedUpgradesUTCList.Clear();
            _hatchEffectPrefabs = new List<GameObject>();
            ClearUTCHEs();
            _utcBasicUnit = null;
            if (_debugBool)
            {
                Debug.Log($"clear all stats");
            }
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
            if (_debugBool)
            Debug.Log($"SET BASIC UNIT");
        }
        public UpgradeTypeClass GetReplacedUTCBasicUnit()
        {
            return _replacedUTCBasicUnit;
        }
        public void SetUTCReplacedBacsicUnitToNull()
        {
            _replacedUTCBasicUnit = null;
            if (_debugBool) Debug.Log($"set replaced basic unit to null");
        }
        public void SetUTCHatchEffect(UpgradeTypeClass utcHatchEffect)
        {
            if (utcHatchEffect != null)
            {
                _utcHatchEffect.Add(utcHatchEffect);
                if (_debugBool) Debug.Log($"set hatch effect");
            }
        }
        public void SetUTCHEsFromGOs()
        {
            ClearUTCHEs();
            foreach(GameObject hatchEffectGO in _hatchEffectPrefabs)
            {
                if (hatchEffectGO == null) continue;
                HatchEffectManager hatchEffectManager = hatchEffectGO.GetComponent<HatchEffectManager>();
                if (hatchEffectManager == null) continue;
                if (hatchEffectManager.UpgradeTypeClass == null) continue;
                _utcHatchEffect.Add(hatchEffectManager.UpgradeTypeClass);
            }
        }
        private void ClearUTCHEs()
        {
            _utcHatchEffect = new List<UpgradeTypeClass>();
        }
        public void AddUTCAdvanced(UpgradeTypeClass advancedToAdd)
        {
            if (advancedToAdd != null)
            {
                _advancedUpgradesUTCList.Add(advancedToAdd);
            }
            if (_debugBool) Debug.Log($"add advanced");
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
            if (_debugBool) Debug.Log($"remove advanced");
        }
        private void OnValidate()
        {
            if (_debugBool) Debug.Log($"validate");
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
                    _advUnitClass?.CalculateDamge();
                }
            }
        }
        public void SetBasicClass()
        {
            if (_debugBool)
            {
                Debug.Log($"set stats" + _utcBasicUnit.GetUnitStatsSO().UnitName);
            }
            {
                UnitStatsMultiplierSO baseMultiplierSO = _utcBasicUnit.GetUnitStatsSO().BaseMultiplier;
                UnitStatsMultiplierSO typeMultipler = _utcBasicUnit.GetUnitStatsSO().TypeMultiplier;
                UnitStatsMultiplierSO tierMultiplier = _utcBasicUnit.GetUnitStatsSO().TierMultiplier;
                _basicUnitClass = new UnitClass();
                _basicUnitClass.ManaCost = baseMultiplierSO.ManaMultiplier * typeMultipler.ManaMultiplier * tierMultiplier.ManaMultiplier;
                _basicUnitClass.HPMax = baseMultiplierSO.HealthMultiplier * typeMultipler.HealthMultiplier * tierMultiplier.HealthMultiplier;
                _basicUnitClass.ChrysalisHPMax = baseMultiplierSO.ChrysalisHealthMultiplier * typeMultipler.ChrysalisHealthMultiplier * tierMultiplier.ChrysalisHealthMultiplier;
                _basicUnitClass.BuildTime = baseMultiplierSO.BuildTimeMultiplier * typeMultipler.BuildTimeMultiplier * tierMultiplier.BuildTimeMultiplier;
                _basicUnitClass.Range = baseMultiplierSO.Range * typeMultipler.Range * tierMultiplier.Range;
                _basicUnitClass.DPS = baseMultiplierSO.DPSMultiplier * typeMultipler.DPSMultiplier * tierMultiplier.DPSMultiplier;
                _basicUnitClass.AttackChargeUp = baseMultiplierSO.AttackChargeUp * typeMultipler.AttackChargeUp * tierMultiplier.AttackChargeUp;
                _basicUnitClass.AttackWindDown = baseMultiplierSO.AttackWindDown * typeMultipler.AttackWindDown * tierMultiplier.AttackWindDown;
                _basicUnitClass.ControllerLifeCostMult = baseMultiplierSO.ControllerLifeCostMult * typeMultipler.ControllerLifeCostMult * tierMultiplier.ControllerLifeCostMult * _compsAndUnitsSO.FreeUnitToControllerLifeLostMult;

                _basicUnitClass.UnitSprite = _utcBasicUnit.GetUnitStatsSO().UnitSprite;
                _basicUnitClass.AmountToPool = _utcBasicUnit.GetUnitStatsSO().AmountToPool;
                _basicUnitClass.ProjectileSpeed = baseMultiplierSO.ProjectileSpeed * typeMultipler.ProjectileSpeed;
                _basicUnitClass.ProjectileSprite = _utcBasicUnit.GetUnitStatsSO().ProjectileSprite;
                _basicUnitClass.UnitName = _utcBasicUnit.GetUnitStatsSO().UnitName;
                _basicUnitClass.ChrysalisSprite = _utcBasicUnit.GetUnitStatsSO().ChrysalisSprite;
                _basicUnitClass.CalculateDamge();
            }
        }
        private void SetAdvClass()
        {
            UnitStatsSO basicUnitStats = _utcBasicUnit.GetUnitStatsSO();
            AdvUnitModifierSO advancedModifier = basicUnitStats.AdvUnitModifierSO;            
            if (_utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO != null)//.GetAdvUnitModifierSO() != null)
            {
                if (_debugBool)
                {
                    Debug.Log($"set base adv stats");
                }
                _advUnitClass = new UnitClass();
                _advUnitClass.ManaCost = (_utcBasicUnit.GetUnitStatsSO().TypeMultiplier.ManaMultiplier * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.ManaMult);
                _advUnitClass.ManaCost += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.ManaAdd;
                _advUnitClass.HPMax = _basicUnitClass.HPMax * _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.HPMaxMult;
                _advUnitClass.HPMax += _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.HPMaxAdd;
                _advUnitClass.ChrysalisHPMax = _basicUnitClass.ChrysalisHPMax;
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
                _advUnitClass.ControllerLifeCostMult = _basicUnitClass.ControllerLifeCostMult + advancedModifier.ControllerLifeCostMult;
                _advUnitClass.UnitSprite = _utcBasicUnit.GetUnitStatsSO().UnitSprite;
                _advUnitClass.AmountToPool = _utcBasicUnit.GetUnitStatsSO().AmountToPool;
                _advUnitClass.ProjectileSpeed = _utcBasicUnit.GetUnitStatsSO().ProjectileSpeed;
                _advUnitClass.ProjectileSprite = _utcBasicUnit.GetUnitStatsSO().ProjectileSprite;
                _advUnitClass.UnitName = $"Adv " + _basicUnitClass.UnitName;
                _advUnitClass.ChrysalisSprite = _utcBasicUnit.GetUnitStatsSO().ChrysalisSprite;
                _advUnitClass.SacrificeControllerAmount = _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.SacrificeControllerAmount;
                _advUnitClass.MoveSpeedAdd = _utcBasicUnit.GetUnitStatsSO().AdvUnitModifierSO.MoveSpeedAdd;
                _advUnitClass.HatchBuildTimeMaxBaseAdd = basicUnitStats.AdvUnitModifierSO.HatchBuildTimeMaxBaseAdd;
                _advUnitClass.SiegeDuration = advancedModifier.SiegeDuration;
                _advUnitClass.BurstHeal = advancedModifier.BurstHeal;

            }
        }
        private void SetAdvWhenAdvUpgrades()
        {
            if (_debugBool) Debug.Log($"_advUpgradesUTCList.Count " + _advancedUpgradesUTCList.Count);
            if (_advancedUpgradesUTCList.Count > 0)
            {
                
                for (int i=0; i< _advancedUpgradesUTCList.Count; i++)
                {
                    if (_advancedUpgradesUTCList[i] != null)
                    {
                        if (_advancedUpgradesUTCList[i].GetAdvUnitModifierSO() != null)
                        {
                            if (_debugBool)
                            Debug.Log($"set adv upgrade stats for " + i);
                            SetStatsForThisAdvUpgrade(_advancedUpgradesUTCList[i].GetAdvUnitModifierSO());
                        }
                    }
                }
            }
        }
        private void SetStatsForThisAdvUpgrade(AdvUnitModifierSO advUnitModifierSO)
        {
            _advUnitClass.ManaCost += advUnitModifierSO.ManaAdd;
            _advUnitClass.HPMax += advUnitModifierSO.HPMaxAdd;
            _advUnitClass.BuildTime += advUnitModifierSO.BuildTimeAdd;
            _advUnitClass.Range = _advUnitClass.Range + advUnitModifierSO.RangeAdd;
            if (_debugBool) Debug.Log($"increase range " + advUnitModifierSO.RangeAdd);
            _advUnitClass.DPS += advUnitModifierSO.DPSAdd;
            _advUnitClass.AttackChargeUp += advUnitModifierSO.AttackChargeUpAdd;
            _advUnitClass.AttackWindDown += advUnitModifierSO.AttackWindDownAdd;
            _advUnitClass.HatchEffectMult += advUnitModifierSO.HatchEffectMultiplierAdd;
            _advUnitClass.HatchEffectDurationAdd += advUnitModifierSO.HatchEffectDurationAdd;
            _advUnitClass.SacrificeControllerAmount += advUnitModifierSO.SacrificeControllerAmount;
            _advUnitClass.MoveSpeedAdd += advUnitModifierSO.MoveSpeedAdd;
            _advUnitClass.HatchBuildTimeMaxBaseAdd += advUnitModifierSO.HatchBuildTimeMaxBaseAdd;
            _advUnitClass.SiegeDuration += advUnitModifierSO.SiegeDuration;
            _advUnitClass.BurstHeal += advUnitModifierSO.BurstHeal;
        }
        private void CheckToSetHatchEffect()
        {
            if (_utcHatchEffect == null) return;
            foreach (UpgradeTypeClass upgradeTypeClassHE in _utcHatchEffect)
            {
                if (upgradeTypeClassHE == null) continue;
                HatchEffectClass duplicateHatchEffectClass = CheckIfDuplicateHatchEffect(upgradeTypeClassHE);
                // if (duplicateHatchEffectClass != null)
                // {
                //     duplicateHatchEffectClass.HatchEffectHealth += upgradeTypeClassHE.HatchEffectManager.HEHealthMax;
                // }
                // else 
                {                                         
                    HatchEffectClass hatchEffectClass = new HatchEffectClass();
                    hatchEffectClass.HatchEffectPrefab = upgradeTypeClassHE.HatchEffectPrefab;
                    // hatchEffectClass.HatchEffectHealth = upgradeTypeClassHE.HatchEffectManager.HEHealthMax;
                    _advUnitClass.HatchEffectClasses.Add(hatchEffectClass);
                    _advUnitClass.HatchEffectPrefab.Add(upgradeTypeClassHE.HatchEffectPrefab);
                    if (upgradeTypeClassHE.HatchEffectManager == null) Debug.LogWarning("upgradeTypeClassHE.HatchEffectmanager = null");
                    _advUnitClass.ManaCost += upgradeTypeClassHE.HatchEffectManager.ManaCostIncrease;
                    _advUnitClass.BuildTime += upgradeTypeClassHE.HatchEffectManager.BuildTimeIncrease;
                }
            }
        }
        private HatchEffectClass CheckIfDuplicateHatchEffect(UpgradeTypeClass hatchEffect)
        {
            if ((_advUnitClass.HatchEffectClasses == null) || (_advUnitClass.HatchEffectPrefab.Count == 0)) return null;
            foreach (HatchEffectClass hatchEffectClass in _advUnitClass.HatchEffectClasses)
            {
                if (hatchEffectClass != null)
                {
                    if (hatchEffectClass.HatchEffectPrefab.GetComponent<HatchEffectManager>().UpgradeTypeClass == hatchEffect)
                    {
                        return hatchEffectClass;
                    }
                }
            }
            return null;
        }
    }
}
