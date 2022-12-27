using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectHealth : MonoBehaviour
    {
        private float _healthMultMult = 0.5f;
        private float _healthMult;
        [SerializeField] private float _HPMax = 30f;
        private float _HPCurrent;

        public void Initialize(float hatchMult)
        {
            // if (_healthMax != 0)
            // _healthMax = healthMax;
            _healthMult = ((hatchMult - 1f) * _healthMultMult) + 1;
            _HPMax *= _healthMult;
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
