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
        private bool _retreating;

        public void Initialize(TargetHolder _targetHolder)
        {
            _mover = GetComponent<Mover>();
            this._targetHolder = _targetHolder;
            _range = this._targetHolder.GetComponent<Range>();
        }
        public void Tick(bool _aiCanMove)
        {
            if ((_aiCanMove) && (_targetHolder.Target != null))
            {
                Vector2 _direction = gameObject.transform.position - _targetHolder.Target.transform.position;
                if ((Mathf.Abs(_direction.magnitude)) < (_range.GetRange() - 0.5f))
                {
                    // Debug.Log($"retreat");
                    _mover.SetDirection(Vector2.up);
                    _retreating = true;
                }
                else {
                    // _mover.SetDirection(Vector2.zero);
                    _retreating = false;
                }
            }
        }
        public bool GetRetreating()
        {
            return _retreating;
        }
    }
}
