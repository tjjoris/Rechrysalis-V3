using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="CompSO", menuName ="Comps/CompSO")]
    public class CompSO : ScriptableObject
    {
        [SerializeField] private UnitStatsSO[] _unitSOArray;        
        public UnitStatsSO[] UnitSOArray {get{return _unitSOArray;} set {_unitSOArray = value;}}
        [SerializeField] private HatchEffectSO[] _hatchEffectSOArray;
        public HatchEffectSO[] HatchEffectSOArray {get{return _hatchEffectSOArray;} set {_hatchEffectSOArray = value;}}
        [SerializeField] private int _parentUnitcount;
        public int ParentUnitCount {get { return _parentUnitcount;}}
        [SerializeField] private int _childUnitCount;
        public int ChildUnitCount {get{return _childUnitCount;}}
        [SerializeField] private int[] _upgradeCountArray;
        public int[] UpgradeCountArray {get {return _upgradeCountArray;}}
    }
}
