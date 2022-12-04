using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UpgradeButtonManager", menuName = "CompCustomizer/UpgradeButtonManager")]

    public class UpgradeButtonManager : MonoBehaviour
    {
        [SerializeField] private UnitStatsSO _unitStatsSO;
        public UnitStatsSO UnitStatsSO { get{ return _unitStatsSO; } set{ _unitStatsSO = value; } }
        
    }
}
