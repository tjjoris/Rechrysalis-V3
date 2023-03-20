using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Background;
using System;
using Rechrysalis.Unit;
using Rechrysalis.Attacking;

namespace Rechrysalis.Movement
{
    public class Mover : MonoBehaviour
    {
        private MainManager _mainManager;
        private PauseScript _pauseScript;
        private bool _debugBool = false;
        [SerializeField] int _controllerIndex;
        [SerializeField] private GameObject _backG;
        [SerializeField] private float _minX;

        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        // private float _pushBackMovement;
        // public float PushBackMovement {set{_pushBackMovement = value;}}
        private float _approachSpeedMult = 0.15f;
        [SerializeField] private Vector3 _moveVector;        
        [SerializeField] private Vector2 _direction = Vector2.zero;
        public Vector2 Direction {set{_direction = value;}get {return _direction;}}
        [SerializeField] private Vector2 _directionBase;
        [SerializeField] private Vector2 _directionWhilePaused = Vector2.zero;
        [SerializeField] bool _isStopped = true;
        public bool IsStopped {set{_isStopped = value;}get {return _isStopped;}}
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _speedVaried;
        [SerializeField] private int _siegeInt;
        private CausesPushBack _causesPushBack;
        private ParentUnitManager _parentUnitManager;
        private Rigidbody2D _rb2d;
        public Rigidbody2D RB2D => _rb2d;
        public Action _resetChargeUp;
        public void Initialize(int _controllerIndex, MainManager mainManager)
        {
            _mainManager = mainManager;
            _pauseScript = _mainManager.GetComponent<PauseScript>();
            _parentUnitManager = GetComponent<ParentUnitManager>();
            _causesPushBack = GetComponent<CausesPushBack>();
            this._controllerIndex = _controllerIndex;
            _backG = GameMaster.GetSingleton().ReferenceManager.BackG;
            BackgroundManager _backGScript = _backG.GetComponent<BackgroundManager>();
            _minX = _backGScript.MinX;
            _maxX = _backGScript.MaxX;
            _minY = _backGScript.MinY;
            _maxY = _backGScript.MaxY;
            _rb2d = GetComponent<Rigidbody2D>();
        }
        public void SetBaseSpeed(float baseSpeed)
        {
            this._baseSpeed = baseSpeed;
            _speedVaried = _baseSpeed;
        }
        public void AddSpeed(float speedToAdd)
        {
            _speedVaried += speedToAdd;
            SetDirection(_direction);
        }
        public void AddSiegeInt(int intToAdd)
        {
            _siegeInt += intToAdd;
            SetDirection(_direction);
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
        public void SetDirection(Vector2 directionInput)
        {
            Vector2 directionToSet = Vector2.zero;
            directionInput.Normalize();          
            if ((_siegeInt <= 0) && (!_pauseScript.IsPaused()))
            {
                float ySpeedMult = TurnV2IntoApproachSpeedMult(directionInput);
                _direction = directionInput * _speedVaried;
                _direction.y = _direction.y * ySpeedMult;
                directionToSet = _direction;
            }
            else {
                directionToSet = Vector2.zero;
            }
            _rb2d.velocity = directionToSet ;
            if (_debugBool)
            {                
                Debug.Log($"speed " +directionToSet.magnitude + " controller " + _controllerIndex);
            }
            SetIsMovingIfMoving(this._direction);
        }
        public void PauseUnPause(bool pauseBool)
        {
            if (_debugBool)
            {
                Debug.Log($"pauseUnpause" + pauseBool);
            }
            if (pauseBool)
            {
                _direction = _rb2d.velocity;
                _rb2d.velocity = Vector2.zero;
            }
            else 
            {
                if (!_pauseScript.IsPaused())
                {
                    _rb2d.velocity = _direction;
                }
            }
        }
        private float TurnV2IntoApproachSpeedMult(Vector2 vectorBeforeRetreat)
        {
            float ySpeedMult = Mathf.Cos(Mathf.Deg2Rad * (gameObject.transform.eulerAngles.z));
            ySpeedMult = 1 / ySpeedMult * Mathf.Abs(ySpeedMult);
            ySpeedMult *= vectorBeforeRetreat.y;
            ySpeedMult *= _approachSpeedMult;
            ySpeedMult = 1 + ySpeedMult;
            return ySpeedMult;
        }
        private Vector2 LimitXMovement(Vector2 direction)
        {            
            if ((direction.x < 0) && (transform.position.x < _minX))
            {
                direction.x = 0;
            }
            else if ((direction.x > 0) && (transform.position.x > _maxX))
            {
                direction.x = 0;
            }
            return direction;
        }
        // public void Tick()
        // {
        //     // if (_isStopped)
        //     // {
        //     //     _direction = Vector2.zero;
        //     // }
        //     GetComponent<Rigidbody2D>().velocity = this._direction;
        // }
        // public void Tick(float _deltaTime)
        // {
        //     float _x = 0f;
        //     float _y = 0f;
        //     if (!_isStopped){                
        //         _x = _direction.x * _speed / _direction.magnitude;
        //         _y = _direction.y * _speed / _direction.magnitude;
        //         if (((_x <0) && (transform.position.x + _x < _minX)) || ((_x > 0) && (transform.position.x + _x > _maxX)))
        //         {
        //             _x = 0; 
        //         }
        //         // if (((_y <0) && (transform.position.y + _y < _minY)) || ((_y > 0) && (transform.position.y + _y > _maxY)))
        //         // {
        //         //     _y = 0;
        //         // }
        //     }
        //     // if ((_causesPushBack != null) && (_y > 0)) 
        //     // {
        //     //     _causesPushBack.PushBack(_y);
        //     // }                
        //     _moveVector = new Vector3(_x, _y, 0f);            
        //     // if ((GetComponent<PushBackFromPlayer>() != null) && (_pushBackMovement > 0) && (_moveVector.y < _pushBackMovement))
        //     // {
        //     //     _moveVector.y = _pushBackMovement;
        //     // }
        //     if (_controllerIndex == 1) {
        //     }
        //     // Debug.Log($"vector " + _moveVector);
        //     if ((float.IsNaN(_moveVector.x) || (float.IsNaN(_moveVector.y) || (float.IsNaN(_moveVector.z)))))
        //     {
        //         _moveVector = Vector3.zero;
        //     }
        //     // Debug.Log($"direction "+ _direction+" move " + _moveVector);
        //     // transform.Translate(_moveVector);
        //     Vector2 _newPostion = transform.position;
        //     // _direction = TurnSpeedIntoRetreatRelativeSpeed(_direction);
        //     _direction.y *= TurnV2IntoApproachSpeedMult(_direction);
        //     _newPostion += (_direction * _deltaTime);
        //     // SetIsMovingIfMoving(_direction);
        //     // GetComponent<Rigidbody2D>()?.MovePosition(_newPostion);
        // }
        private void SetIsMovingIfMoving(Vector2 _direction)
        {
            if (_direction == Vector2.zero)
            {
                if (_controllerIndex == 1)     
                _isStopped = true;
            }
            else
            {
                // if (_isStopped)
                // {
                //     // _resetChargeUp?.Invoke();
                //     // _parentUnitManager?.CurrentSubUnit.GetComponent<Attack>().CheckToResetChargeUp();
                // }
                 _isStopped = false;
            }
            if (_parentUnitManager != null) _parentUnitManager.IsStopped = _isStopped;            
        }
    }
}
