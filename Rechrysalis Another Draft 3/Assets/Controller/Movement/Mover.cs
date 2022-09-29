using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Vector2 _direction;
        public Vector2 Direction {set{_direction = value;}get {return _direction;}}
        [SerializeField] bool _isStopped;
        public bool IsStopped {set{_isStopped = value;}get {return _isStopped;}}

        public void Tick()
        {
            if (!_isStopped){
                Vector3 _directionV3 = _direction;
                transform.Translate(_directionV3);
            }
        }
    }
}
