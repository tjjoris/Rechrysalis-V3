using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Controller;

namespace Rechrysalis.Attacking
{
    public class RemoveUnit : MonoBehaviour
    {
        private PlayerUnitsSO _playerUnits;
        private TargetsListSO _targetsList;
        private ControllerManager _controllerManager;
        private ParentUnitManager _parentUnitManager;


        public void Initialize(PlayerUnitsSO _playerUnits, TargetsListSO _targetsList, ControllerManager controllerManager, ParentUnitManager parentUnitManager)
        {
            this._playerUnits = _playerUnits;
            this._targetsList = _targetsList;
            _controllerManager = controllerManager;
            _parentUnitManager = parentUnitManager;
        }
        public void RemoveUnitFunction()
        {
            if (_playerUnits.ParentUnits.Contains(gameObject))
            {
                _playerUnits.ParentUnits.Remove(gameObject);
            }
        if (_playerUnits.ActiveUnits.Contains(gameObject))
            {        
                _playerUnits.ActiveUnits.Remove(gameObject);
            }
            if (_targetsList.Targets.Contains(gameObject))
            {
                _targetsList.Targets.Remove(gameObject);
            }            
            // ParentHealth parentHealth = 
            // if (_controllerManager.ParentHealths.Contains(`))
            gameObject.SetActive(false);    
        }
    }
}
