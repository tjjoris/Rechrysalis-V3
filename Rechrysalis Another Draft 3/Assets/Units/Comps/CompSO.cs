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
        [SerializeField] private int _parentUnitcount;
        public int ParentUnitCount {get { return _parentUnitcount;}}
        [SerializeField] private int _childUnitCount;
        public int ChildUnitCount {get{return _childUnitCount;}}
    }
}
