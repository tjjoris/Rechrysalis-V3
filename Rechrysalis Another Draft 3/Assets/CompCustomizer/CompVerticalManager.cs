using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CompVerticalManager : MonoBehaviour
    {
        [SerializeField] private CompUpgradeManager[] _compUpgradeManagers;
        private UpgradeButtonDisplay[] _upgradeButtonDisplays;

        public void Initialize(CompSO compSO, int parentIndex, GameObject compButtonPrefab)
        {
            _upgradeButtonDisplays = new UpgradeButtonDisplay[3];
            _compUpgradeManagers = new CompUpgradeManager[3];
            // for (int childIndex = 0; childIndex < 3; childIndex ++)
            // {
                CreateCompButton(compSO, compButtonPrefab, parentIndex, 0);
            SetUpButtonDisplayUnit(compSO, parentIndex, 0);
            _compUpgradeManagers[0].SetUpgradeType(UpgradeTypeClass.UpgradeType.Basic);
            CreateCompButton(compSO, compButtonPrefab, parentIndex, 1);
            SetUpButtonDisplayUnit(compSO, parentIndex, 1);
            _compUpgradeManagers[1].SetUpgradeType(UpgradeTypeClass.UpgradeType.Advanced);
            CreateCompButton(compSO, compButtonPrefab, parentIndex, 2);
            SetUpButtonDisplayHatchEffect(compSO, parentIndex, 2);
            _compUpgradeManagers[2].SetUpgradeType(UpgradeTypeClass.UpgradeType.HatchEffect);
            // }
        }
        private void CreateCompButton(CompSO compSO, GameObject compButtonPrefab, int parentIndex, int childIndex)
        {
            GameObject _compButton = Instantiate(compButtonPrefab, transform);
            _upgradeButtonDisplays[childIndex] = _compButton.GetComponent<UpgradeButtonDisplay>();
            _compUpgradeManagers[childIndex] = _compButton.GetComponent<CompUpgradeManager>();
        }
        private void SetUpButtonDisplayUnit(CompSO compSO, int parentIndex, int childIndex)
        {
            if (compSO.UnitSOArray[(parentIndex * compSO.ChildUnitCount) + childIndex] != null)
            {
                _upgradeButtonDisplays[childIndex].DisplayForUnit(compSO.UnitSOArray[(parentIndex * compSO.ChildUnitCount) + childIndex]);
            }
        }
        private void SetUpButtonDisplayHatchEffect(CompSO compSO, int parentIndex, int childIndex)
        {
            if (compSO.HatchEffectSOArray[(parentIndex * compSO.ChildUnitCount) + childIndex] != null)
            {
                _upgradeButtonDisplays[childIndex].DisplayForHatchEffect(compSO.HatchEffectSOArray[(parentIndex * compSO.ChildUnitCount) + childIndex]);
            }
        }
    }
}
