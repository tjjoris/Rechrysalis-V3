using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class FreeEnemyWaveGenerator : MonoBehaviour
    {
        private CompsAndUnitsSO _compsAndUnitsSO;
        private LifePerFreeWave _lifePerFreeWave;
        [SerializeField] private List<WaveClass> _waveClassList;
        public List<WaveClass> WaveClassList { get => _waveClassList; set => _waveClassList = value; }
        private ControllerHealth _controllerHealth;
        private RandomizeFreeChangingUnits _randomFreeChangingUnits;
        // private float _lifeInThisWave;
        [SerializeField] private float _progressMaxForThisLevel;
        public float ProgressMaxForThisLevel { get => _progressMaxForThisLevel; set => _progressMaxForThisLevel = value; }
        
        private void Awake()
        {
            Initialize(null);
        }
        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _controllerHealth = GetComponent<ControllerHealth>();
            _randomFreeChangingUnits = GetComponent<RandomizeFreeChangingUnits>();
            _lifePerFreeWave = GetComponent<LifePerFreeWave>();
            if (compsAndUnitsSO != null)
            {
                _compsAndUnitsSO = compsAndUnitsSO;
            }
        }
        public void GenerateWaves()
        {
            int wave = 0;
            float progressCost = 0;
            float progressValueMax = _controllerHealth.HealthMax;
            _waveClassList = new List<WaveClass>();
            // while (progressValue >= 0)
            {
                GenerateWave(ref progressCost, ref wave);   
                _progressMaxForThisLevel = progressCost;             
            }
        }
        private bool GenerateWave(ref float progressCost, ref int wave)
        {

            float progressMaxForThisWave = _lifePerFreeWave.GetLifeToSpendOnThisWave(_compsAndUnitsSO.Level, wave);
            float progressCostForThisWave = 0;
            WaveClass waveClass = new WaveClass();
            
            waveClass.UnitsInWave = new List<ParentUnitClass>();
            GenerateUnit(waveClass, ref progressCostForThisWave, progressMaxForThisWave);
            progressCost += progressCostForThisWave;
            if (waveClass.UnitsInWave.Count > 0)
            {
                _waveClassList.Add(waveClass);
                wave++;
                GenerateWave(ref progressCost, ref wave);
            }
            waveClass.ProgressValueOfWave = progressCost;
            return false;
        }
        private bool GenerateUnit(WaveClass waveClass, ref float progressCostForThisWave, float progressMaxForThisWave)
        {
            ParentUnitClass unitForWave = _randomFreeChangingUnits.GetARandomParentUnitClassFromChangingsBasedOnLifeAmount(progressMaxForThisWave - progressCostForThisWave);
            if (unitForWave != null)
            {
                waveClass.UnitsInWave.Add(unitForWave);
                progressCostForThisWave += unitForWave.BasicUnitClass.ControllerLifeCostMult;
                GenerateUnit(waveClass, ref progressCostForThisWave, progressMaxForThisWave);
            }
            else
            {

            }
            return false;
        }
    }
}
