using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class FreeEnemyWaveGenerator : MonoBehaviour
    {
        private CompsAndUnitsSO _compsAndUnitsSO;
        private FreeEnemyInitialize _freeEnemyInitialize;
        private WaveLayoutsByRange _waveLayoutsByRange;
        private LifePerFreeWave _lifePerFreeWave;
        [SerializeField] private List<WaveClass> _waveClassList;
        public List<WaveClass> WaveClassList { get => _waveClassList; set => _waveClassList = value; }
        private ControllerHealth _controllerHealth;
        private RandomizeFreeChangingUnits _randomFreeChangingUnits;
        // private float _lifeInThisWave;
        [SerializeField] private float _progressMaxForThisLevel;
        public float ProgressMaxForThisLevel { get => _progressMaxForThisLevel; set => _progressMaxForThisLevel = value; }
        
        
        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _freeEnemyInitialize = GetComponent<FreeEnemyInitialize>();
            _waveLayoutsByRange = _freeEnemyInitialize.WaveLayoutsByRange;
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
            GenerateWave(ref progressCost, ref wave, progressValueMax);   
            _progressMaxForThisLevel = progressCost; 
        }
        private bool GenerateWave(ref float progressCost, ref int wave, float progressValueMax)
        {
            float progressMaxForThisWave = _lifePerFreeWave.GetProgressMaxForThisWave(progressCost, progressValueMax, _compsAndUnitsSO.Level, wave);

            Debug.Log($"generarte wave progress cost " + progressCost + " wave " + wave + " progess value max " + progressValueMax + " max for this wave " + progressMaxForThisWave);
            float progressCostForThisWave = 0;
            WaveClass waveClass = new WaveClass();
            
            waveClass.UnitsInWave = new List<ParentUnitClass>();
            float focusFireCostThisWave = progressMaxForThisWave * _compsAndUnitsSO.RatioOfFreeUnitsToBeFF;
            GenerateUnit(waveClass, ref progressCostForThisWave, progressMaxForThisWave, focusFireCostThisWave);
            progressCost += progressCostForThisWave;

            if ((waveClass.UnitsInWave.Count > 0) && (waveClass.UnitsInWave[0] != null))
            {
                _waveClassList.Add(waveClass);
                waveClass.ProgressValueOfWave = progressCostForThisWave;
                wave++;
                GenerateWave(ref progressCost, ref wave, progressValueMax);
            }
            return false;
        }
        private bool GenerateUnit(WaveClass waveClass, ref float progressCostForThisWave, float progressMaxForThisWave, float focusFireCostThisWave)
        {
            if (waveClass.UnitsInWave.Count < _waveLayoutsByRange.WaveLayouts[0].UnitInWave.Length)
            {
                // ParentUnitClass unitForWave = _randomFreeChangingUnits.GetARandomNonFFParentUnitClassBasedOnControllerLife(progressMaxForThisWave - progressCostForThisWave);
                // if (unitForWave != null)
                // {
                //     Debug.Log($"unit generated " + unitForWave.BasicUnitClass.UnitName);
                //     waveClass.UnitsInWave.Add(unitForWave);
                //     progressCostForThisWave += unitForWave.BasicUnitClass.ControllerLifeCostMult;
                bool didGenerateFFUnit = GenerateFFUnit(waveClass, ref progressCostForThisWave, ref focusFireCostThisWave);
                if (didGenerateFFUnit)
                {
                    GenerateUnit(waveClass, ref progressCostForThisWave, progressMaxForThisWave, focusFireCostThisWave);
                }
                else
                {
                    Debug.Log($"ff cost exceeded");
                    bool didGenerateNonFFUnit = GenerateNonFFUnit(waveClass, ref progressCostForThisWave, ref progressMaxForThisWave);
                    if (didGenerateNonFFUnit)
                    {
                        GenerateUnit(waveClass, ref progressCostForThisWave, progressMaxForThisWave, focusFireCostThisWave);
                    }
                    else{
                        Debug.Log($"unit cost exceeded");
                    }
                }
            }
            else 
            {
                Debug.Log($"wave class units in wave count " + waveClass.UnitsInWave.Count.ToString());
            }
            return false;            
        }
        private bool GenerateNonFFUnit(WaveClass waveClass, ref float progressCostForThisWave, ref float progressMaxForThisWave)
        {

            ParentUnitClass unitForWave = _randomFreeChangingUnits.GetARandomNonFFParentUnitClassBasedOnControllerLife(progressMaxForThisWave - progressCostForThisWave);
            if (unitForWave != null)
            {
                Debug.Log($"unit generated " + unitForWave.BasicUnitClass.UnitName);
                waveClass.UnitsInWave.Add(unitForWave);
                progressCostForThisWave += unitForWave.BasicUnitClass.ControllerLifeCostMult;
                return true;
            }
            return false;
        }
        private bool GenerateFFUnit(WaveClass waveClass, ref float progressCostForThisWave, ref float progressMaxForThisWave)
        {

            ParentUnitClass unitForWave = _randomFreeChangingUnits.GetARandomFFParentUnitClassBasedOnControllerLife(progressMaxForThisWave - progressCostForThisWave);
            if (unitForWave != null)
            {
                Debug.Log($"unit generated " + unitForWave.BasicUnitClass.UnitName);
                waveClass.UnitsInWave.Add(unitForWave);
                progressCostForThisWave += unitForWave.BasicUnitClass.ControllerLifeCostMult;
                return true;
            }
            return false;
        }
    }
}
