using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.UI;

namespace Rechrysalis.Controller
{
    public class ManaGenerator : MonoBehaviour
    {
        [SerializeField] private ManaDisplay _manaDisplay;
        private float _manaCurrent;
        private float _manaCooldownTimerCurrent = 3;
        private float _manaCooldownTimerMax = 2f;
        private float _generateIntervalCurrent;
        private float _generateIntervalMax = 2f;
        private float _generateAmount = 5;
        private bool _generatingMana;
        private void Start() {
            _manaDisplay.SetManaNumber(_manaCurrent);
        }
        public void StartTimer()
        {
            Debug.Log($"reset timer");
            _manaCooldownTimerCurrent = 0;
        }
        public void Tick(float _timeAmount)
        {
            if (_manaCooldownTimerCurrent < _manaCooldownTimerMax)
            {
                _generatingMana = true;
                _manaCooldownTimerCurrent += _timeAmount;
                _generateIntervalCurrent += _timeAmount;
            }
            else
            {
                _generatingMana = false;
            }
            if ((_generatingMana) && (_generateIntervalCurrent >= _generateIntervalMax))
            {
                _generateIntervalCurrent = 0;
                _manaCurrent+= _generateAmount;
                _manaDisplay.SetManaNumber(_manaCurrent);
            }
        }
    }
}
