using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using Rechrysalis.UI;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ParentHealth : MonoBehaviour
    {
        bool _debugBool = true;
        private ParentUnitManager _parentUnitManager;
        public ParentUnitManager ParentUnitManager => _parentUnitManager;
        private ControllerManager _controllerManager;
        public ControllerManager ControllerManager => _controllerManager;
        private RecalculatePercentDPSTypesForController _recalculatePercentDPSTypesForController;
        private FreeEnemyInitialize _freeEnemyInitialize;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        public float CurrentHealth => _currentHealth;
        private bool _isChrysalis;
        public bool IsChrysalis => _isChrysalis;
        private float _incomingDamageMultbase = 0f;
        [SerializeField] private float _incomingDamageReductionMult = 0f;
        private float _chrysalisDefenceMult = 0.4f;
        private float _enemyControllerHealMult = 0.5f;
        private UnitManager _currentUnit;
        public UnitManager CurrentUnit {set {_currentUnit = value;} get {return _currentUnit;}}
        private Die _die;
        private BuildTimeFasterWithHigherHP _buildTimeFasterwithHigherHP;
        [SerializeField] private Transform _hpBarFill;
        [SerializeField] private SpriteRenderer _hpBarSprite;
        private Color[] _hpBarTintByTier = new Color[3];        
        public Action<int> _unitDies;
        public Action<float> _controllerTakeDamage;
        public Action<float> _enemyControllerHeal;

        private void Awake()
        {
            _parentUnitManager = GetComponent<ParentUnitManager>();
            _die = GetComponent<Die>();
            // _buildTimeFasterwithHigherHP = GetComponent<BuildTimeFasterWithHigherHP>();
        }
        public void Initialize()
        {
            // _parentUnitManager = GetComponent<ParentUnitManager>();
            _hpBarTintByTier[0] = new Color(1, 0.6f, 0, 1);
            _hpBarTintByTier[1] = new Color(1, 1, 0, 1);            
            _hpBarTintByTier[2] = new Color(0, 1, 0, 1);
            _controllerManager = _parentUnitManager.ControllerManager;
            _freeEnemyInitialize = _controllerManager.FreeEnemyInitialize;
            _buildTimeFasterwithHigherHP = GetComponent<BuildTimeFasterWithHigherHP>();
            _recalculatePercentDPSTypesForController = GameMaster.GetSingleton().ReferenceManager.CompsAndUnitsSO.ControllerManagers[GetOppositeController.ReturnOppositeController(_controllerManager.ControllerIndex)].GetComponent<RecalculatePercentDPSTypesForController>();
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
            float damageToTake = AddDamageIfChrysalis(_damage);
            damageToTake = AddDamageIfunit(damageToTake);            
            damageToTake = _damage * (1 - _incomingDamageReductionMult); 
           
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
            if (_debugBool) 
            { 
                if (_controllerManager.ControllerIndex == 1)
                {
                    Debug.Log($"take damage " + damageToTake);
                }
            }
            UpdateHpBar();
            // GetComponent<ParentUnitHatchEffects>()?.TakeDamage(_damage);
            CheckIfDead();
        }
        private float AddDamageIfChrysalis(float damage)
        {
            float damageOld = damage;
            if ((!_isChrysalis) || (_recalculatePercentDPSTypesForController == null)) return damage;
            damage = damage * (1 + _recalculatePercentDPSTypesForController.PercentDPSToChrysalis);
            // Debug.Log($"added chryaslis damage " + damage + " damage old " + damageOld);
            return damage;
        }
        private float AddDamageIfunit(float damage)
        {
            float damageOld = damage;
            if ((_isChrysalis) || (_recalculatePercentDPSTypesForController == null)) return damage;
            damage = damage * (1 + _recalculatePercentDPSTypesForController.PercentDPSToUnit);
            // if (_debugBool) Debug.Log($"added unit damage " + damage + " damage old " + damageOld);
            return damage;
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
                int childIndex = 0;
                if ((!PlayerPrefsInteract.GetHasBasicUnit()) && (_freeEnemyInitialize == null))
                {
                    childIndex = 1;
                }                
                if (_die == null)
                {
                    _unitDies?.Invoke(childIndex);
                } 
                else
                {
                    GetComponent<Die>()?.UnitDies();
                }
            }
        }
        public void ReCalculateIncomingDamageModifier(List<HEIncreaseDefence> hEIncreaseDefenceList)
        {
            _incomingDamageReductionMult = _incomingDamageMultbase;
            foreach (HEIncreaseDefence hatchEffect in hEIncreaseDefenceList)
            {
                if (hatchEffect != null)
                _incomingDamageReductionMult += hatchEffect.GetIncomingDamageMult();
            }
            if (_debugBool)
            {
                // Debug.Log($"incoming damage mult " + _incomingDamageReductionMult);
            }
        }
        private void UpdateHpBar()
        {
            // Debug.Log($"update hp bar" + _currentHealth / _maxHealth);  
            float healthRatio = GetHealthRatio();          
            Vector2 hpBarScale = new Vector2 (healthRatio, 1f);        
            if (_buildTimeFasterwithHigherHP != null)
            {
                TintHPBar(_buildTimeFasterwithHigherHP.GetBuildSpeedMultIndex(healthRatio));
            }    
            _hpBarFill.localScale = hpBarScale;
        }
        private void TintHPBar(int tier)
        {
            if ((_hpBarTintByTier.Length > tier) && (_hpBarTintByTier[tier] != null))
            {
                _hpBarSprite.color = _hpBarTintByTier[tier];
            }
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
            if (_maxHealth == _currentHealth)
            {
                return 0;
            }
            return ((_maxHealth - _currentHealth) / _maxHealth);
        }
    }
}
