using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class UnitRingManager : MonoBehaviour
    {
        private float _unitRingAngle;
        public float UnitRingAngle {get{return _unitRingAngle;}}
        private float _targetAngle;
        private Transform _targetQuaternionRotationTransform;
        private float _minAngleToMove = 1f;
        private float _amountToRotate = 50f;
        [SerializeField] private float _unitDegreeWidth;
        public float UnitDegreeWidth {get{return _unitDegreeWidth;}}
        private float[] _unitDegreeWidthArray;
        // public float[] UnitDegreeWidthArray {get {return _unitDegreeWidthArray;}}
        private GameObject[] _parentUnits;        
    
        public void Initialize (int _numberOfParentUnits, GameObject[] _parentunits, float _unitRingAngle, Transform _targetTransform)
        {
            this._targetQuaternionRotationTransform = _targetTransform;
            this._unitRingAngle = _unitRingAngle;
            this._parentUnits = _parentUnits;
            if (_numberOfParentUnits > 0) {
            _unitDegreeWidthArray = new float[_numberOfParentUnits * 2];
            for (int _parentUnitIndex = 0; _parentUnitIndex < _numberOfParentUnits; _parentUnitIndex++)
            {
                _unitDegreeWidthArray[_parentUnitIndex * 2] = ((360 / _numberOfParentUnits) * _parentUnitIndex) - _unitDegreeWidth;
                _unitDegreeWidthArray[(_parentUnitIndex * 2) + 1] = ((360 / _numberOfParentUnits) * _parentUnitIndex) + _unitDegreeWidth;
            }
            }
        }
        public void SetTargetAngle(float _targetAngle)
        {
            this._targetAngle = _targetAngle;
        }
        public void SetTargetTransform (Transform _transform)
        {
            this._targetQuaternionRotationTransform = _transform;
        }
        public void Tick (float _timeAmount)
        {
            // Debug.Log($"tick");
            // if (_targetAngle > _unitRingAngle + _minAngleToMove)
            // {
            //     RotateRing(_timeAmount * _amountToRotate);
            // }
            // else if (_targetAngle < _unitRingAngle - _minAngleToMove)
            // {
            //     RotateRing(-_timeAmount * _amountToRotate);
            // }
            // RotateRing(AnglesMath.GetIncreaseOrDecreaseAngle(_targetAngle, _unitRingAngle, _minAngleToMove) * _timeAmount + _amountToRotate);
            
            Vector3 _targetV3 = new Vector3 (0, 0, _targetAngle * Mathf.Deg2Rad);
            // Vector3 _rotateTowards = Vector3.RotateTowards(transform.rotation, _targetV3, _amountToRotate, 0.0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetQuaternionRotationTransform.rotation, _amountToRotate * _timeAmount);
            _unitRingAngle = AnglesMath.LimitAngle((transform.rotation.eulerAngles.z) );
        }
        private void RotateRing (float _amountToRotate)
        {
            Debug.Log($"rotate ring " + _amountToRotate);
            _unitRingAngle = AnglesMath.LimitAngle(_unitRingAngle + _amountToRotate);
            Vector3 _eluerAmngle = new Vector3 (0, 0, _unitRingAngle - 90);
            transform.eulerAngles = _eluerAmngle;
        }
        public float[] GetUnitDegreeWidthArray()
        {
            float[] _unitDegreeWidthArrayWithRingAngle = new float[_unitDegreeWidthArray.Length];
            for (int _unitWidth = 0; _unitWidth < _unitDegreeWidthArray.Length; _unitWidth++)
            {
                _unitDegreeWidthArrayWithRingAngle[_unitWidth] = AnglesMath.LimitAngle(_unitDegreeWidthArray[_unitWidth] + _unitRingAngle);
            }
            return _unitDegreeWidthArray;
        }
    }    
}
