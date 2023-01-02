using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

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
