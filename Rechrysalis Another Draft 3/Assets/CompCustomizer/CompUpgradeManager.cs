using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class CompUpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradeTypeClass.UpgradeType _upgradeType = UpgradeTypeClass.UpgradeType.Error;

        public void SetUpgradeType(UpgradeTypeClass.UpgradeType upgradeType)
        {
            _upgradeType = upgradeType;
        }
        public UpgradeTypeClass.UpgradeType GetUpgradeTypeClass()
        {
            return _upgradeType;
        }
    }
}
