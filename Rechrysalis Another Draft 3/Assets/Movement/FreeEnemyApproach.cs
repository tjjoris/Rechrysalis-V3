using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;

namespace Rechrysalis.Unit
{
    public class FreeEnemyApproach : MonoBehaviour
    {
        // private float _range;
        private PlayerUnitsSO _ownUnits;
        private ClosestTarget _closestTarget;
        private GameObject _targetUnit;
        private Range _range;

        public void Initialize(PlayerUnitsSO _ownUnits, Range _range)
        {
            // this._range = _range;
            this._ownUnits = _ownUnits;
            this._range = _range;
            this._closestTarget = this._range.GetComponent<ClosestTarget>();
        }

        public void Tick()
        {
            Vector3 _approachDirection = Vector3.zero;
            _targetUnit = _closestTarget.GetNearestEnemyInRange();
            Vector2 _distV2 = (_targetUnit.transform.position - transform.position);
            if (Mathf.Abs(_distV2.magnitude) > _range.GetRange())
            {
                _approachDirection = Vector3.MoveTowards(transform.position, _targetUnit.transform.position, 1);
            }
            // Vector2 _approachV2 = _approachDirection;
            GetComponent<Mover>()?.SetDirection(_approachDirection);
        }
    }
}
