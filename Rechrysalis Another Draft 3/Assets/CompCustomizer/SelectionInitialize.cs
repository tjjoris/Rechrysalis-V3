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
        private int _numberOfUpgrades = 3;
        [SerializeField] private CompUpgradeManager[] _compUpgradeManagers;
        [SerializeField] private int[] _upgradeButtonIndex;
        private SelectionIndexToSelection _selectionIndexToSelection;
        private int _upgradeSelectionCount;
        private RandomUpgradeSelection _randomUpgradeSelection;
        private UpgradeTypeClass[] _upgradeTypeClassesToChooseFrom;
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO, Transform movingButtonHolder, CompSO compSO)
        {
            _movingButtonHolder = movingButtonHolder;
            _compUpgradeManagers = new CompUpgradeManager[_numberOfUpgrades];
            // _upgradebuttonManager = new UpgradeButtonManager[_numberOfUpgrades];
            _upgradeButtonIndex = new int[_numberOfUpgrades];
            _compCustomizerSO = compCustomizerSO;
            _upgradeTypeClassesToChooseFrom = new UpgradeTypeClass[_numberOfUpgrades];
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
            _selectionIndexToSelection.Initialize(compCustomizerSO);
            _randomUpgradeSelection= GetComponent<RandomUpgradeSelection>();
            _randomUpgradeSelection.Initialize();
            CalculateUpgradeSelectionCount();
            if (!IsCompExists(compSO)) Debug.Log($"comp does not exist");
            CreateAllSelectionButtons();   
        }
        private void CreateSelectionButton(int index)
        {            
            GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, _selectionContainer);
            _compUpgradeManagers[index] = _selectionButton.GetComponent<CompUpgradeManager>();
            _compUpgradeManagers[index].Initialize(_movingButtonHolder);
            UpgradeTypeClass _randomUpgradeTypeClass = _randomUpgradeSelection.GetRandomUpgradeTypeClass(_upgradeTypeClassesToChooseFrom, _upgradeSelectionCount);
            _upgradeTypeClassesToChooseFrom[index] = _randomUpgradeTypeClass;
            _compUpgradeManagers[index].SetUpgradeTypeClass(_randomUpgradeTypeClass);
            _compUpgradeManagers[index].SetDisplay(_randomUpgradeTypeClass);
            
        }
        private void CreateAllSelectionButtons()
        {
            for (int _index = 0; _index < _numberOfUpgrades; _index ++)
            {
                CreateSelectionButton(_index);
            }
        }
        public void CalculateUpgradeSelectionCount()
        {
            _upgradeSelectionCount = _compCustomizerSO.BasicUnitArray.Length + _compCustomizerSO.AdvancedUnitSelectionT1Array.Length + _compCustomizerSO.HatchEffectSelectionArray.Length;
        }
        private bool IsCompExists(CompSO compSO)
        {
            if (compSO == null)
            return false;
            return false;
        }
    }
}
