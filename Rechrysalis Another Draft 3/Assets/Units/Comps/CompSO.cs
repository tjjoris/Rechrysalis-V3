using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="CompSO", menuName ="Comps/CompSO")]
    public class CompSO : ScriptableObject
    {
        [SerializeField] private UnitStatsSO[] _unitSOArray;
        public UnitStatsSO[] UnitSOArray {get{return _unitSOArray;}}
    }
}
