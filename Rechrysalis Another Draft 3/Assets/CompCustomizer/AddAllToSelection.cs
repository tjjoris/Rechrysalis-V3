using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class AddAllToSelection : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;

        public UpgradeTypeClass GetUpgradeTypeClassFromAllUpgrades(int index)
        {
            int categoryCount = 0;
            if (_compCustomizerSO.BasicUnitArray[index] != null)
            {
                return _compCustomizerSO.BasicUnitArray[index].UpgradeTypeClass;
            }
            categoryCount += _compCustomizerSO.BasicUnitArray.Length;
            if (_compCustomizerSO.AdvancedUnitSelectionT1Array[index + categoryCount] != null)
            {
                return _compCustomizerSO.AdvancedUnitSelectionT1Array[index + categoryCount].UpgradeTypeClass;
            }
            categoryCount += _compCustomizerSO.AdvancedUnitSelectionT1Array.Length;
            if (_compCustomizerSO.HatchEffectSelectionArray[index + categoryCount] != null)
            {
                return _compCustomizerSO.HatchEffectSelectionArray[index + categoryCount].UpgradeTypeClass;
            }
            categoryCount += _compCustomizerSO.HatchEffectSelectionArray.Length;
            if (_compCustomizerSO.ControllerHeartUpgrades[index + categoryCount] != null)
            {
                return  _compCustomizerSO.ControllerHeartUpgrades[index + categoryCount].UpgradeTypeClass;
            }
            return null;
        }
    }
}
