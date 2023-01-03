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
        private float _retreatSpeedMult = 0.2f;
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
        public void SetDirection(Vector2 direction)
        {
            // Debug.Log($"direction" + _direction);            
            this._direction = (Vector2.ClampMagnitude(direction, 1) * _speed);
            // Debug.Log($"rotation " + gameObject.transform.eulerAngles.z);            
                // float ySpeed = (Mathf.Cos(Mathf.Deg2Rad * (gameObject.transform.eulerAngles.z))) * _direction.y;
                // Debug.Log($"y speed " + ySpeed);
                // ySpeed *= _retreatSpeedMult;
                // Debug.Log($"direction y "+ _direction.y);
                // _direction.y -= Mathf.Abs(ySpeed);
                // Debug.Log($"direction y speed after " + _direction.y);
            // _direction = TurnSpeedIntoRetreatRelativeSpeed(_direction);
            _direction.y *= TurnV2IntoApproachSpeedMult(_direction);
            GetComponent<Rigidbody2D>().velocity = this._direction ;
            SetIsMovingIfMoving(this._direction);
            // if (_controllerIndex == 0)
            // Debug.Log($"velocity " + GetComponent<Rigidbody2D>().velocity);
        }
        // private Vector2 TurnSpeedIntoRetreatRelativeSpeed(Vector2 vectorBeforeRetreat)
        // {
        //     // Debug.Log($"rotation " + gameObject.transform.eulerAngles.z); 
        //     float ySpeed = (Mathf.Cos(Mathf.Deg2Rad * (gameObject.transform.eulerAngles.z))) * vectorBeforeRetreat.y;
        //     // Debug.Log($"angle speed " + Mathf.Cos(Mathf.Deg2Rad * (gameObject.transform.eulerAngles.z)));
        //     // Debug.Log($"y speed " + ySpeed);
        //     ySpeed *= _retreatSpeedMult;
        //     // Debug.Log($"direction y " + _direction.y);
        //     vectorBeforeRetreat.y -= Mathf.Abs(ySpeed);
        //     // Debug.Log($"direction y speed after " + _direction.y);
        //     return vectorBeforeRetreat;
        // }
        private float TurnV2IntoApproachSpeedMult(Vector2 vectorBeforeRetreat)
        {
            // Debug.Log($"rotation " + gameObject.transform.eulerAngles.z); 
            float ySpeedMult = (Mathf.Cos(Mathf.Deg2Rad * (gameObject.transform.eulerAngles.z))) * vectorBeforeRetreat.y;
            // Debug.Log($"angle speed " + Mathf.Cos(Mathf.Deg2Rad * (gameObject.transform.eulerAngles.z)));
            // Debug.Log($"y speed " + ySpeed);
            ySpeedMult *= Mathf.Abs(_retreatSpeedMult);
            ySpeedMult -= (_retreatSpeedMult * 0.5f);
            ySpeedMult += 1f;
            Debug.Log($"y speed mult" + ySpeedMult);
            return ySpeedMult;
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
            // _direction = TurnSpeedIntoRetreatRelativeSpeed(_direction);
            _direction.y *= TurnV2IntoApproachSpeedMult(_direction);
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
                if (_isStopped)
                {
                    _resetChargeUp?.Invoke();
                }
                 _isStopped = false;
            }
        }
    }
}
