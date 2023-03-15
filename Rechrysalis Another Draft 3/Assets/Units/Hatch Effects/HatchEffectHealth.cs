using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectHealth : MonoBehaviour
    {
        bool debugBool = false;
        private float _healthMultMult = 0.5f;
        private float _healthMult;
        [SerializeField] private float _HPMax = 30f;
        private float _HPCurrent;
        // [SerializeField] private float _durationToHealthMult = 1;

        public void Initialize(float hatchMult, UnitClass advUnit)
        {
            if (debugBool) Debug.Log($"hatch mult " + hatchMult);
            // if (_healthMax != 0)
            // _healthMax = healthMax;
            _healthMult = ((hatchMult - 1f) * _healthMultMult) + 1f;
            _HPMax *= _healthMult;
            _HPMax += advUnit.HatchEffectDurationAdd;
            _HPCurrent = _HPMax;
        }
        public void TakeDamage(float _damageAmount)
        {
            _HPCurrent -= _damageAmount;
        }
        // public void TakeDamage()
        public bool CheckIfAlive()
        {
            if (_HPCurrent > 0)
            {
                return true;
            }
            return false;
        }
    }
}
