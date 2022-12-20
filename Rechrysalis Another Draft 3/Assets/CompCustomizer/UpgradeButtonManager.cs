using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using System;

namespace Rechrysalis.CompCustomizer
{
    // [System.Serializable]
    // [CreateAssetMenu(fileName = "UpgradeButtonManager", menuName = "CompCustomizer/UpgradeButtonManager")]

    public class UpgradeButtonManager : MonoBehaviour
    {
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        public UpgradeTypeClass UpgradeTypeClass {get {return _upgradeTypeClass;}}
        [SerializeField] private UnitStatsSO _unitStatsSO;
        public UnitStatsSO UnitStatsSO { get{ return _unitStatsSO; } set{ _unitStatsSO = value; } }
        [SerializeField] private HatchEffectSO _hatchEffectSO;
        public HatchEffectSO HatchEffectSO { get{ return _hatchEffectSO; } set{ _hatchEffectSO = value; } }
        private RandomUpgradeSelection _randomUpgradeSelection;
        private SelectionIndexToSelection _selectionIndexToSelection;
        private UpgradeButtonDisplay _upgradeButtonDisplay;
        public Action<UpgradeButtonManager> _onUpgradeButtonClicked;
        
        public void Initialize(CompCustomizerSO _compCustomizerSO)
        {
            _randomUpgradeSelection = GetComponent<RandomUpgradeSelection>();
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
            _upgradeButtonDisplay = GetComponent<UpgradeButtonDisplay>();
            _selectionIndexToSelection.Initialize(_compCustomizerSO);
            _randomUpgradeSelection.Initialize();   
            _upgradeButtonDisplay.Initialzie();
            // _upgradeTypeClass = upgradeTypeClass;
        }
        public void GetRandomSelection(CompCustomizerSO compCustomizerSO, int[] upgradeSelectionIndex, int selectionCount)
        {
            _randomUpgradeSelection.GetRandomSelectionOld(compCustomizerSO, upgradeSelectionIndex, selectionCount);
            // _selectionIndexToSelection.UpgradeFromIndex(_randomUpgradeSelection.GetRandomIndex());
            _upgradeTypeClass = _selectionIndexToSelection.GetUpgradeTypeClassFromIndex(_randomUpgradeSelection.GetRandomIndexOld());            
            _upgradeButtonDisplay.SetButotnDisplay(_upgradeTypeClass);
        }
        public RandomUpgradeSelection GetRandomUpgradeSelection()
        {
            return _randomUpgradeSelection;
        }
        public SelectionIndexToSelection GetSelectionIndexToSelection()
        {
            return _selectionIndexToSelection;
        }
        public void ButtonClicked()
        {
            _onUpgradeButtonClicked?.Invoke(this);
        }
    }
}
