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
            StartCoroutine(RemoveSpeed());
        }
        public void OnDisable()
        {
            if (_debugBool)
            {
                Debug.Log($"decrease Move Speed");
            }
            _mover.AddSpeed(-_moveSpeedAdd);
        }
        IEnumerator RemoveSpeed()
        {
            yield return new WaitForSeconds (_timeToWait);
            Destroy(this);
        }
    }
}
