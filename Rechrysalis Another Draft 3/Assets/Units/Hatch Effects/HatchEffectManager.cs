using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectManager : MonoBehaviour
    {
        private HatchEffectSO _hatchEffectSO;
        private HETimer _hETimer;
        private HatchEffectHealth _hEHealth;
        private bool _affectAll = true;
        public bool AffectAll {get{return _affectAll;}}
        private HEDisplay _hEDisplay;
        [SerializeField] private TMP_Text _name;
        private float _maxHP;
        private float _currentHP;
        private float _hpDrainPerTick;
        private int _tier;

        public void Initialize(HatchEffectSO _hatchEffectSO, int _tier)
        {
            this._tier = _tier -1;
            Debug.Log($"Name ");
            this._hatchEffectSO = _hatchEffectSO;
            _hETimer = GetComponent<HETimer>();
            _name.text = _hatchEffectSO.HatchEffectName;
            _hEDisplay = GetComponent<HEDisplay>();
            _hEHealth = GetComponent<HatchEffectHealth>();
            _maxHP = _hatchEffectSO.HealthMax[_tier];
            _currentHP = _maxHP;
            _hpDrainPerTick = _hatchEffectSO.DamageLossPerTick[_tier];
        }
        public void SetOffset(int _multiplier)
        {
            _hEDisplay?.PositionOffset(_multiplier);
        }       
        public void Tick(float _timeAmount)
        {
            // _hEHealth?.TakeDamage(_timeAmount * _hpDrainPerTick);
            // if (!_hEHealth.CheckIfAlive())
            // {
            //     // Destroy(gameObject);
            // }
        } 
    }
}
