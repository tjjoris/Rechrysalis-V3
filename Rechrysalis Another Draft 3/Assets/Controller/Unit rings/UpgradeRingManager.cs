using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class UpgradeRingManager : MonoBehaviour
    {
        [SerializeField] private float _currentAngle;
        public float CurrentAngle {get{return _currentAngle;}}

        public void Initialize (float _currentAngle)
        {
            this._currentAngle = _currentAngle;
        }
        public void SetCurrentAngle (float  _currentAngle)
        {
            this._currentAngle = _currentAngle;
        }
    }
}
