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
        private int _upgradeBasicSelectionCount;
        private int _upgradeT1SelectionCount;
        private int _upgradeHatchEffectCount;
        // private UnitStatsSO _unitStatsSO;
        // private HatchEffectSO _hatchEffectSO;
        private RandomUpgradeSelection _randomUpgradeSelection;
        public enum UpgradeType {Basic, Advanced, HatchEffect, Error};
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _upgradeTypeClass = new UpgradeTypeClass();
            _randomUpgradeSelection = GetComponent<RandomUpgradeSelection>();
            _compCustomizerSO = compCustomizerSO;        
            _upgradeBasicSelectionCount = _compCustomizerSO.BasicUnitArray.Length;
            _upgradeT1SelectionCount = _compCustomizerSO.AdvancedUnitSelectionT1Array.Length;
            _upgradeHatchEffectCount = _compCustomizerSO.HatchEffectSelectionArray.Length;
        }
        public void UpgradeFromIndex(int upgradeIndex)
        {
            if (upgradeIndex < _upgradeBasicSelectionCount)
            {
                // _unitStatsSO = _compCustomizerSO.BasicUnitArray[upgradeIndex];
                // _hatchEffectSO = null; 
                _upgradeTypeClass.SetUnitStatsSO(_compCustomizerSO.BasicUnitArray[upgradeIndex]);
                _upgradeTypeClass.SetHatchEffectSO(null);
                SetUpgradeTypeClass(UpgradeTypeClass.UpgradeType.Basic);              
            }
            else if (upgradeIndex < (_upgradeBasicSelectionCount + _upgradeT1SelectionCount))
            {
                // _unitStatsSO = _compCustomizerSO.AdvancedUnitSelectionT1Array[upgradeIndex - _upgradeBasicSelectionCount];
                // _hatchEffectSO = null;
                _upgradeTypeClass.SetUnitStatsSO(_compCustomizerSO.AdvancedUnitSelectionT1Array[upgradeIndex - _upgradeBasicSelectionCount]);
                _upgradeTypeClass.SetHatchEffectSO(null);
                SetUpgradeTypeClass(UpgradeTypeClass.UpgradeType.Advanced);
            }
            else {
                // _hatchEffectSO = _compCustomizerSO.HatchEffectSelectionArray[upgradeIndex - _upgradeBasicSelectionCount - _upgradeT1SelectionCount];
                // _unitStatsSO = null;
                _upgradeTypeClass.SetUnitStatsSO(null);
                _upgradeTypeClass.SetHatchEffectSO(_compCustomizerSO.HatchEffectSelectionArray[upgradeIndex - _upgradeBasicSelectionCount - _upgradeT1SelectionCount]);
                SetUpgradeTypeClass(UpgradeTypeClass.UpgradeType.HatchEffect);
            }
        }
        private void SetUpgradeTypeClass(UpgradeTypeClass.UpgradeType upgradeType)
        {
            _upgradeTypeClass.SetUpgradeType(upgradeType);
        }
        public UnitStatsSO GetUnitStatsSO()
        {
            // return _unitStatsSO;
            return _upgradeTypeClass.GetUnitStatsSO();
        }
        public HatchEffectSO GetHatchEffectSO()
        {
            // return _hatchEffectSO;
            return _upgradeTypeClass.GetHatchEffectSO();
        }
        public UpgradeTypeClass.UpgradeType GetThisUpgradeType()
        {
            return _upgradeTypeClass.GetUpgradeType();
        }
    }
}
