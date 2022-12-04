using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "CompCustomizerSO", menuName = "CompCustimzer/CompCustomizerSO")]
    public class CompCustomizerSO : ScriptableObject
    {
        [SerializeField] private int _numberOfUpgrades;
        public int NumberOfUpgrades { get{ return _numberOfUpgrades; } set{ _numberOfUpgrades = value; } }
        [SerializeField] private UnitStatsSO[] _basicUnitSelectionArray;
        public UnitStatsSO[] BasicUnitArray { get{ return _basicUnitSelectionArray; }}
        [SerializeField] private UnitStatsSO[] _advancedUnitSelectionT1Array;
        public UnitStatsSO[] AdvancedUnitSelectionT1Array { get{ return _advancedUnitSelectionT1Array; }}
        [SerializeField] private HatchEffectSO[] _hatchEffectSelectionArray;
        public HatchEffectSO[] HatchEffectSelectionArray { get{ return _hatchEffectSelectionArray; }}

        
    }
}
