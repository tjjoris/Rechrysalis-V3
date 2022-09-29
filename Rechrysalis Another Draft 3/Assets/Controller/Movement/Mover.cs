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
        [SerializeField] float _speed;

        public void Tick(float _deltaTime)
        {
            if (!_isStopped){                
                float _x = _direction.x * _speed / _direction.magnitude;
                float _y = _direction.y * _speed / _direction.magnitude;
                Vector3 _directionV3 = new Vector3(_x, _y, 0f);
                transform.Translate(_directionV3);
            }
        }
    }
}
