using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class CompUpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradeTypeClass _upgradeType;
        public void Initialize()
        {
            _upgradeType = new UpgradeTypeClass();
        }

        public void SetUpgradeType(UpgradeTypeClass.UpgradeType upgradeType)
        {
            _upgradeType.SetUpgradeType(upgradeType);
        }
        public UpgradeTypeClass.UpgradeType GetUpgradeTypeClass()
        {
            return _upgradeType.GetUpgradeType();
        }
    }
}
