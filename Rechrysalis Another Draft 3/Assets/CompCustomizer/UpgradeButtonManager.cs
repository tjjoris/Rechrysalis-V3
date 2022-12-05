using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UpgradeButtonManager", menuName = "CompCustomizer/UpgradeButtonManager")]

    public class UpgradeButtonManager : MonoBehaviour
    {
        [SerializeField] private UnitStatsSO _unitStatsSO;
        public UnitStatsSO UnitStatsSO { get{ return _unitStatsSO; } set{ _unitStatsSO = value; } }
        [SerializeField] private HatchEffectSO _hatchEffectSO;
        public HatchEffectSO HatchEffectSO { get{ return _hatchEffectSO; } set{ _hatchEffectSO = value; } }
        private RandomUpgradeSelection _randomUpgradeSelection;
        private SelectionIndexToSelection _selectionIndexToSelection;
        
        public void Initialize(CompCustomizerSO _compCustomizerSO)
        {
            _randomUpgradeSelection = GetComponent<RandomUpgradeSelection>();
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();  
            _selectionIndexToSelection.Initialize(_compCustomizerSO);
            _randomUpgradeSelection.Initialize();          
        }
        public void GetRandomSelection(CompCustomizerSO compCustomizerSO, int[] upgradeSelectionIndex, int selectionCount)
        {
            _randomUpgradeSelection.GetRandomSelection(compCustomizerSO, upgradeSelectionIndex, selectionCount);
        }
        public RandomUpgradeSelection GetRandomUpgradeSelection()
        {
            return _randomUpgradeSelection;
        }
        public SelectionIndexToSelection GetSelectionIndexToSelection()
        {
            return _selectionIndexToSelection;
        }
    }
}
