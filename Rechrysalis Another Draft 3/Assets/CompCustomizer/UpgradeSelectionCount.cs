using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class UpgradeSelectionCount : MonoBehaviour
    {        
        public int GetUpgradeSelectionCount(CompCustomizerSO _compCustomizer)
        {

            return _compCustomizer.BasicUnitArray.Length + _compCustomizer.AdvancedUnitSelectionT1Array.Length + _compCustomizer.HatchEffectSelectionArray.Length;
        }
    }
}
