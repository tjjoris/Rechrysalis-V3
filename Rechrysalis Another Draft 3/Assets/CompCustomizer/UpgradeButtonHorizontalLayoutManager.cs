using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class UpgradeButtonHorizontalLayoutManager : MonoBehaviour
    {
        [SerializeField] private UpgradeButtonManager[] _upgradeButtonManagerArray;
        public UpgradeButtonManager[] UpgradeButtonManagerArray {get {return _upgradeButtonManagerArray;}}
    }
}
