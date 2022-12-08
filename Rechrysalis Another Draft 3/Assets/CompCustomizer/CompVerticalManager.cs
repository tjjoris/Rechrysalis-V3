using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;
using UnityEngine.UI;

namespace Rechrysalis.CompCustomizer
{
    public class CompVerticalManager : MonoBehaviour
    {
        [SerializeField] private CompUpgradeManager[] _compUpgradeManagers;
        private UpgradeButtonDisplay[] _upgradeButtonDisplays;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;
        [SerializeField] private GameObject _verticalContainer;
        [SerializeField] private ScrollRect _scrollRect;

        public void Initialize(CompSO compSO, int parentIndex, GameObject compButtonPrefab, GameObject movingButtonHolder)
        {
            _upgradeButtonDisplays = new UpgradeButtonDisplay[3];
            _compUpgradeManagers = new CompUpgradeManager[3];
            _scrollRect = GetComponent<ScrollRect>();
            for (int i=0; i<3; i++)
            {
                int upgradeIndexInArray = ((parentIndex * compSO.ChildUnitCount) + i);
                CreateAndSetupCompButton(compSO, compButtonPrefab, parentIndex, i, movingButtonHolder);
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
                        _compUpgradeManagers[_index]._disableVerticalScroll -= DisableScrollRect;
                        _compUpgradeManagers[_index]._disableVerticalScroll += DisableScrollRect;
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
                        _compUpgradeManagers[_index]._disableVerticalScroll -= DisableScrollRect;
                    }
                }
            }
        }
        private void CompUpgradeClicked(CompUpgradeManager compUpgradeManager)
        {
            _onCompUpgradeClicked?.Invoke(compUpgradeManager);
        }
        private void DisableScrollRect()
        {
            _scrollRect.enabled = false;
        }
        private void CreateAndSetupCompButton(CompSO compSO, GameObject compButtonPrefab, int parentIndex, int childIndex, GameObject movingButtonHolder)
        {
            CreateCompButton(compSO, compButtonPrefab, parentIndex, childIndex);
            SetUpButtonDisplayUnit(compSO, parentIndex, childIndex);
            _compUpgradeManagers[childIndex].Initialize(parentIndex, childIndex, movingButtonHolder);
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
            GameObject _compButton = Instantiate(compButtonPrefab, _verticalContainer.transform);
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
