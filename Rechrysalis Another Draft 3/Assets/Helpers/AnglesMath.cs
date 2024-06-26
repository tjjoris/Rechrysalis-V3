using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class AnglesMath
    {
        public static float LimitAngle(float angle)
        {
            while (angle < 0)
            {
                angle += 360;
            }
            while (angle > 360)
            {
                angle -= 360;
            }
            return angle;
        }
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        public static float V2ToDegrees(Vector2 _v2)
        {
            return LimitAngle(Mathf.Atan2(_v2.y, _v2.x) * Mathf.Rad2Deg);
        }
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        public static float GetIncreaseOrDecreaseAngle(float _firstAngle, float _secondAngle, float _minToIgnore)
        {
            _firstAngle = AnglesMath.LimitAngle(_firstAngle);
            _secondAngle = AnglesMath.LimitAngle(_secondAngle);
            // if ((_firstAngle > _secondAngle - 180) && (_firstAngle < _secondAngle - _minToIgnore))
            // {
            //     return 1;
            // }
            // else if ((_firstAngle < _secondAngle + 180) && (_firstAngle > _secondAngle - _minToIgnore)
            // {
            //     return  -1;
            // }
            // else 
            // return 0;
            float _newAngle = AnglesMath.LimitAngle(_secondAngle - _firstAngle);
            if ((_newAngle < 180) && (_newAngle > _minToIgnore))
            {
                return -1;
            }
            else if ((_newAngle > 180) && (_newAngle < 360 - _minToIgnore))
            {
                return 1;
            }
            else {
                return  0;
            }
        }

        public static float UnitAngle(int _unitIndex, int _maxUnits)
        {
            return AnglesMath.LimitAngle((360 / _maxUnits) * _unitIndex);
        }
        public static Vector2 PosForUnitInRing(int _numberInRing, int _indexInRing, float _ringAngle, float _distFromCentre)
        {
            float _radToOffset = Mathf.Deg2Rad * (((360f / _numberInRing) * _indexInRing) + _ringAngle);
            Vector2 _unitOffset = new Vector2(Mathf.Cos(_radToOffset) * _distFromCentre, Mathf.Sin(_radToOffset) * _distFromCentre);
            return _unitOffset;
        }
        public static Vector2 GetOffsetPosForParentInRing(float parentUnitIndex, float parentUnitCount, float unitRingAngle, float ringDistFromCentre)
        {
            float _radToOffset = Mathf.Deg2Rad * (((360f / parentUnitCount) * parentUnitIndex) + unitRingAngle);
            Vector2 unitOffset = new Vector3(Mathf.Cos(_radToOffset) * ringDistFromCentre, Mathf.Sin(_radToOffset) * ringDistFromCentre);
            return unitOffset;
        }
    }
}
