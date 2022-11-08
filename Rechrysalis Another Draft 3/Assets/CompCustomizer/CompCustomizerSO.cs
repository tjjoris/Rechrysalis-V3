using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "CompCustomizerSO", menuName = "Comps/CompCustomizerSO")]
    public class CompCustomizerSO : ScriptableObject
    {
        [SerializeField] private int _numberOfUpgrades;
        public int NumberOfUpgrades {get{return _numberOfUpgrades;} set{_numberOfUpgrades = value;}}
        [SerializeField] private int _numberOfAdvUnits;
        public int NumberOfAdvUnits {get{return _numberOfAdvUnits;}}
        [SerializeField] private CompSO _compSO;
        public CompSO CompSO {get{return _compSO;}}
        [SerializeField] private UnitStatsSO[] _arrayOfAvailableBasicUnits;
        public UnitStatsSO[] ArrayOfAvailableBasicUnits {get {return _arrayOfAvailableBasicUnits;}}
        [SerializeField] private UnitStatsSO[] _arrayOfAvailableAdvUnits;
        public UnitStatsSO[] ArrayOfAvailableAdvUnits {get {return _arrayOfAvailableAdvUnits;}}
         [SerializeField] private HatchEffectSO[] _arrayOfAvailableHatcheffects;
         public HatchEffectSO[] ArrayOfAvailableHatchEffects {get {return _arrayOfAvailableHatcheffects;}}
    }
}
