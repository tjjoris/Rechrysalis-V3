using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CompVerticalManager : MonoBehaviour
    {
        private CompUpgradeManager[] _compUpgradeManagers;

        public void Initialize(CompSO compSO, int parentIndex, GameObject compButtonPrefab)
        {
            _compUpgradeManagers = new CompUpgradeManager[3];
            for (int _parentIndex = 0; _parentIndex < 3; _parentIndex ++)
            {
                CreateCompButton(compButtonPrefab);
            }
        }
        private void CreateCompButton(GameObject compButtonPrefab)
        {
            GameObject _compButton = Instantiate(compButtonPrefab, transform);
        }
    }
}
