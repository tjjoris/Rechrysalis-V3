using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Unit;
// using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionInitialize : MonoBehaviour
    {
        [SerializeField] private Transform _selectionContainer;
        private bool debugBool = true;
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private GameObject _upgradeButtonPrefab;
        private Transform _movingButtonHolder;
        private int _numberOfUpgrades = 4;
        [SerializeField] private int[] _upgradeButtonIndex;
        private SelectionIndexToSelection _selectionIndexToSelection;
        private int _upgradeSelectionCount;
        private RandomUpgradeSelection _randomUpgradeSelection;
        private UpgradeTypeClass[] _upgradeTypeClassesToChooseFrom;
        private AddAllToSelection _addAllToSelection;
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO, Transform movingButtonHolder, CompSO compSO)
        {
            _addAllToSelection = GetComponent<AddAllToSelection>();
            _movingButtonHolder = movingButtonHolder;
            _upgradeButtonIndex = new int[_numberOfUpgrades];
            _compCustomizerSO = compCustomizerSO;
            _upgradeTypeClassesToChooseFrom = new UpgradeTypeClass[_numberOfUpgrades];
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
            _selectionIndexToSelection.Initialize(compCustomizerSO);
            _randomUpgradeSelection= GetComponent<RandomUpgradeSelection>();
            _randomUpgradeSelection.Initialize();
            CalculateUpgradeSelectionCount();
            if (_addAllToSelection != null)
            {
                for (int i = 0; i < _upgradeSelectionCount; i++)
                {
                    UpgradeTypeClass upgradeTypeClass = _addAllToSelection.GetUpgradeTypeClassFromAllUpgrades(i);
                    CreateSelectionButton(i, upgradeTypeClass);
                }
            }
            else if (!compSO.IsCompExists()) 
            {
                CreateOnlyBasicSelection();
            }
            else
            {
                CreateAllSelectionButtons();   
            }
        }
        private void CreateOnlyBasicSelection()
        {
            for (int i=0; i< _compCustomizerSO.BasicUnitArray.Length; i++)
            {
                GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, _selectionContainer);
                CompUpgradeManager compUpgradeManager = _selectionButton.GetComponent<CompUpgradeManager>();
                compUpgradeManager.Initialize(_movingButtonHolder);
                // UpgradeTypeClass _randomUpgradeTypeClass = _randomUpgradeSelection.GetRandomUpgradeTypeClass(_upgradeTypeClassesToChooseFrom, _upgradeSelectionCount);
                // _upgradeTypeClassesToChooseFrom[index] = _randomUpgradeTypeClass;
                compUpgradeManager.SetUpgradeTypeClass(_compCustomizerSO.BasicUnitArray[i].UpgradeTypeClass);
                compUpgradeManager.SetDisplay(_compCustomizerSO.BasicUnitArray[i].UpgradeTypeClass);
            }
        }
       
        private void CreateSelectionButton(int index, UpgradeTypeClass upgradeTypeClass)
        {            
            GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, _selectionContainer);
            CompUpgradeManager compUpgradeManager = _selectionButton.GetComponent<CompUpgradeManager>();
            compUpgradeManager.Initialize(_movingButtonHolder);
            // UpgradeTypeClass _randomUpgradeTypeClass = _randomUpgradeSelection.GetRandomUpgradeTypeClass(_upgradeTypeClassesToChooseFrom, _upgradeSelectionCount);            
            _upgradeTypeClassesToChooseFrom[index] = upgradeTypeClass;
            compUpgradeManager.SetUpgradeTypeClass(upgradeTypeClass);
            compUpgradeManager.SetDisplay(upgradeTypeClass);
            
        }
        private void CreateAllSelectionButtons()
        {
            for (int index = 0; index < _numberOfUpgrades; index ++)
            {
                UpgradeTypeClass _randomUpgradeTypeClass = _randomUpgradeSelection.GetRandomUpgradeTypeClass(_upgradeTypeClassesToChooseFrom, _upgradeSelectionCount);
                CreateSelectionButton(index, _randomUpgradeTypeClass);
            }
        }
        public void CalculateUpgradeSelectionCount()
        {
            _upgradeSelectionCount = _compCustomizerSO.BasicUnitArray.Length + _compCustomizerSO.AdvancedUnitSelectionT1Array.Length + _compCustomizerSO.HatchEffectSelectionArray.Length + _compCustomizerSO.ControllerHeartUpgrades.Length;
        }
    }
}
