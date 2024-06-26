using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rechrysalis.Unit;
using Rechrysalis.Controller;

namespace Rechrysalis.CompCustomizer
{
    public class CompVerticalManager : MonoBehaviour//, IDropHandler
    {
        bool _debugBool = false;
        private CompsAndUnitsSO _compsAndUnitsSO;
        [SerializeField] private List<CompUpgradeManager> _compUpgradeManagers;
        [SerializeField] private Transform _verticalContainer;
        public Transform VerticalContainer => _verticalContainer;
        [SerializeField] private DropBackGround _dropBackGround;
        [SerializeField] private ScrollRect _scrollRect;
        private Transform _movingButtonHolder;
        [SerializeField] private ParentUnitClass _parentUnitClass;
        [SerializeField] private InstantiateButton _instanatiateButton;
        // public Action<CompVerticalManager, CompUpgradeManager> _vertcialDropped;
        
        // private void OnEnable()
        // {
        //     if (_dropBackGround == null) return;
        //     _dropBackGround._buttonDropped -= DroppedIntoVertical;
        //     _dropBackGround._buttonDropped += DroppedIntoVertical;
        // }
        // private void OnDisable()
        // {
        //     if (_dropBackGround == null) return;
        //     _dropBackGround._buttonDropped -= DroppedIntoVertical;
        // }
        public void Initialize(Transform movingButtonHolder, CompsAndUnitsSO compsAndUnitsSO, ControllerHPTokens controllerHPTokens, InstantiateButton instantiateButton)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
            if (_debugBool)
                Debug.Log($"initialize vertical");
            _scrollRect = GetComponent<ScrollRect>();
            _movingButtonHolder = movingButtonHolder;
            _dropBackGround.Initialize(compsAndUnitsSO);
            DropComp dropComp =(DropComp) _dropBackGround;
            if (dropComp != null) dropComp.ControllerHPTokens = controllerHPTokens;
            if ((_verticalContainer != null) && (_verticalContainer.GetComponent<DropComp>() != null))
            _verticalContainer.GetComponent<DropComp>().ControllerHPTokens = controllerHPTokens;
            _instanatiateButton = instantiateButton;
        }
        // private void DroppedIntoVertical(CompUpgradeManager compUpgradeManager)
        // {
        //     if (debugBool) Debug.Log($"dropped into vertical for comp vertical manager");
        //     // _vertcialDropped?.Invoke(this, compUpgradeManager);
        // }
        public void CreateAndSetUpCompButtons(ParentUnitClass parentUnitClass, GameObject compButtonPrefab)
        {
            CreateCompButton(compButtonPrefab, parentUnitClass.UTCBasicUnit);
            if ((parentUnitClass.UTCHatchEffects != null) && (parentUnitClass.UTCHatchEffects.Count > 0))
            {
                foreach (UpgradeTypeClass hatchEffect in parentUnitClass.UTCHatchEffects)
                {
                    if (hatchEffect != null)
                    {
                        CreateCompButton(compButtonPrefab, hatchEffect);
                    }
                }
            }
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
                // GameObject compButtonCreated = Instantiate(compButtonPrefab, _verticalContainer);
                // CompUpgradeManager compUpgradeManager = compButtonCreated.GetComponent<CompUpgradeManager>();
                // compUpgradeManager?.Initialize(_movingButtonHolder);
                // compUpgradeManager?.SetUpgradeTypeClass(upgradeTypeClass);
                // compUpgradeManager?.SetDisplay(upgradeTypeClass);
                _instanatiateButton.CreateSelectionButton(upgradeTypeClass, _verticalContainer);
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
            foreach (Transform upgradeTransform in _verticalContainer)
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
            foreach (Transform upgradeTransform in _verticalContainer)
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
            foreach (Transform upgradeTransform in _verticalContainer)
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
            parentUnitClassToReturn.Initialize(_compsAndUnitsSO);
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
                        if (_debugBool) Debug.Log($"add hatch effect " +compUpgradeManager.gameObject.name);
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
