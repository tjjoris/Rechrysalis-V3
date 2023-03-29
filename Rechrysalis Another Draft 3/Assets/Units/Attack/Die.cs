using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.UI;
using System;

namespace Rechrysalis.Attacking
{
    public class Die : MonoBehaviour
    {
        private bool _debugBool = false;
        private PlayerUnitsSO _playerUnits;
        private TargetsListSO _targetsList;
        private RemoveUnit _removeUnit;
        private CompsAndUnitsSO _compsAndUnitsSO;
        private int _controllerIndex;
        private ParentHealth _parentHealth;
        private UnitActivation _unitActivation;
        private ChrysalisActivation _chrysalisActivation;
        public Action _spawnWaveAction;
        [SerializeField] private float _controllerProgressValue;
        public float ControllerProgressValue {get  => _controllerProgressValue; set => _controllerProgressValue = value;}
        [SerializeField] private FreeControllerControllerProgressBar _freeControllerProgressBar;
        public FreeControllerControllerProgressBar FreeControllerProgressBar {get => _freeControllerProgressBar; set => _freeControllerProgressBar = value;}

        public void Initialize(CompsAndUnitsSO _compsAndUbnitsSO, int _controllerIndex)
        {
            _playerUnits = _compsAndUbnitsSO.PlayerUnits[_controllerIndex];
            _targetsList = _compsAndUbnitsSO.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)];
            _removeUnit = GetComponent<RemoveUnit>();
            this._compsAndUnitsSO = _compsAndUbnitsSO;
            this._controllerIndex = _controllerIndex;
            _parentHealth = GetComponent<ParentHealth>();
            _unitActivation = GetComponent<UnitActivation>();
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
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
            if (!_parentHealth.IsChrysalis)
            {
                _chrysalisActivation.ActivateChrysalis(0);
            }
            else
            if (gameObject.activeInHierarchy)
            {
                _freeControllerProgressBar?.AddProgress(_controllerProgressValue);
                _removeUnit.RemoveUnitFunction();
                // if (_compsAndUnitsSO.PlayerUnits[_controllerIndex].ActiveUnits.Count <= 0)
                {
                    if (_debugBool)
                    {
                        Debug.Log($"next wave");
                    }
                    _spawnWaveAction?.Invoke();
                }
            }
        }
    }
}
