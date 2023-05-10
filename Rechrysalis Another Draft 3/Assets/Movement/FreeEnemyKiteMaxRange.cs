using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;
using Rechrysalis.Unit;

namespace Rechrysalis.Movement
{
    public class FreeEnemyKiteMaxRange : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private TargetHolder _targetHolder;
        private Range _range;
        private Mover _mover;
        private bool _retreating;
        // private Attack _attack;
        private FreeEnemyApproach _freeEnemyApproach;

        private void Awake()
        {

            _freeEnemyApproach = GetComponent<FreeEnemyApproach>();
            _mover = GetComponent<Mover>();
            _parentUnitManager = GetComponent<ParentUnitManager>();
        }
        // public void Initialize()
        // {
        //     // _attack = attack;
        //     // this._targetHolder = _targetHolder;
        //     // _range = this._targetHolder.GetComponent<Range>();
        // }
        public void Tick(bool aiCanMove)
        {
            _targetHolder = _parentUnitManager.CurrentSubUnit.GetComponent<TargetHolder>();
            if (_targetHolder == null)
            {
                return;
            }
            Attack attack = _targetHolder.GetComponent<Attack>();
            if ((attack != null) && (((_freeEnemyApproach != null) && (!_freeEnemyApproach.Approaching)) || (_freeEnemyApproach == null)))
            {
                if ((_targetHolder.Target != null))
                {
                    // Vector2 _direction = gameObject.transform.position - _targetHolder.Target.transform.position;
                    // if ((Mathf.Abs(_direction.magnitude)) < (_range.GetRange() - 0.5f))
                    if ((!attack.IsAttackReady()) && (_targetHolder.IsTargetInRangePlusValue(0.7f)))
                    // if (_targetHolder.IsTargetInRange())
                    {
                        // Debug.Log($"retreat");
                        _mover.SetDirection(Vector2.up);
                        _retreating = true;
                    }
                    else {
                        // _mover.SetDirection(Vector2.zero);
                        _mover.SetDirection(Vector2.zero);
                        _retreating = false;
                    }
                }
            }
        }
        public bool GetRetreating()
        {
            return _retreating;
        }
    }
}
