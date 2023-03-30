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
        bool _debugBool = false;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        public float CurrentHealth => _currentHealth;
        private bool _isChrysalis;
        public bool IsChrysalis => _isChrysalis;
        private float _incomingDamageMultbase = 0f;
        private float _incomingDamageMult = 0f;
        private float _chrysalisDefenceMult = 0.4f;
        private float _enemyControllerHealMult = 0.5f;
        private UnitManager _currentUnit;
        public UnitManager CurrentUnit {set {_currentUnit = value;} get {return _currentUnit;}}
        private Die _die;
        [SerializeField] private Transform _hpBarFill;
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
            UpdateHpBar();
        }
        public void SetCurrentHealth(float currentHealth)
        {
            _currentHealth = currentHealth;
            UpdateHpBar();
        }
        public void TakeDamage(float _damage)
        {
            // float _damageToTake = _damage * _currentUnit.GetIncomingDamageMultiplier();            
            float damageToTake = _damage * (1 - _incomingDamageMult);            
            if ((_isChrysalis) && (_die == null))
            {
                damageToTake *= (_chrysalisDefenceMult);
                _enemyControllerHeal?.Invoke(_damage * _enemyControllerHealMult);
                _controllerTakeDamage?.Invoke(damageToTake);
            }
            else
            {
                _currentHealth -= damageToTake;
            }
            if (_debugBool) Debug.Log($"take damage " + damageToTake);
            UpdateHpBar();
            GetComponent<ParentUnitHatchEffects>()?.TakeDamage(_damage);
            CheckIfDead();
        }
        public void Heal(float healAmount)
        {
            if (!_isChrysalis)
            {
                _currentHealth += healAmount;
                if (_currentHealth >= _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
                UpdateHpBar();
            }
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
            _incomingDamageMult = _incomingDamageMultbase;
            foreach (HEIncreaseDefence hatchEffect in hEIncreaseDefenceList)
            {
                if (hatchEffect != null)
                _incomingDamageMult += hatchEffect.GetIncomingDamageMult();
            }
            Debug.Log($"incoming damage mult " + _incomingDamageMult);
        }
        private void UpdateHpBar()
        {
            // Debug.Log($"update hp bar" + _currentHealth / _maxHealth);            
            Vector2 hpBarScale = new Vector2 (GetHealthRatio(), 1f);
            _hpBarFill.localScale = hpBarScale;
        }
        public float GetHealthRatio()
        {
            if (_currentHealth <= 0)
            {
                return 0;
            }
            return (_currentHealth / _maxHealth);
        }
        public float GetHealthMissingRatio()
        {
            return ((_maxHealth - _currentHealth) / _maxHealth);
        }
    }
}
