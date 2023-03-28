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
        private WaveLayoutsByRange _waveLayoutByRange;
        private FreeEnemyInitialize _freeEnemyInitialize;

        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
            _freeEnemyWaveGenerator = GetComponent<FreeEnemyWaveGenerator>();
            _freeEnemyInitialize = GetComponent<FreeEnemyInitialize>();
            _waveLayoutByRange = _freeEnemyInitialize.WaveLayoutsByRange;
        }
        public void CreateWave()
        {
            if (_freeEnemyWaveGenerator.WaveClassList.Count > _waveIndex)
            {
                if (_freeEnemyWaveGenerator.WaveClassList[_waveIndex].UnitsInWave.Count > 0)
                {
                    foreach (ParentUnitClass parentUnitClass in _freeEnemyWaveGenerator.WaveClassList[_waveIndex].UnitsInWave)
                    {

                    }
                }
            }
        }
    }
}
