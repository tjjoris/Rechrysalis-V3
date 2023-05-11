using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
// using Rechrysalis.Movement;
using Rechrysalis.Unit;

namespace Rechrysalis.Movement
{
    public class FreeEnemyApproach : MonoBehaviour
    {
        // private float _range;
        private ParentUnitManager _parentUnitManager;
        private PlayerUnitsSO _ownUnits;
        private ClosestTarget _closestTarget;
        // private GameObject _targetUnit;
        private TargetHolder _targetHolder;
        private Range _range;  
        private Mover _enemyControllerMover;  //this may be a circular dependancy 
        private bool _approaching;
        public bool Approaching => _approaching;   
        private Mover _mover;
        private TargetPrioratizeByScore _targetPrioratizeByScore;

        private void Awake()
        {

            _parentUnitManager = GetComponent<ParentUnitManager>();
            _mover = GetComponent<Mover>();
        }
        public void Initialize(PlayerUnitsSO _ownUnits, Mover enemyControllerMover)
        {
            // this._range = _range;
            _enemyControllerMover = enemyControllerMover;
            this._ownUnits = _ownUnits;
            // this._range = _range;
            // this._closestTarget = this._range.GetComponent<ClosestTarget>();
            // _targetHolder = this._range.GetComponent<TargetHolder>();
        }

        public void Tick(bool _isRetreating, bool _aiCanMove)
        {
            _range = _parentUnitManager.CurrentSubUnit.GetComponent<Range>();
            if (_range == null)
            {
                return;
            }
            _targetHolder = _range.GetComponent<TargetHolder>();
            _targetPrioratizeByScore = _range.GetComponent<TargetPrioratizeByScore>();
            GameObject focusTarget = null;
            if (_targetPrioratizeByScore != null)
            {                
                focusTarget = _targetPrioratizeByScore.GetFocusTarget();
                if ((focusTarget != null))
                {
                    _targetHolder.Target = focusTarget;
                }
            }
            if (focusTarget == null)
            {
                focusTarget = _targetHolder.Target;
            }
            if (!_isRetreating)
            {
                Vector3 _approachDirection = Vector3.zero;
                // _targetUnit = _closestTarget.GetNearestEnemy();
                // if (!_targetHolder.IsTargetInRange())
                // {
                //     _closestTarget.GetNearestEnemy();
                // }
                // if (!_approaching)
                if ((_targetHolder.Target != null) && (!_isRetreating))
                {
                    Vector2 _distV2 = (_targetHolder.Target.transform.position - transform.position);
                    // if (Mathf.Abs(_distV2.magnitude) > _range.GetRange())
                    if ((_enemyControllerMover.Direction.y < -0.9f) && (_approaching) && (!_targetHolder.IsTargetInRange()))
                    {
                        // _approachDirection = Vector3.MoveTowards(transform.position, _targetHolder.Target.transform.position, 1);
                        _approachDirection = ApproachDirection(_distV2);
                        _approaching = true;
                    }
                    else if ((!_targetHolder.IsTargetInRange()))
                    {
                        _approachDirection = ApproachDirection(_distV2);
                        _approaching = true;
                    }
                    else
                    {
                        _approaching = false;
                    }
                    // Vector2 _approachV2 = _approachDirection;
                    
                    
                }
                _mover?.SetDirection(_approachDirection);
            }
        }

        private Vector3 ApproachDirection(Vector2 distV2)
        {
            Vector3 _approachDirection = (-transform.position + _targetHolder.Target.transform.position);
            // Vector3 _approachDirection = distV2;
            _approachDirection = Vector3.ClampMagnitude(_approachDirection, 1);
            return _approachDirection;
        }

        public bool GetIsApproaching()
        {
            return _approaching;
        }
    }
}
