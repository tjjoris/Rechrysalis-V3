using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Movement;

namespace Rechrysalis.AdvancedUpgrade
{
    public class MoveSpeedAddManager : MonoBehaviour
    {
        private bool _debugBool = true;
        private Mover _mover;
        private Rigidbody2D _rb2d;
        private float _moveSpeedAdd;
        private float _timeToWait = 4f;
        [SerializeField] private float _timeCurrent = 0;
        [SerializeField] private bool _hasBeenDeactivated = false;
        public void Initialize(Mover mover, float moveSpeedAdd)
        {
            _mover = mover;
            _moveSpeedAdd = moveSpeedAdd;
            _rb2d = _mover.RB2D;
        }
        // public void OnEnable()
        // {
        // }
        public void Activate()
        {
            if (_debugBool)
            {
                Debug.Log($"increase move speed");
            }
            _mover.AddSpeed(_moveSpeedAdd);
            _timeCurrent = 0;
            _hasBeenDeactivated = false;
        }
        public void OnDisable()
        {
            if (!_hasBeenDeactivated)
            {
                _mover.AddSpeed(-_moveSpeedAdd);
            }
        }
        private void RemoveSpeed()
        {
            if (_debugBool)
            {
                Debug.Log($"decrease Move Speed");
            }
            _hasBeenDeactivated = true;
            _mover.AddSpeed(-_moveSpeedAdd);
        }
        public void Tick(float timeAmount)
        {
            if ((!_hasBeenDeactivated) && (_rb2d.velocity != Vector2.zero))
            {
                _timeCurrent += timeAmount;
                if (_timeCurrent >= _timeToWait)
                {
                    RemoveSpeed();
                }
            }
        }
    }
}
