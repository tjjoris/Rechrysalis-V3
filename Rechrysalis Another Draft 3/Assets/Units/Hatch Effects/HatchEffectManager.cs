using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectManager : MonoBehaviour
    {
        private int _parentIndex;
        private int _unitIndex;
        private HatchEffectSO _hatchEffectSO;
        private HETimer _hETimer;
        private HatchEffectHealth _hEHealth;
        private bool _affectAll = true;
        public bool AffectAll {get{return _affectAll;}}
        private float _dPSIncrease;
        public float DPSIncrease {get {return _dPSIncrease;}}
        private float _incomingDamageMult = 1;
        public float IncomingDamageMult {get{return _incomingDamageMult;}}
        [SerializeField] private float _hatchMult;
        public float HatchMult => _hatchMult;
        [SerializeField] private float _hatchDurationMult;
        private HEDisplay _hEDisplay;
        [SerializeField] private TMP_Text _name;
        // private float _maxHP;
        // private float _currentHP;
        private float _hpDrainPerTick;
        private int _tier;
        [SerializeField] private HEIncreaseDamage _hEIncreaseDamage;
        public HEIncreaseDamage HEIncreaseDamage => _hEIncreaseDamage;
        [SerializeField] private HEIncreaseDefence _hEIncreaseDefence;
        public HEIncreaseDefence HEIncreaseDefence => _hEIncreaseDefence;
        [SerializeField] private UnitClass _unitClass;
        public Action<GameObject, int, int, bool> _hatchEffectDies;

        public void Initialize(HatchEffectSO _hatchEffectSO, int _parentIndex, int _unitIndex, bool _affectAll, UnitClass advUnitClass)
        {
            _unitClass = advUnitClass;
            Debug.Log($"HE Initialize " + _hatchEffectSO.HatchEffectName +  " tier " + _tier);
            this._parentIndex = _parentIndex;
            this._unitIndex = _unitIndex;
            this._affectAll = _affectAll;
            // this._tier = _tier;
            this._hatchEffectSO = _hatchEffectSO;
            _hETimer = GetComponent<HETimer>();
            // _name.text = _hatchEffectSO.HatchEffectName;
            _hEDisplay = GetComponent<HEDisplay>();
            _hEHealth = GetComponent<HatchEffectHealth>();
            // if (_hatchEffectSO.HealthMax.Length > this._tier)
            {
            // _maxHP = _hatchEffectSO.HealthMax[_tier];
            _hatchMult = advUnitClass.HatchEffectMult;
            _hatchDurationMult = advUnitClass.HatchEffectDurationAdd;
            _hEHealth.Initialize(_hatchMult, advUnitClass);
            }
            // _currentHP = _maxHP;
            if (_hatchEffectSO.DamageLossPerTick.Length > this._tier)
            {
            _hpDrainPerTick = _hatchEffectSO.DamageLossPerTick[this._tier];
            }
            if (_hatchEffectSO.DPSIncrease.Length > this._tier)
            {
                _dPSIncrease = _hatchEffectSO.DPSIncrease[this._tier];
            }
            if (_hatchEffectSO.IncomingDamageMultiplier.Length > this._tier)
            {
                _incomingDamageMult = _hatchEffectSO.IncomingDamageMultiplier[this._tier];
            }
            _hEIncreaseDamage = GetComponent<HEIncreaseDamage>();
            _hEIncreaseDamage?.Initialize(_hatchMult);
            _hEIncreaseDefence = GetComponent<HEIncreaseDefence>();
            _hEIncreaseDefence?.Initialize(_hatchMult);

        }
        public void SetOffset(int _multiplier)
        {
            _hEDisplay?.PositionOffset(_multiplier);
        }       
        public void Tick(float _timeAmount)
        {
            TakeDamage(_timeAmount * _hpDrainPerTick);
        } 
        public void TakeDamage(float _damageAmount)
        {
            _hEHealth?.TakeDamage(_damageAmount);
            if (!_hEHealth.CheckIfAlive())
            {
                _hatchEffectDies?.Invoke(gameObject, _parentIndex, _unitIndex, _affectAll);
            }            
        }
    }
}
