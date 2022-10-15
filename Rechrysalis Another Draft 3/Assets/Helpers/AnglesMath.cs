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
    }
}
