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
        [SerializeField] private List<CompUpgradeManager> _compUpgradeManagers;
        private UpgradeButtonDisplay[] _upgradeButtonDisplays;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;
        [SerializeField] private VerticalContainer _verticalContainer;
        [SerializeField] private ScrollRect _scrollRect;
        private Transform _movingButtonHolder;
        [SerializeField] private ParentUnitClass _parentUnitClass;
        // public ParentUnitClass ParentUnitClass { get{ return _parentUnitClass; } set{ _parentUnitClass = value; } }    
        

        public void Initialize(Transform movingButtonHolder)
        {

            if (debugBool)
                Debug.Log($"initialize vertical");
            // if (parentUnitClass != null)
                // _parentUnitClass = parentUnitClass;
            _upgradeButtonDisplays = new UpgradeButtonDisplay[3];
            // _compUpgradeManagers = new CompUpgradeManager[3];
            _scrollRect = GetComponent<ScrollRect>();
            _movingButtonHolder = movingButtonHolder;
        }
        public void CreateAndSetUpCompButtons(ParentUnitClass parentUnitClass, GameObject compButtonPrefab)
        {
            CreateCompButton(compButtonPrefab, parentUnitClass.UTCBasicUnit);
            CreateCompButton(compButtonPrefab, parentUnitClass.UTCHatchEffect);
            if (parentUnitClass.AdvancedUpgradesUTCList.Count > 0)
            {
                for (int i=0; i<parentUnitClass.AdvancedUpgradesUTCList.Count; i++)
                {
                    CreateCompButton(compButtonPrefab, parentUnitClass.AdvancedUpgradesUTCList[i]);
                }
            }
        }
        public void CreateCompButton(GameObject compButtonPrefab, UpgradeTypeClass upgradeTypeClass)
        {
            if ((upgradeTypeClass != null) && (upgradeTypeClass.GetUpgradeType() != UpgradeTypeClass.UpgradeType.Error))
            {
                // Debug.Log($"creating utc "+ upgradeTypeClass.GetUnitStatsSO().UnitName);
                GameObject compButtonCreated = Instantiate(compButtonPrefab, _verticalContainer.transform);
                CompUpgradeManager compUpgradeManager = compButtonCreated.GetComponent<CompUpgradeManager>();
                compUpgradeManager?.Initialize(_movingButtonHolder);
                compUpgradeManager?.SetUpgradeTypeClass(upgradeTypeClass);
                compUpgradeManager?.SetDisplay(upgradeTypeClass);
            }
        }
        public void CreateAndSetUpCompButtonsOld(CompSO compSO, int parentIndex, GameObject compButtonPrefab, Transform movingButtonHolder, ParentUnitClass parentUnitClass)
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
                for (int _index=0; _index<_compUpgradeManagers.Count; _index++)
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
                for (int _index = 0; _index < _compUpgradeManagers.Count; _index++)
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
            CreateCompButtonOld(compSO, compButtonPrefab, parentIndex, childIndex);
            SetUpButtonDisplayUnit(compSO, parentIndex, childIndex);
            _compUpgradeManagers[childIndex].InitializeOld(parentIndex, childIndex, movingButtonHolder);
        }
        public void LoopChildrenAndSetDisplay(CompSO compSO, int parentIndex)
        {
            for (int childIndex = 0; childIndex < _upgradeButtonDisplays.Length; childIndex++)
            {
                // SetCompUpgradeDisplay(childIndex, )
            }
        }
        private void CreateCompButtonOld(CompSO compSO, GameObject compButtonPrefab, int parentIndex, int childIndex)
        {
            GameObject _compButton = Instantiate(compButtonPrefab, _verticalContainer.transform);
            _upgradeButtonDisplays[childIndex] = _compButton.GetComponent<UpgradeButtonDisplay>();
            _compUpgradeManagers.Add(_compButton.GetComponent<CompUpgradeManager>());
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
                // _compUpgradeManagers.Add(compUpgradeManager);
            }
        }
        public bool CheckIfAtLeastOneBasic()
        {
            foreach (Transform upgradeTransform in _verticalContainer.transform)
            {
                CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager!= null)
                {
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
                    {
                        return true;
                    }
                }            
            }
            return false;
            // if (_compUpgradeManagers != null)
            // {
            //     if (_compUpgradeManagers.Count > 0)
            //     {
            //         for (int i = 0; i<_compUpgradeManagers.Count; i++)
            //         {
            //             if (_compUpgradeManagers[i] != null)
            //             {
            //                 if (_compUpgradeManagers[i].GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
            //                 {
            //                     return true;
            //                 }
            //             }
            //         }
            //     }
            // }
            // return false;
        }
        public bool IsNoErrorsInThisUnitUpgrades()
        {
            int numberOfBasic = 0;
            int numberOfHatchEffects = 0;
            foreach (Transform upgradeTransform in _verticalContainer.transform)
            {
                CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager != null)
                {
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
                    numberOfBasic ++;
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
                    numberOfHatchEffects ++;  
                }                  
                if ((numberOfBasic > 1) || (numberOfHatchEffects > 1))
                return false;
            }                        
            return true;

                    // int numberOfBasic = 0;
                    // int numberOfHatchEffects = 0;
                    // if (_compUpgradeManagers.Count > 0)
                    // {
                    //     for (int i = 0; i< _compUpgradeManagers.Count; i++)
                    //     {
                    //         if (_compUpgradeManagers[i] != null)
                    //         {
                    //             if (_compUpgradeManagers[i].GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
                    //             numberOfBasic ++;
                    //             if (_compUpgradeManagers[i].GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
                    //             numberOfHatchEffects ++;
                    //         }
                    //     }
                    //     if ((numberOfBasic > 1) || (numberOfHatchEffects > 1))
                    //     return true;
                    // }
                    // return false;
        }
        // public UpgradeTypeClass GetBasicUpgrade()
        // {
        //     foreach (Transform upgradeTransform in _verticalContainer.transform)
        //     {
        //         CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
        //         if (compUpgradeManager != null)
        //         {
        //             if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
        //             {
        //                 return compUpgradeManager.GetUpgradeTypeClass();
        //             }
        //         }
        //     }
        //     return null;
        // }
        public ParentUnitClass GetParentUnitClass()
        {
            ParentUnitClass parentUnitClassToReturn = new ParentUnitClass(); 
            // parentUnitClassToReturn.ClearAllUpgrades();
            foreach (Transform upgradeTransform in _verticalContainer.transform)
            {
                CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager != null)
                {
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
                    {
                        parentUnitClassToReturn.SetUTCBasicUnit(compUpgradeManager.GetUpgradeTypeClass());
                    }
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
                    {
                        parentUnitClassToReturn.SetUTCHatchEffect(compUpgradeManager.GetUpgradeTypeClass());                        
                    }
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced)
                    {
                        parentUnitClassToReturn.AddUTCAdvanced(compUpgradeManager.GetUpgradeTypeClass());
                    }
                }
            }
            return parentUnitClassToReturn;
        }
    }
}
