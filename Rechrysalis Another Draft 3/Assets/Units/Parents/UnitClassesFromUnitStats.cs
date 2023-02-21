using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class UnitClassesFromUnitStats : MonoBehaviour
    {
        [SerializeField] private ParentUnitClass _parentUnitClass;
        public ParentUnitClass ParentUnitClass { get => _parentUnitClass; set => _parentUnitClass = value; }
        // [SerializeField] private UnitStatsSO _unitStatsSO;
        // public UnitStatsSO UnitStatsSO { get => _unitStatsSO; set => _unitStatsSO = value; }
        
        public void SetUnitClassesFunc(UnitStatsSO basicUnitStats, UnitStatsSO advUnitStats)
        {
            _parentUnitClass = new ParentUnitClass();
            UpgradeTypeClass utc = new UpgradeTypeClass(); 
            utc.SetUnitStatsSO(basicUnitStats);
            utc.SetUpgradeType(UpgradeTypeClass.UpgradeType.Basic);
            _parentUnitClass.SetUTCBasicUnit(utc);
        }
    }
}
