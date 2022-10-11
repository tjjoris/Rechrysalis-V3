using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class Die : MonoBehaviour
    {
        private PlayerUnitsSO _playerUnits;
        private TargetsListSO _targetsList;
        private RemoveUnit _removeUnit;
        public void Initialize(CompsAndUnitsSO _compsAndUbnitsSO, int _unitIndex)        
        {
            _playerUnits = _compsAndUbnitsSO.PlayerUnits[_unitIndex];
            _targetsList = _compsAndUbnitsSO.TargetsLists[GetOppositeController.ReturnOppositeController(_unitIndex)];
            _removeUnit = GetComponent<RemoveUnit>();
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
        }
    }
}
