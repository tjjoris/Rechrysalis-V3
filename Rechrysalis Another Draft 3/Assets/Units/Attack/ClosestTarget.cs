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
            float _rangeOfUnit = _range.GetRange();
            bool _unitInRange = false;
            GameObject _closestUnitChecked = null;
            foreach (GameObject _targetToCheck in _enemyUnits.ActiveUnits)
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
            return null;
        }
    }
}
