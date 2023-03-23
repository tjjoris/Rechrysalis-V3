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
        [SerializeField] private List<float> _rangesList = new List<float>();

        public WaveLayout GetWaveLayoutByRange(float range)
        {
            for (int i=0; i < _waveLayouts.Count; i++)
            {
                float minRange = 0;
                if ((i >= 1) && (i <= _rangesList.Count - 1))
                {
                    minRange = _rangesList[i -1];
                }           
                float maxRange = 99;
                if ((i <= _rangesList.Count -1))
                {
                    maxRange = _rangesList[i];
                }     
                if ((range >= minRange) && (range <= maxRange))
                {
                    Debug.Log($"wave layout " + i);
                    return _waveLayouts[i];
                }                
            }
            return null;
        }
    }
}
