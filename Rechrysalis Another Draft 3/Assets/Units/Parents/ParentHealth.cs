using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    public class ParentHealth : MonoBehaviour
    {
        private float _maxHealth;
        private float _currentHealth;
        private bool _isChrysalis;
        private float _incomingDamageMultbase = 1f;
        private float _incomingDamageMult = 1f;
        private float _chrysalisDefenceMult = 0.4f;
        private float _enemyControllerHealMult = 0.5f;
        private UnitManager _currentUnit;
        public UnitManager CurrentUnit {set {_currentUnit = value;} get {return _currentUnit;}}
        private Die _die;
        public Action<int> _unitDies;
        public Action<float> _controllerTakeDamage;
        public Action<float> _enemyControllerHeal;

        public void Initialize()
        {
            _die = GetComponent<Die>();
            // _parentUnitManager = GetComponent<ParentUnitManager>();
        }

        public void SetMaxHealth(float _maxHealth)
        {
            this._maxHealth = _maxHealth;
            _currentHealth = _maxHealth;
        }
        public void TakeDamage(float _damage)
        {
            // float _damageToTake = _damage * _currentUnit.GetIncomingDamageMultiplier();            
            float _damageToTake = _damage * _incomingDamageMult;            
            if (_isChrysalis)
            {
                _damageToTake *= _chrysalisDefenceMult;
                _enemyControllerHeal?.Invoke(_damage * _enemyControllerHealMult);
            }
            _currentHealth -= _damageToTake;
            _controllerTakeDamage?.Invoke(_damageToTake);
            GetComponent<ParentUnitHatchEffects>()?.TakeDamage(_damage);
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
                GetComponent<Die>()?.UnitDies();
            }
        }
        public void ReCalculateIncomingDamageModifier(List<HEIncreaseDefence> hEIncreaseDefenceList)
        {
            foreach (HEIncreaseDefence hatchEffect in hEIncreaseDefenceList)
            {
                if (hatchEffect != null)
                _incomingDamageMult = _incomingDamageMultbase * hatchEffect.GetIncomingDamageMult();
            }
            Debug.Log($"incoming damage mult " + _incomingDamageMult);
        }
    }
}
