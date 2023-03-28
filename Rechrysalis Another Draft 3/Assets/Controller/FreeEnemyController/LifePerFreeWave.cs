using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;


namespace Rechrysalis.Unit
{
    public class LifePerFreeWave : MonoBehaviour
    {
        private ControllerHealth _controllerHealth;
        [SerializeField] private float _firstLevelFirstWaveLife = 5;
        [SerializeField] private float _extraLifePerWave = 1;
        [SerializeField] private float _extraLifePerLevelForFirstWave =1;
        [SerializeField] private float _lifeMultByLevelToAddToWave = 1.01f;

        public void Initialize()
        {
            _controllerHealth = GetComponent<ControllerHealth>();
        }
        public float GetLifeToSpendOnThisWave(int level, int wave)
        {
            float amount = _firstLevelFirstWaveLife;
            amount += (_extraLifePerLevelForFirstWave * level);
            amount += (_extraLifePerWave * wave);
            amount += ((_lifeMultByLevelToAddToWave * level * wave));
            if (amount > _controllerHealth.HealthCurrent)
            {
                amount = _controllerHealth.HealthCurrent;
            }

            return amount;
        }
        public float GetProgressMaxForThisWave(float progressValue, float progressMax, int level, int wave)
        {
            float amount = GetLifeToSpendOnThisWave(level, wave);
            if (amount > progressMax - progressValue)
            {
                amount = progressMax - progressValue;   
            }
            return amount;


        }
    }
}
