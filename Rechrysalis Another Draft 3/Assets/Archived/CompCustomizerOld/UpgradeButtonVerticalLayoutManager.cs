using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizerOld
{
    public class UpgradeButtonVerticalLayoutManager : MonoBehaviour
    {
        [SerializeField]private GameObject _upgradeButtonHorizontalLayoutPrefab;
        private UpgradeButtonHorizontalLayoutManager[] _upgradeButtonHorizontalLayoutManagerArray;

        // public void Initialize(CompCustomizerSO _compCustomizerSO, int _upgradeCount)
        // {
        //     for (int _upgradeIndex = 0; _upgradeIndex < _upgradeCount; _upgradeIndex ++)
        //     {
        //         GameObject go = Instantiate(_upgradeButtonHorizontalLayoutPrefab, transform);
        //     }
        // }

    }
}
