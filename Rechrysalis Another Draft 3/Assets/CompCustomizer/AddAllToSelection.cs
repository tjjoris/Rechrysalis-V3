using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class AddAllToSelection : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;

        public UpgradeTypeClass GetUpgradeTypeClassFromAllUpgrades(int index)
        {
            int categoryCount = 0;
            if ((_compCustomizerSO.BasicUnitArray.Length > (index)) && (_compCustomizerSO.BasicUnitArray[index] != null))
            {
                return _compCustomizerSO.BasicUnitArray[index].UpgradeTypeClass;
            }
            categoryCount += _compCustomizerSO.BasicUnitArray.Length;
            if ((_compCustomizerSO.AdvancedUnitSelectionT1Array.Length > (index - categoryCount)) && (_compCustomizerSO.AdvancedUnitSelectionT1Array[index - categoryCount] != null))
            {
                return _compCustomizerSO.AdvancedUnitSelectionT1Array[index - categoryCount].UpgradeTypeClass;
            }
            categoryCount += _compCustomizerSO.AdvancedUnitSelectionT1Array.Length;
            if ((_compCustomizerSO.HatchEffectSelectionPrefabArray.Length > (index - categoryCount)) && (_compCustomizerSO.HatchEffectSelectionPrefabArray[index - categoryCount] != null))
            {
                return _compCustomizerSO.HatchEffectSelectionPrefabArray[index - categoryCount].GetComponent<HatchEffectManager>().UpgradeTypeClass;
            }
            categoryCount += _compCustomizerSO.HatchEffectSelectionPrefabArray.Length;
            if ((_compCustomizerSO.ControllerHeartUpgrades.Length > (index - categoryCount)) && (_compCustomizerSO.ControllerHeartUpgrades[index - categoryCount] != null))
            {
                return  _compCustomizerSO.ControllerHeartUpgrades[index - categoryCount].UpgradeTypeClass;
            }
            return null;
        }
    }
}
