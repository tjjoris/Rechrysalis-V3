using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewWave", menuName ="Comps/Wave")]
    public class WaveSO : ScriptableObject
    {
        [SerializeField] private UnitStatsSO[] _unitWave;
        public UnitStatsSO[] UnitWave {get{return _unitWave;}}
    }
}
