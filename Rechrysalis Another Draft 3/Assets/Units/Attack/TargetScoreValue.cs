using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class TargetScoreValue : MonoBehaviour
    {
        [SerializeField] private float _currentScoreValue;
        public float CurrentScoreValue { get => _currentScoreValue; set => _currentScoreValue = value; }
        
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
        private PriorityScoreChrysalis _priorityScoreChrysalis;
        private PriorityScoreDPS _priorityScoreDPS;
        private PriorityScoreHatchEffect _priorityScoreHatchEffect;
        
        public void Initialize()
        {
            _parentUnitHatchEffects = GetComponent<ParentUnitHatchEffects>();
            _parentHealth = GetComponent<ParentHealth>();
            _priorityScoreChrysalis = GetComponent<PriorityScoreChrysalis>();
            _priorityScoreDPS = GetComponent<PriorityScoreDPS>();
            _priorityScoreHatchEffect = GetComponent<PriorityScoreHatchEffect>();
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
            EggScore();
        }
        private void EggScore()
        {
            if (_isEgg)
            {
                _eggValue = 100;
            }
            else
            {
                _eggValue = 0;
            }
        }
        private void HatchScore()
        {
            // if (_parentUnitHatchEffects.);
        }
        public void CalculateScoreValue()
        {
            _currentScoreValue = 0;
            if (_priorityScoreChrysalis != null)
            {
                _currentScoreValue += _priorityScoreChrysalis.GenerateScore();
            }
            if (_priorityScoreDPS != null)
            {
                _currentScoreValue += _priorityScoreDPS.GenerateScore();                
            }
            if (_priorityScoreHatchEffect != null)
            {
                _currentScoreValue += _priorityScoreHatchEffect.GenerateScore();
            }
        }
    }
}
