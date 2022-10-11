using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class RemoveUnit : MonoBehaviour
    {
        private PlayerUnitsSO _playerUnits;
        private TargetsListSO _targetsList;

        public void Initialize(PlayerUnitsSO _playerUnits, TargetsListSO _targetsList)
        {
            this._playerUnits = _playerUnits;
            this._targetsList = _targetsList;
        }
        public void RemoveUnitFunction()
        {
        if (_playerUnits.ActiveUnits.Contains(gameObject))
            {        
                _playerUnits.ActiveUnits.Remove(gameObject);
            }
            if (_targetsList.Targets.Contains(gameObject))
            {
                _targetsList.Targets.Remove(gameObject);
            }
            gameObject.SetActive(false);    
        }
    }
}
