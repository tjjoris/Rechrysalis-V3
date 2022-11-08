using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class ClosestTarget : MonoBehaviour
    {
        [SerializeField] private PlayerUnitsSO _enemyUnits;
        private Range _range;

        public void Initialize (PlayerUnitsSO _enemyUnits)
        {
            this._enemyUnits = _enemyUnits;            
            _range = GetComponent<Range>();
        }
        public GameObject GetNearestEnemyInRange()
        {
            if ((_enemyUnits.ParentUnits.Count > 0) && (_range != null))
            {
                float _rangeOfUnit = _range.GetRange();
                bool _unitInRange = false;
                GameObject _closestUnitChecked = null;
                foreach (GameObject _targetToCheck in _enemyUnits.ParentUnits)
                {
                    float _distToUnitChecking = (_targetToCheck.transform.position - gameObject.transform.position).magnitude;
                    if ((_distToUnitChecking <= _rangeOfUnit))
                    {   
                        _unitInRange = true;
                        _closestUnitChecked = _targetToCheck;
                        _rangeOfUnit = _distToUnitChecking;
                    }
                }
                if (_unitInRange)
                {
                    return _closestUnitChecked;
                }
            }
            return null;            
        }
        public GameObject GetNearestEnemy()
        {
            GameObject _unit = null;
            if ((_enemyUnits.ParentUnits.Count > 0))
            {
                float _rangeToCompare = 9999;
                for (int _index = 0; _index < _enemyUnits.ParentUnits.Count; _index ++ )
                {
                    if (_unit == null)
                    {
                        _unit = _enemyUnits.ParentUnits[_index];
                    }
                    float _rangeFound = Mathf.Abs((_enemyUnits.ParentUnits[_index].transform.position - _unit.transform.position).magnitude);
                    if (_rangeFound < _rangeToCompare)
                    {
                        _unit = _enemyUnits.ParentUnits[_index];
                        _rangeToCompare = _rangeFound;
                    }
                }
            }
            return _unit;
        }
    }
}
