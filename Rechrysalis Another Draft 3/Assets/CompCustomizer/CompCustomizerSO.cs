using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "CompCustomizerSO", menuName = "CompCustimzer/CompCustomizerSO")]
    public class CompCustomizerSO : ScriptableObject
    {
        [SerializeField] private int _numberOfUpgrades;
        public int NumberOfUpgrades { get{ return _numberOfUpgrades; } set{ _numberOfUpgrades = value; } }
        
    }
}
