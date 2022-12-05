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
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _upgradebuttonManager = new UpgradeButtonManager[_numberOfUpgrades];
            _upgradeButtonIndex = new int[_numberOfUpgrades];
            _compCustomizerSO = compCustomizerSO;
            // _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
            // _selectionIndexToSelection?.Initialize(_compCustomizerSO);  
            CreateAllSelectionButtons();     
        }
        private void CreateSelectionButton(int _index)
        {
            GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, transform);
            _upgradebuttonManager[_index] = _selectionButton.GetComponent<UpgradeButtonManager>();
            _upgradebuttonManager[_index]?.Initialize();
            _upgradebuttonManager[_index]?.GetRandomSelection(_compCustomizerSO, _upgradeButtonIndex);
            _upgradeButtonIndex[_index] = _upgradebuttonManager[_index].GetRandomUpgradeSelection().GetRandomIndex();
        }
        private void CreateAllSelectionButtons()
        {
            for (int _index = 0; _index < _numberOfUpgrades; _index ++)
            {
                CreateSelectionButton(_index);
            }
        }
    }
}
