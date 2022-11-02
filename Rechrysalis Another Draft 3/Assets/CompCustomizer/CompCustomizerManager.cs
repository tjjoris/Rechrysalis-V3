using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class CompCustomizerManager : MonoBehaviour
    {
        [SerializeField] GameObject _upgradeButtonPrefab;
        private int _numberOfUpgradesToChoose = 2;
        [SerializeField] GameObject _upgradeButtonHorizontalLayoutGroup;
        
        public void Initialize()
        {
            for (int _numberOfUpgradesCount = 0; _numberOfUpgradesCount < _numberOfUpgradesToChoose; _numberOfUpgradesCount ++)
            {
                GameObject go = Instantiate (_upgradeButtonHorizontalLayoutGroup, transform);
            }
        }
    }
}
