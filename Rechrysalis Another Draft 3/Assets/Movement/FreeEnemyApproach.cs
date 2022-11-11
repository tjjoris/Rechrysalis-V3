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
        // private GameObject _targetUnit;
        private TargetHolder _targetHolder;
        private Range _range;     
        private bool _approaching;   

        public void Initialize(PlayerUnitsSO _ownUnits, Range _range)
        {
            // this._range = _range;
            this._ownUnits = _ownUnits;
            this._range = _range;
            this._closestTarget = this._range.GetComponent<ClosestTarget>();
            _targetHolder = this._range.GetComponent<TargetHolder>();
        }

        public void Tick(bool _isRetreating)
        {
            Vector3 _approachDirection = Vector3.zero;
            // _targetUnit = _closestTarget.GetNearestEnemy();
            // if (!_targetHolder.IsTargetInRange())
            // {
            //     _closestTarget.GetNearestEnemy();
            // }
            if ((_targetHolder.Target != null) && (!_isRetreating))
            {
                Vector2 _distV2 = (_targetHolder.Target.transform.position - transform.position);
                // if (Mathf.Abs(_distV2.magnitude) > _range.GetRange())
                if (!_targetHolder.IsTargetInRange())
                {
                    // _approachDirection = Vector3.MoveTowards(transform.position, _targetHolder.Target.transform.position, 1);
                    _approachDirection = (-transform.position + _targetHolder.Target.transform.position);
                    _approachDirection = Vector3.ClampMagnitude(_approachDirection, 1);
                }
                // Vector2 _approachV2 = _approachDirection;
                // Debug.Log($"approach " + _approachDirection);
                Debug.Log($"approach " + _approachDirection);
                GetComponent<Mover>()?.SetDirection(_approachDirection);
                _approaching = true;
            }
        }
        public bool GetIsApproaching()
        {
            return _approaching;
        }
    }
}
