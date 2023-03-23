using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.UI;
using Rechrysalis.Unit;
using System;

namespace Rechrysalis.Controller
{

    public class ControllerHealth : MonoBehaviour
    {
        private bool _debugBool = false;
        [SerializeField] private float _healthMax;
        public float HealthMax => _healthMax;
        [SerializeField] private float _healthCurrent;
        public float HealthCurrent => _healthCurrent;
        [SerializeField] private ControllerHPBar _controllerHPBar;
        [SerializeField] private ControllerHPTokens _controllerHPTokens;
        [SerializeField] private ControllerHit _controllerHit;
        private GameObject[] _parentUnits;
        private List<GameObject> _allUnits;
        public Action _controllerTakesDamageAction;

        public void Initialize(float _healthMax, List<GameObject> _allUnits, CompsAndUnitsSO compsAndUnitsSO)
        {
            this._healthMax = _healthMax;
            _healthCurrent = _healthMax;
            this._allUnits = _allUnits;
            _controllerHPBar?.Initialize(this);
            // _controllerDeathGameOver = GetComponent<ControllerDeathGameOver>();
            SubscribeToControllerDamage();
            _controllerHPTokens?.Initialize(compsAndUnitsSO);
            _controllerHit = GetComponent<ControllerHit>();
        }
        public void TakeDamage(float _damageAmount)
        {
            if (_debugBool)
            {
                Debug.Log($"controller take damage " + _damageAmount);
            }
            _healthCurrent -= _damageAmount;
            RemoveHeartIfHealthZero();
            _controllerHPBar?.ChangeHPBar(_healthCurrent);
            _controllerTakesDamageAction?.Invoke();     
            _controllerHit?.ControllerIsHit();       
        }
        public void IncreaseMaxHealth(float amount)
        {
            _healthMax += amount;
            _healthCurrent += amount;
            _controllerHPBar?.ChangeHPBar(_healthCurrent);
        }
        private void RemoveHeartIfHealthZero()
        {
            if ((_healthCurrent <= 0) && (GetComponent<FreeEnemyInitialize>() == null))
            {
                _controllerHPTokens.RemoveToken();
                _healthCurrent = _healthMax;
            }            
        }
        public bool CheckIfHealthZero()
        {
            Debug.Log($"checking controller health " + _healthCurrent);
            if (_healthCurrent <= 0)
            {
                return true;
            }
            return false;
        }
        public void SetHealthToZero()
        {
            _healthCurrent = 0;
            _controllerHPBar?.ChangeHPBar(_healthCurrent);
        }
        public bool CheckIfHasEnoughHealth(float healthToCheck)
        {
            Debug.Log($"health current " + _healthCurrent + " health to check " + healthToCheck);
            if (_healthCurrent >= healthToCheck)
            {
                return true;
            }
            return false;
        }
        public void SubscribeToControllerDamage()        
        {            
            if ((_allUnits != null) && (_allUnits.Count > 0))
            {
                foreach (GameObject _unit in _allUnits)
                {
                    if (_unit != null)
                    {
                        DamagesController _damagesController = _unit.GetComponent<DamagesController>();
                        if (_damagesController != null)
                        {
                            // Debug.Log($"adding damages controller");
                            _damagesController._damagesControllerAction -= TakeDamage;
                            _damagesController._damagesControllerAction += TakeDamage;
                        }
                    }
                }
            }
        }
        public void SubscribeToParentUnits(GameObject[] _parentUnits)
        {
            this._parentUnits = _parentUnits;
            if (( _parentUnits != null) && (_parentUnits.Length > 0))
            {
                for (int _index = 0; _index < _parentUnits.Length; _index ++)
                {
                    if (_parentUnits[_index] != null)
                    {
                        _parentUnits[_index].GetComponent<ParentHealth>()._controllerTakeDamage -= TakeDamage;
                        _parentUnits[_index].GetComponent<ParentHealth>()._controllerTakeDamage += TakeDamage;
                    }
                }
            }
        }
        public void UnsubscribeToParentUnits()
        {
            if ((_parentUnits != null) && (_parentUnits.Length > 0))
            {
                for (int _index = 0; _index < _parentUnits.Length; _index++)
                {
                    _parentUnits[_index].GetComponent<ParentHealth>()._controllerTakeDamage -= TakeDamage;
                }
            }
        }
        private void OnEnable()
        {
            // SubscribeToControllerDamage();
            SubscribeToParentUnits(null);
        }
        private void OnDisable()
        {
            if (_allUnits.Count > 0)
            {
                foreach (GameObject _unit in _allUnits)
                {
                    if (_unit != null)
                    {
                        DamagesController _damagesController = _unit.GetComponent<DamagesController>();
                        if (_damagesController != null)
                        {
                            _damagesController._damagesControllerAction -= TakeDamage;
                        }
                    }
                }
            }            
        }

    }
}
