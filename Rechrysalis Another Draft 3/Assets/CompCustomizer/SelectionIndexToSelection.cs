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
        private UnitStatsSO _unitStatsSO;
        private HatchEffectSO _hatchEffectSO;
        public enum UpgradeType {Basic, Advanced, HatchEffect, error};
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _compCustomizerSO = compCustomizerSO;        
            _upgradeBasicSelectionCount = _compCustomizerSO.BasicUnitArray.Length;
            _upgradeT1SelectionCount = _compCustomizerSO.AdvancedUnitSelectionT1Array.Length;
            _upgradeHatchEffectCount = _compCustomizerSO.HatchEffectSelectionArray.Length;
            _upgradeselectionCount = (_upgradeBasicSelectionCount + _upgradeT1SelectionCount + _upgradeHatchEffectCount);
        }
        public UpgradeType UpgradeFromIndex(ref UnitStatsSO unitStats, ref HatchEffectSO hatchEffect, int upgradeIndex)
        {
            if (upgradeIndex <= _upgradeBasicSelectionCount)
            {
                unitStats = _compCustomizerSO.BasicUnitArray[upgradeIndex];
                _unitStatsSO = unitStats;
                _hatchEffectSO = null;
                return UpgradeType.Basic;
            }
            else if (upgradeIndex <= (_upgradeBasicSelectionCount + _upgradeT1SelectionCount))
            {
                unitStats = _compCustomizerSO.AdvancedUnitSelectionT1Array[upgradeIndex - _upgradeBasicSelectionCount];
                _unitStatsSO = unitStats;
                _hatchEffectSO = null;
                return UpgradeType.Advanced;
            }
            else {
                hatchEffect = _compCustomizerSO.HatchEffectSelectionArray[upgradeIndex - _upgradeBasicSelectionCount - _upgradeT1SelectionCount];
                _hatchEffectSO = hatchEffect;
                _unitStatsSO = null;
                return UpgradeType.HatchEffect;
            }
            return UpgradeType.error;
        }
        public UnitStatsSO GetUnitStatsSO()
        {
            return _unitStatsSO;
        }
        public HatchEffectSO GetHatchEffectSO()
        {
            return _hatchEffectSO;
        }
    }
}
