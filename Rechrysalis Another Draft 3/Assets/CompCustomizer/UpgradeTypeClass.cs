using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis
{
    [System.Serializable]
    public class UpgradeTypeClass
        {
            public enum UpgradeType { Basic, Advanced, HatchEffect, Error };
            [SerializeField] private UpgradeType _upgradeType;
            public UpgradeType UnitTypeVar { get { return _upgradeType; } set { _upgradeType = value; } }
            [SerializeField] private UnitStatsSO _unitStatsSO;
            [SerializeField] private HatchEffectSO _hatchEffectSO;

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
        }
}
