using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CompVerticalManager : MonoBehaviour//, IDropHandler
    {
        bool debugBool = true;
        [SerializeField] private CompUpgradeManager[] _compUpgradeManagers;
        private UpgradeButtonDisplay[] _upgradeButtonDisplays;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;
        [SerializeField] private VerticalContainer _verticalContainer;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private ParentUnitClass _parentUnitClass;
        // public ParentUnitClass ParentUnitClass { get{ return _parentUnitClass; } set{ _parentUnitClass = value; } }
        

        public void Initialize()
        {

            if (debugBool)
                Debug.Log($"initialize vertical");
            // if (parentUnitClass != null)
                // _parentUnitClass = parentUnitClass;
            _upgradeButtonDisplays = new UpgradeButtonDisplay[3];
            _compUpgradeManagers = new CompUpgradeManager[3];
            _scrollRect = GetComponent<ScrollRect>();
        }
        public void CreateAndSetUpCompButtons(CompSO compSO, int parentIndex, GameObject compButtonPrefab, Transform movingButtonHolder, ParentUnitClass parentUnitClass)
        {
            if (parentUnitClass != null)
            _parentUnitClass = parentUnitClass;
            for (int i=0; i<3; i++)
            {
                int upgradeIndexInArray = ((parentIndex * compSO.ChildUnitCount) + i);
                CreateAndSetupCompButton(compSO, compButtonPrefab, parentIndex, i, movingButtonHolder);
                if (i == 2)//its a hatch effect
                _compUpgradeManagers[i].SetUpgradeTypeClass(compSO.HatchEffectSOArray[upgradeIndexInArray].UpgradeTypeClass);
                else 
                _compUpgradeManagers[i].SetUpgradeTypeClass(compSO.UnitSOArray[upgradeIndexInArray].UpgradeTypeClass);
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
            _verticalContainer._buttonDropped += ButtonDroppedIntoVertical;     
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
            _verticalContainer._buttonDropped -= ButtonDroppedIntoVertical;
        }
        private void CompUpgradeClicked(CompUpgradeManager compUpgradeManager)
        {
            _onCompUpgradeClicked?.Invoke(compUpgradeManager);
        }
        private void DisableScrollRect()
        {
            _scrollRect.enabled = false;
        }
        public void EnableScrollRect()
            {
                _scrollRect.enabled = true;
            }
        private void CreateAndSetupCompButton(CompSO compSO, GameObject compButtonPrefab, int parentIndex, int childIndex, Transform movingButtonHolder)
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

        // public void OnDrop(PointerEventData eventData)
        // {
        //     // Debug.Log($"ondrop called");
        //     // GameObject dropped = eventData.pointerDrag;
        //     // CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
        //     // compUpgradeManager.ParentAfterDrag = transform;
        // }
        private void ButtonDroppedIntoVertical(CompUpgradeManager compUpgradeManager)
        {
            if ((compUpgradeManager != null))
            {
                if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
                {
                    _parentUnitClass.SetUTCBasicUnit(compUpgradeManager.GetUpgradeTypeClass());
                }
                if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
                {
                    _parentUnitClass.SetUTCHatchEffect(compUpgradeManager.GetUpgradeTypeClass());
                }
                if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced)
                {
                    _parentUnitClass.AddUTCAdvanced(compUpgradeManager.GetUpgradeTypeClass());
                }
            }
        }
    }
}
