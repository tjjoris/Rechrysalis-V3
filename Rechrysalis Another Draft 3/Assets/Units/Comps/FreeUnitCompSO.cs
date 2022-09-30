using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "FreeUnitCompSO", menuName = "Comps/FreeUnitCompSO")]
    public class FreeUnitCompSO : ScriptableObject
    {
        [SerializeField] private UnitStatsSO[] _unitSOArray;
        public UnitStatsSO[] UnitSOArray { get { return _unitSOArray; } }
        
    }
}
