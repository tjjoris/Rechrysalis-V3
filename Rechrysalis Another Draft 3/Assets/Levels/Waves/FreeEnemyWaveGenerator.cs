using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class FreeEnemyWaveGenerator : MonoBehaviour
    {
        [SerializeField] private List<WaveClass> _waveClassList;
        public List<WaveClass> WaveClassList { get => _waveClassList; set => _waveClassList = value; }
        private ControllerHealth _controllerHealth;
        private RandomizeFreeChangingUnits _randomFreeChangingUnits;
        private float _lifeInThisWave;

        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            _controllerHealth = GetComponent<ControllerHealth>();
            _randomFreeChangingUnits = GetComponent<RandomizeFreeChangingUnits>();
        }
        public void GenerateWaves()
        {
        _waveClassList = new List<WaveClass>();
            float progressValue = _controllerHealth.HealthMax;
            // while (progressValue >= 0)
            {
                WaveClass waveClass = new WaveClass();
        _waveClassList.Add(waveClass);
                waveClass.UnitsInWave = new List<ParentUnitClass>();
            }
        }
        private UnitStatsSO GetUnitInWave()
        {
            
            return null;
        }
    }
}
