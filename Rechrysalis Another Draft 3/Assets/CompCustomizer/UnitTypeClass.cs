using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class UnitTypeClass
        {
            public enum UnitType { Basic, Advanced, HatchEffect, Error };
            [SerializeField] private UnitType _unitType;
            public UnitType UnitTypeVar { get { return _unitType; } set { _unitType = value; } }
        }
}
