using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionIndexToSelection : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private int _upgradeselectionCount;
        public int UpgradeSelctionCount { get{ return _upgradeselectionCount; } set{ _upgradeselectionCount = value; } }
        private int _upgradeBasicSelectionCount;
        private int _upgradeT1SelectionCount;
        private int _upgradeHatchEffectCount;
        public enum UpgradeType {Basic, Advanced, HatchEffect, error};
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _compCustomizerSO = compCustomizerSO;        
            _upgradeBasicSelectionCount = _compCustomizerSO.BasicUnitArray.Length;
            _upgradeT1SelectionCount = _compCustomizerSO.AdvancedUnitSelectionT1Array.Length;
            _upgradeHatchEffectCount = _compCustomizerSO.HatchEffectSelectionArray.Length;
            _upgradeselectionCount = (_upgradeBasicSelectionCount + _upgradeT1SelectionCount + _upgradeHatchEffectCount);
        }
        public UpgradeType UpgradeFromIndex(ref UnitStatsSO _unitStats, ref HatchEffectSO _hatchEffect, int _upgradeIndex)
        {
            if (_upgradeIndex <= _upgradeBasicSelectionCount)
            {
                _unitStats = _compCustomizerSO.BasicUnitArray[_upgradeIndex];
                return UpgradeType.Basic;
            }
            else if (_upgradeIndex <= (_upgradeBasicSelectionCount + _upgradeT1SelectionCount))
            {
                _unitStats = _compCustomizerSO.AdvancedUnitSelectionT1Array[_upgradeIndex - _upgradeBasicSelectionCount];
                return UpgradeType.Advanced;
            }
            else {
                _hatchEffect = _compCustomizerSO.HatchEffectSelectionArray[_upgradeIndex - _upgradeBasicSelectionCount - _upgradeT1SelectionCount];
                return UpgradeType.HatchEffect;
            }
            return UpgradeType.error;
        }
    }
}
