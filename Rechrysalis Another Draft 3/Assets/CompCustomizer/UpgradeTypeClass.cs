using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    [System.Serializable]
    public class UpgradeTypeClass
        {
            public enum UpgradeType { Basic, Advanced, HatchEffect, Error };
            [SerializeField] private UpgradeType _upgradeType;
            public UpgradeType UnitTypeVar { get { return _upgradeType; } set { _upgradeType = value; } }

            public void SetUpgradeType(UpgradeType upgradeType)
            {
                _upgradeType = upgradeType;
            }
            public UpgradeType GetUpgradeType()
            {
                return _upgradeType;
            }
        }
}
