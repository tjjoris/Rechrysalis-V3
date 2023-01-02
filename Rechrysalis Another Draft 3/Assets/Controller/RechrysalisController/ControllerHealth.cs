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
        [SerializeField] private float _healthMax;
        [SerializeField] private float _healthCurrent;
        [SerializeField] private ControllerHPBar _controllerHPBar;
        [SerializeField] private ControllerHPTokens _controllerHPTokens;
        // private ControllerDeathGameOver _controllerDeathGameOver;
        private GameObject[] _parentUnits;
        private List<GameObject> _allUnits;
        public Action _controllerTakesDamageAction;

        public void Initialize(float _healthMax, List<GameObject> _allUnits, CompsAndUnitsSO compsAndUnitsSO)
        {
            this._healthMax = _healthMax;
            _healthCurrent = _healthMax;
            this._allUnits = _allUnits;
            _controllerHPBar?.Initialize(_healthMax);
            // _controllerDeathGameOver = GetComponent<ControllerDeathGameOver>();
            SubscribeToControllerDamage();
            _controllerHPTokens.Initialize(compsAndUnitsSO);
        }
        public void TakeDamage(float _damageAmount)
        {
            _healthCurrent -= _damageAmount;
            CheckIfHealthZero();
            _controllerHPBar?.ChangeHPBar(_healthCurrent);
            _controllerTakesDamageAction?.Invoke();            
        }
        private void CheckIfHealthZero()
        {
            if (_healthCurrent <= 0)
            {
                _controllerHPTokens.RemoveToken();
                _healthCurrent = _healthMax;
            }            
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
