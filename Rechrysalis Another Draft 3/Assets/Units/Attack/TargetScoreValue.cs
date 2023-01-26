using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class TargetScoreValue : MonoBehaviour
    {
        [SerializeField] private bool _isEgg;
        [SerializeField] private float _eggValue;
        public float EggValue { get => _eggValue; set => _eggValue = value; }
        [SerializeField] private float _hatchEffectValue;
        public float HatchEffectValue { get => _hatchEffectValue; set => _hatchEffectValue = value; }
        [SerializeField] private float _dpsValue;
        public float DpsValue { get => _dpsValue; set => _dpsValue = value; }
        private ParentUnitHatchEffects _parentUnitHatchEffects;
        private ParentHealth _parentHealth;
        private Attack _currentUnitAttack;
        
        public void Initialize()
        {
            _parentUnitHatchEffects = GetComponent<ParentUnitHatchEffects>();
            _parentHealth = GetComponent<ParentHealth>();
        }
        public void SetCurrentUnit(Attack attack)
        {
            _currentUnitAttack = attack;
            SetEgg(false);
        }
        public void ReCalculateScore()
        {
            _dpsValue = _currentUnitAttack.CurrentDPS;
        }
        public void SetEgg(bool isEgg)
        {
            _isEgg = isEgg;
            if (_isEgg)
            {
                _eggValue = 100;
            }
            else 
            {
                _eggValue = 0;
            }
        }
    }
}
