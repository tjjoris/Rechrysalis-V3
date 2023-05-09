using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionIndexToSelection : MonoBehaviour
    {
        bool debugBool = false;
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        private int _upgradeBasicSelectionCount;
        private int _upgradeT1SelectionCount;
        private int _upgradeHatchEffectCount;
        private int _upgradeHeartsCount;
        private RandomUpgradeSelection _randomUpgradeSelection;
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _randomUpgradeSelection = GetComponent<RandomUpgradeSelection>();
            _compCustomizerSO = compCustomizerSO;        
            _upgradeBasicSelectionCount = _compCustomizerSO.BasicUnitArray.Length;
            _upgradeT1SelectionCount = _compCustomizerSO.AdvancedUnitSelectionT1Array.Length;
            _upgradeHatchEffectCount = _compCustomizerSO.HatchEffectSelectionPrefabArray.Length;
            _upgradeHeartsCount = _compCustomizerSO.ControllerHeartUpgrades.Length;
        }
        public UpgradeTypeClass GetUpgradeTypeClassFromIndex(int index)
        {
            if (debugBool)
            Debug.Log($"upgrade index " + index);
            if (index < _upgradeBasicSelectionCount)
            {
                return _compCustomizerSO.BasicUnitArray[index].UpgradeTypeClass;
            }
            else if (index < (_upgradeBasicSelectionCount + _upgradeT1SelectionCount))
            {
                return _compCustomizerSO.AdvancedUnitSelectionT1Array[index - _upgradeBasicSelectionCount].UpgradeTypeClass;
            }
            else if (index < (_upgradeBasicSelectionCount + _upgradeT1SelectionCount + _upgradeHatchEffectCount)) {
                return _compCustomizerSO.HatchEffectSelectionPrefabArray[index - _upgradeBasicSelectionCount - _upgradeT1SelectionCount].GetComponent<HatchEffectManager>().UpgradeTypeClass;
            }
            else 
            {
                return _compCustomizerSO.ControllerHeartUpgrades[index - _upgradeBasicSelectionCount - _upgradeT1SelectionCount - _upgradeHatchEffectCount].UpgradeTypeClass;
            }
        }
    }
}
