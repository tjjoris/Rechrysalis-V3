using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;

namespace Rechrysalis.Movement
{
    public class FreeEnemyKiteMaxRange : MonoBehaviour
    {
        private TargetHolder _targetHolder;
        private Range _range;
        private Mover _mover;

        public void Initialize(TargetHolder _targetHolder)
        {
            _mover = GetComponent<Mover>();
            this._targetHolder = _targetHolder;
            _range = this._targetHolder.GetComponent<Range>();
        }
        public void Tick()
        {
            if (_targetHolder.Target != null)
            {
                Vector2 _direction = _targetHolder.Target.transform.position - gameObject.transform.position;
                if ((Mathf.Abs(_direction.magnitude)) < (_range.GetRange() - 0.5f))
                {
                    _mover.SetDirection(_direction);
                }
                else {
                    _mover.SetDirection(Vector2.zero);
                }
            }
        }
    }
}
