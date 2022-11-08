using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.UI;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class ManaGenerator : MonoBehaviour
    {
        [SerializeField] private ManaDisplay _manaDisplay;
        private float _manaCurrent;
        private float _manaCooldownTimerCurrent = 3;
        private float _manaCooldownTimerMax = 2f;
        private float _generateIntervalCurrent;
        private float _generateIntervalMax = 0.5f;
        private float _generateAmount = 5;
        private bool _generatingMana;
        private GameObject[] _parentUnits;
        // private void Start() {
        //     _manaDisplay.SetManaNumber(_manaCurrent);
        // }
        public void Initialize(GameObject[] _parentUnits)
        {
            _manaDisplay.SetManaNumber(_manaCurrent);
            this._parentUnits = _parentUnits;
            SubscribeToParentUnits();
        }
        private void SubscribeToParentUnits()
        {
            if ((_parentUnits != null) && (_parentUnits.Length > 0))
            {
                for (int _index = 0; _index < _parentUnits.Length; _index++)
                if (_parentUnits[_index] != null)
                {
                    _parentUnits[_index].GetComponent<ParentUnitManager>()._subtractMana -= SubtractMana;
                    _parentUnits[_index].GetComponent<ParentUnitManager>()._subtractMana += SubtractMana;
                }
            }
        }
        private void OnEnable()
        {
            SubscribeToParentUnits();
        }
        private void OnDisable()
        {
            if ((_parentUnits != null) && (_parentUnits.Length > 0))
            {
                for (int _index = 0; _index < _parentUnits.Length; _index++)
                    if (_parentUnits[_index] != null)
                    {
                        _parentUnits[_index].GetComponent<ParentUnitManager>()._subtractMana -= SubtractMana;
                    }
            }
            
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
                // _manaCurrent+= _generateAmount;
                SetMana(_manaCurrent + _generateAmount);
                _manaDisplay.SetManaNumber(_manaCurrent);
            }
        }
        private void SetMana(float _amount)
        {
            _manaCurrent = _amount;
            if ((_parentUnits != null) && (_parentUnits.Length > 0))
            {
                for (int _index=0; _index < _parentUnits.Length; _index++)
                {
                    if (_parentUnits[_index] != null)
                    {
                        _parentUnits[_index].GetComponent<ParentUnitManager>().ManaAmount = _amount;
                    }
                }
            }
        }
        private void SubtractMana(float _amount)
        {
            SetMana(_manaCurrent -= _amount);
        }
    }
}
