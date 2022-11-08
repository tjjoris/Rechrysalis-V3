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
        [SerializeField] private WaveSO[] _waves;
        public WaveSO[] Waves { get {return _waves;}}
        [SerializeField] private WaveLayout _waveLayout;
        public WaveLayout WaveLayout {get {return _waveLayout;}}
        
    }
}
