using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class FreeEnemyUnitInstantiator : MonoBehaviour
    {
        private WaveLayoutsByRange _waveLayoutsByRange;

        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            _waveLayoutsByRange = GetComponent<WaveLayoutsByRange>();
        }
        public void InstantiateUnit(ParentUnitClass parentUnitClass)
        {
            
        }
    }
}
