using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using Rechrysalis.CompCustomizer;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    public class UpgradeTypeClass
        {
            public enum UpgradeType {Error, Basic, Advanced, HatchEffect, SingleHeart, TrippleHeart};
            [SerializeField] private UpgradeType _upgradeType;
            public UpgradeType UnitTypeVar { get { return _upgradeType; } set { _upgradeType = value; } }
            [SerializeField] private UnitStatsSO _unitStatsSO;
            [SerializeField] private HatchEffectSO _hatchEffectSO;
            [SerializeField] private AdvUnitModifierSO _advUnitModifierSO;
            [SerializeField] private ControllerHeartUpgrade _controllerHeartUpgrade;
            [SerializeField] private HatchEffectManager _hatchEffectManager;
            public HatchEffectManager HatchEffectManager { get => _hatchEffectManager; set => _hatchEffectManager = value;}
            [SerializeField] private GameObject _hatchEffectPrefab;
            public GameObject HatchEffectPrefab => _hatchEffectPrefab;
            [SerializeField] private string _buttonName;
            public string ButtonName => _buttonName;
            
            // private void Awake()
            // {
            // }
            // [ContextMenu("Set Up UTC")] void SetUpUTC()
            // {
            //     SetUpFunc();
            // }
            private void SetUpFunc()
        {
            _hatchEffectManager = _hatchEffectPrefab.GetComponent<HatchEffectManager>();
            }
            public void SetUpgradeType(UpgradeType upgradeType)
            {
                _upgradeType = upgradeType;
            }
            public UpgradeType GetUpgradeType()
            {
                return _upgradeType;
            }
            public void SetUnitStatsSO(UnitStatsSO value)
            {
                _unitStatsSO = value;
            }
            public UnitStatsSO GetUnitStatsSO()
            {
                return _unitStatsSO;
            }
            public void SetHatchEffectSO(HatchEffectSO value)
            {
                _hatchEffectSO = value;                
            }
            public HatchEffectSO GetHatchEffectSO()
            {
                return _hatchEffectSO;
            }
            public void SetControllerHeartUpgrade(ControllerHeartUpgrade value)
            {
                _controllerHeartUpgrade = value;
            }
            public ControllerHeartUpgrade GetControllerHeartUpgrade()
            {
                return _controllerHeartUpgrade;
            }
            public void SetAdvUnitModifierSO(AdvUnitModifierSO advUnitModifierSO)
            {
                _advUnitModifierSO = advUnitModifierSO;
            }
            public AdvUnitModifierSO GetAdvUnitModifierSO()
            {
                return _advUnitModifierSO;
            }
        }
}
