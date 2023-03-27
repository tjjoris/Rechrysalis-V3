using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    //for free enemy initialize
    public class WaveClass 
    {
        [SerializeField] private List<ParentUnitClass> _unitsInWave = new List<ParentUnitClass>();
        public List<ParentUnitClass> UnitsInWave { get => _unitsInWave; set => _unitsInWave = value; }
        [SerializeField] private float _progressValueOfWave;
        public float ProgressValueOfWave { get => _progressValueOfWave; set => _progressValueOfWave = value; }
        
        
    }
}
