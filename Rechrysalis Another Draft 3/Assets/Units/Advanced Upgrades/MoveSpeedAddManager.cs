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
        private float _moveSpeedAdd;
        private float _timeToWait = 2f;
        [SerializeField] private float _timeCurrent = 0;
        [SerializeField] private bool _hasBeenDeactivated = false;
        public void Initialize(Mover mover, float moveSpeedAdd)
        {
            _mover = mover;
            _moveSpeedAdd = moveSpeedAdd;
        }
        public void OnEnable()
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
            if (_debugBool)
            {
                Debug.Log($"decrease Move Speed");
            }
            if (!_hasBeenDeactivated)
            {
                _mover.AddSpeed(-_moveSpeedAdd);
            }
        }
        private void RemoveSpeed()
        {
            _hasBeenDeactivated = true;
            _mover.AddSpeed(-_moveSpeedAdd);
        }
        public void Tick(float timeAmount)
        {
            if (!_hasBeenDeactivated)
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
