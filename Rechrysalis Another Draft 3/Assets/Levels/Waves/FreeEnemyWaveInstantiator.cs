using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class FreeEnemyWaveInstantiator : MonoBehaviour
    {
        [SerializeField] private int _waveIndex = 0;
        private FreeEnemyWaveGenerator _freeEnemyWaveGenerator;
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;

        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
            _freeEnemyWaveGenerator = GetComponent<FreeEnemyWaveGenerator>();
        }
        public void CreateWave()
        {

        }
    }
}
