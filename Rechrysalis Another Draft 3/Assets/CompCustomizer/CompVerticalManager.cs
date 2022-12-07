using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class CompVerticalManager : MonoBehaviour
    {
        [SerializeField] private CompUpgradeManager[] _compUpgradeManagers;
        private UpgradeButtonDisplay[] _upgradeButtonDisplays;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;

        public void Initialize(CompSO compSO, int parentIndex, GameObject compButtonPrefab)
        {
            _upgradeButtonDisplays = new UpgradeButtonDisplay[3];
            _compUpgradeManagers = new CompUpgradeManager[3];
            for (int i=0; i<3; i++)
            {
                int upgradeIndexInArray = ((parentIndex * compSO.ChildUnitCount) + i);
                CreateAndSetupCompButton(compSO, compButtonPrefab, parentIndex, i);
                if (i == 2)//its a hatch effect
                _compUpgradeManagers[i].SetUpgradeType(compSO.HatchEffectSOArray[upgradeIndexInArray].UpgradeTypeClass);
                else 
                _compUpgradeManagers[i].SetUpgradeType(compSO.UnitSOArray[upgradeIndexInArray].UpgradeTypeClass);
            }
            SubscribeToCompButtons();
        }
        private void SubscribeToCompButtons()
        {
            if (_compUpgradeManagers != null)
            {
                for (int _index=0; _index<_compUpgradeManagers.Length; _index++)
                {
                    if (_compUpgradeManagers[_index] != null)
                    {
                        _compUpgradeManagers[_index]._onCompUpgradeClicked -= CompUpgradeClicked;
                        _compUpgradeManagers[_index]._onCompUpgradeClicked += CompUpgradeClicked;
                    }
                }
            }
        }
        private void OnEnable()
        {
            SubscribeToCompButtons();
        }
        private void OnDisable()
        {
            if (_compUpgradeManagers != null)
            {
                for (int _index = 0; _index < _compUpgradeManagers.Length; _index++)
                {
                    if (_compUpgradeManagers[_index] != null)
                    {
                        _compUpgradeManagers[_index]._onCompUpgradeClicked -= CompUpgradeClicked;
                    }
                }
            }
        }
        private void CompUpgradeClicked(CompUpgradeManager compUpgradeManager)
        {
            _onCompUpgradeClicked?.Invoke(compUpgradeManager);
        }
        private void CreateAndSetupCompButton(CompSO compSO, GameObject compButtonPrefab, int parentIndex, int childIndex)
        {
            CreateCompButton(compSO, compButtonPrefab, parentIndex, childIndex);
            SetUpButtonDisplayUnit(compSO, parentIndex, childIndex);
            _compUpgradeManagers[childIndex].Initialize(parentIndex, childIndex);
        }
        public void LoopChildrenAndSetDisplay(CompSO compSO, int parentIndex)
        {
            for (int childIndex = 0; childIndex < _upgradeButtonDisplays.Length; childIndex++)
            {
                // SetCompUpgradeDisplay(childIndex, )
            }
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
        public void SetCompUpgradeDisplay(int childIndex, UpgradeTypeClass upgradeTypeClass)
        {
            if ((upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic) || (upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced))            
            {
                _upgradeButtonDisplays[childIndex]?.DisplayForUnit(upgradeTypeClass.GetUnitStatsSO());
            }
            if (upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
            {
                _upgradeButtonDisplays[childIndex]?.DisplayForHatchEffect(upgradeTypeClass.GetHatchEffectSO());
            }
        }
    }
}
