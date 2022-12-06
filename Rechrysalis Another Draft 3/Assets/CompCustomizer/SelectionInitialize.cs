using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionInitialize : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private GameObject _upgradeButtonPrefab;
        private int _numberOfUpgrades = 3;
        [SerializeField] private UpgradeButtonManager[] _upgradebuttonManager;
        public UpgradeButtonManager[] UpgradeButtonManager { get{ return _upgradebuttonManager; } set{ _upgradebuttonManager = value; } }
        [SerializeField] private int[] _upgradeButtonIndex;
        private SelectionIndexToSelection _selectionIndexToSelection;
        private int _upgradeSelectionCount;
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _upgradebuttonManager = new UpgradeButtonManager[_numberOfUpgrades];
            _upgradeButtonIndex = new int[_numberOfUpgrades];
            _compCustomizerSO = compCustomizerSO;
            CalculateUpgradeSelectionCount();
            CreateAllSelectionButtons();     
        }
        private void CreateSelectionButton(int index)
        {
            GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, transform);
            _upgradebuttonManager[index] = _selectionButton.GetComponent<UpgradeButtonManager>();
            _upgradebuttonManager[index]?.Initialize(_compCustomizerSO);
            _upgradebuttonManager[index]?.GetRandomSelection(_compCustomizerSO, _upgradeButtonIndex, _upgradeSelectionCount);
            _upgradeButtonIndex[index] = _upgradebuttonManager[index].GetRandomUpgradeSelection().GetRandomIndex();
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
    }
}
