using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Controller;
using Rechrysalis.Movement;

namespace Rechrysalis.Attacking
{
    public class RemoveUnit : MonoBehaviour
    {
        private PlayerUnitsSO _playerUnits;
        private TargetsListSO _targetsList;
        private ControllerManager _controllerManager;
        private ParentUnitManager _parentUnitManager;
        private ParentHealth _parentHealth;
        private Mover _parentMover;


        public void Initialize(PlayerUnitsSO _playerUnits, TargetsListSO _targetsList, ControllerManager controllerManager, ParentUnitManager parentUnitManager)
        {
            this._playerUnits = _playerUnits;
            this._targetsList = _targetsList;
            _controllerManager = controllerManager;
            _parentUnitManager = parentUnitManager;
            _parentHealth = parentUnitManager.GetComponent<ParentHealth>();
            _parentMover = parentUnitManager.GetComponent<Mover>();
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
            if (_controllerManager.ParentHealths.Contains(_parentHealth))
            {
                _controllerManager.ParentHealths.Remove(_parentHealth);                
            }
            if (_controllerManager.ParentUnitManagers.Contains(_parentUnitManager))
            {
                _controllerManager.ParentUnitManagers.Remove(_parentUnitManager);
            }
            if (_controllerManager.ParentUnitMovers.Contains(_parentMover))
            {
                _controllerManager.ParentUnitMovers.Remove(_parentMover);
            }
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);    
            }
        }
    }
}
