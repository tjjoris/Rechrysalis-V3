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
        private float _minAngleToMove = 1f;
        private float _amountToRotate = 1f;
        [SerializeField] private float _unitDegreeWidth;
        public float UnitDegreeWidth {get{return _unitDegreeWidth;}}
        private float[] _unitDegreeWidthArray;
        // public float[] UnitDegreeWidthArray {get {return _unitDegreeWidthArray;}}
        private GameObject[] _parentUnits;        
    
        public void Initialize (int _numberOfParentUnits, GameObject[] _parentunits, float _unitRingAngle)
        {
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
        public void Tick (float _timeAmount)
        {
            if (_targetAngle > _unitRingAngle + _minAngleToMove)
            {
                RotateRing(_timeAmount * _amountToRotate);
            }
            else if (_targetAngle < _unitRingAngle - _minAngleToMove)
            {
                RotateRing(-_timeAmount * _amountToRotate);
            }
        }
        private void RotateRing (float _amountToRotate)
        {
            Vector3 _eluerAmngle = new Vector3 (0, 0, transform.eulerAngles.z + _amountToRotate);
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
