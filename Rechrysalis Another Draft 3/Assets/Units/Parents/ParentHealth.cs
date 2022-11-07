using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.Unit
{
    public class ParentHealth : MonoBehaviour
    {
        private float _maxHealth;
        private float _currentHealth;
        private bool _isChrysalis;
        private float _chrysalisDefenceMult = 0.4f;
        private float _enemyControllerHealMult = 0.5f;
        public Action<int> _unitDies;
        public Action<float> _controllerTakeDamage;
        public Action<float> _enemyControllerHeal;

        public void Initialize()
        {
            // _parentUnitManager = GetComponent<ParentUnitManager>();
        }

        public void SetMaxHealth(float _maxHealth)
        {
            this._maxHealth = _maxHealth;
            _currentHealth = _maxHealth;
        }
        public void TakeDamage(float _damage)
        {
            float _damageToTake = _damage;
            if (_isChrysalis)
            {
                _damageToTake *= _chrysalisDefenceMult;
                _enemyControllerHeal?.Invoke(_damage * _enemyControllerHealMult);
            }
            _currentHealth -= _damageToTake;
            _controllerTakeDamage?.Invoke(_damageToTake);
            CheckIfDead();
        }
        public void SetChrysalis(bool _value)
        {
            _isChrysalis = _value;
        }
        private void CheckIfDead()
        {
            if (_currentHealth <= 0)
            {
                _unitDies?.Invoke(0);
            }
        }

    }
}
