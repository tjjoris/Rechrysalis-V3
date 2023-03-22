using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "WaveLayoutsByRange" , menuName = "Unit/FreeUnitLayout/WavesByRange")]
    public class WaveLayoutsByRange : ScriptableObject
    {
        [SerializeField] private List<WaveLayout> _waveLayouts = new List<WaveLayout>();
        public List<WaveLayout> WaveLayouts { get => _waveLayouts; set => _waveLayouts = value; }
        
    }
}
