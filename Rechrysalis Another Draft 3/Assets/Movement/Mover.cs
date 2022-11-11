using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Background;
using System;

namespace Rechrysalis.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] int _controllerIndex;
        [SerializeField] private GameObject _backG;
        [SerializeField] private float _minX;

        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        // private float _pushBackMovement;
        // public float PushBackMovement {set{_pushBackMovement = value;}}
        [SerializeField] private Vector3 _moveVector;        
        [SerializeField] private Vector2 _direction = Vector2.zero;
        public Vector2 Direction {set{_direction = value;}get {return _direction;}}
        [SerializeField] bool _isStopped = true;
        public bool IsStopped {set{_isStopped = value;}get {return _isStopped;}}
        [SerializeField] float _speed;
        private CausesPushBack _causesPushBack;
        public Action _resetChargeUp;
        public void Initialize(int _controllerIndex)
        {
            _causesPushBack = GetComponent<CausesPushBack>();
            this._controllerIndex = _controllerIndex;
            _backG = GameMaster.GetSingleton().ReferenceManager.BackG;
            BackgroundManager _backGScript = _backG.GetComponent<BackgroundManager>();
            _minX = _backGScript.MinX;
            _maxX = _backGScript.MaxX;
            _minY = _backGScript.MinY;
            _maxY = _backGScript.MaxY;
        }
        public void SetSpeed(float _speed)
        {
            this._speed = _speed;
        }
        public void ResetMovement()
        {   
            // _pushBackMovement = 0;
            _moveVector = Vector2.zero;            
        }
        // public void PushBackMovement(float _y)
        // {
        //     _pushBackMovement = _y;
        // }
        public void SetDirection(Vector2 _direction)
        {
            // Debug.Log($"direction" + _direction);            
            this._direction = (Vector2.ClampMagnitude(_direction, 1) * _speed);            
            GetComponent<Rigidbody2D>().velocity = this._direction ;
            SetIsMovingIfMoving(this._direction);
            if (_controllerIndex == 0)
            Debug.Log($"velocity " + GetComponent<Rigidbody2D>().velocity);
        }
        public void Tick(float _deltaTime)
        {
            float _x = 0f;
            float _y = 0f;
            if (!_isStopped){                
                _x = _direction.x * _speed / _direction.magnitude;
                _y = _direction.y * _speed / _direction.magnitude;
                if (((_x <0) && (transform.position.x + _x < _minX)) || ((_x > 0) && (transform.position.x + _x > _maxX)))
                {
                    _x = 0; 
                }
                // if (((_y <0) && (transform.position.y + _y < _minY)) || ((_y > 0) && (transform.position.y + _y > _maxY)))
                // {
                //     _y = 0;
                // }
            }
            // if ((_causesPushBack != null) && (_y > 0)) 
            // {
            //     _causesPushBack.PushBack(_y);
            // }                
            _moveVector = new Vector3(_x, _y, 0f);            
            // if ((GetComponent<PushBackFromPlayer>() != null) && (_pushBackMovement > 0) && (_moveVector.y < _pushBackMovement))
            // {
            //     _moveVector.y = _pushBackMovement;
            // }
            if (_controllerIndex == 1) {
            }
            // Debug.Log($"vector " + _moveVector);
            if ((float.IsNaN(_moveVector.x) || (float.IsNaN(_moveVector.y) || (float.IsNaN(_moveVector.z)))))
            {
                _moveVector = Vector3.zero;
            }
            // Debug.Log($"direction "+ _direction+" move " + _moveVector);
            // transform.Translate(_moveVector);
            Vector2 _newPostion = transform.position;
            _newPostion += (_direction * _deltaTime);
            // SetIsMovingIfMoving(_direction);
            // GetComponent<Rigidbody2D>()?.MovePosition(_newPostion);
        }
        private void SetIsMovingIfMoving(Vector2 _direction)
        {
            if (_direction == Vector2.zero)
            {
                if (_controllerIndex == 1)     
                _isStopped = true;
            }
            else
            {
                if (_controllerIndex == 1)
                Debug.Log($"direction " + _direction);
                if (_isStopped)
                {
                    _resetChargeUp?.Invoke();
                }
                 _isStopped = false;
            }
        }
    }
}
