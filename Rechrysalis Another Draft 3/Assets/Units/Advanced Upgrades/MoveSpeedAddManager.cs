using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Movement;

namespace Rechrysalis.AdvancedUpgrade
{
    public class MoveSpeedAddManager : MonoBehaviour
    {
        private Mover _mover;
        private float _moveSpeedAdd;
        public void Initialize(Mover mover, float moveSpeedAdd)
        {
            _mover = mover;
            _moveSpeedAdd = moveSpeedAdd;
        }
        public void OnEnable()
        {
            Debug.Log($"increase move speed");
        }
    }
}
