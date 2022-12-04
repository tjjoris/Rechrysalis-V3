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
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _upgradebuttonManager = new UpgradeButtonManager[_numberOfUpgrades];
            _compCustomizerSO = CompCustomizerSO;
            CreateSelectionButton();
        }
        private void CreateSelectionButton()
        {
            GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, transform);
        }
    }
}
