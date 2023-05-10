using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Movement
{
    public class FreeUnitChrysalisMovementStop : MonoBehaviour
    {
        private Mover _mover;

        public void Awake()
        {
            _mover = GetComponent<Mover>();
        }
        public void StopMovement()
        {
            _mover.SetDirection(Vector2.zero);
        }
    }
}
