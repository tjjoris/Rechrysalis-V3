using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;

namespace Rechrysalis.Attacking
{
    public class Die : MonoBehaviour
    {
        private PlayerUnitsSO _playerUnits;
        private TargetsListSO _targetsList;
        private RemoveUnit _removeUnit;
        private CompsAndUnitsSO _compsAndUnitsSO;
        private int _controllerIndex;
        public Action _spawnWaveAction;
        public void Initialize(CompsAndUnitsSO _compsAndUbnitsSO, int _controllerIndex)        
        {
            _playerUnits = _compsAndUbnitsSO.PlayerUnits[_controllerIndex];
            _targetsList = _compsAndUbnitsSO.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)];
            _removeUnit = GetComponent<RemoveUnit>();
            this._compsAndUnitsSO = _compsAndUbnitsSO;
            this._controllerIndex = _controllerIndex;
        }
        public void UnitDies()
        {   
            // if (_playerUnits.ActiveUnits.Contains(gameObject))
            // {        
            //     _playerUnits.ActiveUnits.Remove(gameObject);
            // }
            // if (_targetsList.Targets.Contains(gameObject))
            // {
            //     _targetsList.Targets.Remove(gameObject);
            // }
            // gameObject.SetActive(false);
            _removeUnit.RemoveUnitFunction();
            if (_compsAndUnitsSO.PlayerUnits[_controllerIndex].ActiveUnits.Count <= 0)
            {
                Debug.Log($"next wave");
                _spawnWaveAction?.Invoke();
            }
        }
    }
}