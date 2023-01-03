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
        bool debugBool = false;
        [SerializeField] private List<CompUpgradeManager> _compUpgradeManagers;
        [SerializeField] private VerticalContainer _verticalContainer;
        [SerializeField] private DropBackGround _dropBackGround;
        [SerializeField] private ScrollRect _scrollRect;
        private Transform _movingButtonHolder;
        [SerializeField] private ParentUnitClass _parentUnitClass;
        public Action<CompVerticalManager> _vertcialDropped;
        
        private void OnEnable()
        {
            if (_dropBackGround == null) return;
            _dropBackGround._buttonDropped -= DroppedIntoVertical;
            _dropBackGround._buttonDropped += DroppedIntoVertical;
        }
        private void OnDisable()
        {
            if (_dropBackGround == null) return;
            _dropBackGround._buttonDropped -= DroppedIntoVertical;
        }
        public void Initialize(Transform movingButtonHolder)
        {

            if (debugBool)
                Debug.Log($"initialize vertical");
            _scrollRect = GetComponent<ScrollRect>();
            _movingButtonHolder = movingButtonHolder;
        }
        private void DroppedIntoVertical()
        {
            Debug.Log($"dropped into vertical for comp vertical manager");
            _vertcialDropped?.Invoke(this);
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
        private void DisableScrollRect()
        {
            _scrollRect.enabled = false;
        }
        public void EnableScrollRect()
            {
                _scrollRect.enabled = true;
            }
        public int GetNumberOfBasic()
        {
            int numberOfBasic = 0;
            foreach (Transform upgradeTransform in _verticalContainer.transform)
            {
                CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager!= null)
                {
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
                    {
                        numberOfBasic ++;
                    }
                }            
            }
            return numberOfBasic;
        }
        public int GetNumberOfHatchEffects()
        {
            int numberOfHatchEffects = 0;
            foreach (Transform upgradeTransform in _verticalContainer.transform)
            {
                CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager != null)
                {
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
                        numberOfHatchEffects++;
                }
            }
            return numberOfHatchEffects;
        }
        public bool IsAtLeastOneAdvUpgrade()
        {
            int numberOfAdvUpgrades = 0;
            foreach (Transform upgradeTransform in _verticalContainer.transform)
            {
                CompUpgradeManager compUpgradeManager = upgradeTransform.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager != null)
                {
                    if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced)
                        numberOfAdvUpgrades++;
                }
            }
            if (numberOfAdvUpgrades > 0)
            return true;
            return false;
        }
        public ParentUnitClass GetParentUnitClass()
        {
            ParentUnitClass parentUnitClassToReturn = new ParentUnitClass(); 
            parentUnitClassToReturn.ClearAllUpgrades();
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
                        // parentUnitClassToReturn.AdvancedUpgradesUTCList.Add(compUpgradeManager.GetUpgradeTypeClass());
                        parentUnitClassToReturn.AddUTCAdvanced(compUpgradeManager.GetUpgradeTypeClass());
                        // parentUnitClassToReturn.AddUTCAdvanced(compUpgradeManager.GetUpgradeTypeClass());
                    }
                }
            }
            parentUnitClassToReturn.SetAllStats();
            return parentUnitClassToReturn;
        }
    }
}
