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
        [SerializeField] private List<ParentUnitClass> _parentUnitClassList;
        public List<ParentUnitClass> ParentUnitClassList { get{ return _parentUnitClassList; } set{ _parentUnitClassList = value; } }
        
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

        public bool IsCompExists()
        {
            if (_parentUnitClassList.Count == 0)
                return false;
            bool basicExists = false;
            for (int i = 0; i < _parentUnitClassList.Count; i++)
            {
                if (DoesParentExist(i))
                basicExists = true;
            }
            if (basicExists)
                return true;
            return false;
        }

        public bool DoesParentExist(int parentIndex)
        {
            if (_parentUnitClassList[parentIndex].UTCBasicUnit.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)
            {
                return true;
            }
            return false;
        }
    }
}
