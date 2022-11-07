using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
        private HEDisplay _hEDisplay;
        [SerializeField] private TMP_Text _name;
        // private float _maxHP;
        // private float _currentHP;
        private float _hpDrainPerTick;
        private int _tier;
        public Action<GameObject, int, int, bool> _hatchEffectDies;

        public void Initialize(HatchEffectSO _hatchEffectSO, int _tier, int _parentIndex, int _unitIndex, bool _affectAll)
        {
            this._parentIndex = _parentIndex;
            this._unitIndex = _unitIndex;
            this._affectAll = _affectAll;
            this._tier = _tier -1;
            Debug.Log($"Name ");
            this._hatchEffectSO = _hatchEffectSO;
            _hETimer = GetComponent<HETimer>();
            _name.text = _hatchEffectSO.HatchEffectName;
            _hEDisplay = GetComponent<HEDisplay>();
            _hEHealth = GetComponent<HatchEffectHealth>();
            if (_hatchEffectSO.HealthMax.Length > this._tier)
            {
            // _maxHP = _hatchEffectSO.HealthMax[_tier];
            _hEHealth.Initialize(_hatchEffectSO.HealthMax[_tier]);
            }
            // _currentHP = _maxHP;
            if (_hatchEffectSO.DamageLossPerTick.Length > _tier)
            {
            _hpDrainPerTick = _hatchEffectSO.DamageLossPerTick[this._tier];
            }
        }
        public void SetOffset(int _multiplier)
        {
            _hEDisplay?.PositionOffset(_multiplier);
        }       
        public void Tick(float _timeAmount)
        {
            _hEHealth?.TakeDamage(_timeAmount * _hpDrainPerTick);
            if (!_hEHealth.CheckIfAlive())
            {
                _hatchEffectDies?.Invoke(gameObject, _parentIndex, _unitIndex, _affectAll);
                // Destroy(gameObject);
            }
        } 
    }
}
