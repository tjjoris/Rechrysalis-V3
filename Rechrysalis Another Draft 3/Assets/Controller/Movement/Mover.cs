using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private GameObject _backG;
        [SerializeField] private float _minX;

        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        [SerializeField] private Vector2 _direction;
        public Vector2 Direction {set{_direction = value;}get {return _direction;}}
        [SerializeField] bool _isStopped;
        public bool IsStopped {set{_isStopped = value;}get {return _isStopped;}}
        [SerializeField] float _speed;
        public void Initialize()
        {
            _backG = GameMaster.GetSingleton().ReferenceManager.BackG;
            Background _backGScript = _backG.GetComponent<Background>();
            _minX = _backGScript.MinX;
            _maxX = _backGScript.MaxX;
            _minY = _backGScript.MinY;
            _maxY = _backGScript.MaxY;
        }

        public void Tick(float _deltaTime)
        {
            if (!_isStopped){                
                float _x = _direction.x * _speed / _direction.magnitude;
                float _y = _direction.y * _speed / _direction.magnitude;
                if (((_x <0) && (transform.position.x + _x < _minX)) || ((_x > 0) && (transform.position.x + _x > _maxX)))
                {
                    _x = 0; 
                }
                if (((_y <0) && (transform.position.y + _y < _minY)) || ((_y > 0) && (transform.position.y + _y > _maxY)))
                {
                    _y = 0;
                }
                Vector3 _directionV3 = new Vector3(_x, _y, 0f);
                transform.Translate(_directionV3);
            }
        }
    }
}
