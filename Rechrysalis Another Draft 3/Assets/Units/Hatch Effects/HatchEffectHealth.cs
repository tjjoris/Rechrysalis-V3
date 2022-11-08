using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectHealth : MonoBehaviour
    {
        private float _healthMax;
        private float _healthCurrent;

        public void Initialize(float _healthMax)
        {
            this._healthMax = _healthMax;
            _healthCurrent = _healthMax;
        }
        public void TakeDamage(float _damageAmount)
        {
            _healthCurrent -= _damageAmount;
        }
        // public void TakeDamage()
        public bool CheckIfAlive()
        {
            if (_healthCurrent > 0)
            {
                return true;
            }
            return false;
        }
    }
}
