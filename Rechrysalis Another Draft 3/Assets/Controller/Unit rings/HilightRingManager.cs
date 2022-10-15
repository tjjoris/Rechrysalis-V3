using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class HilightRingManager : MonoBehaviour
    {
        private float _oldAngle;
        private UnitRingManager _unitRingManager;

        public void Initialize(UnitRingManager _unitRingManager)
        {
            this._unitRingManager = _unitRingManager;
        }
        public void SetAngle(float _angle)
        {
            Debug.Log($"angle " + _angle);
            float _newAngle = _angle - _oldAngle;
            transform.eulerAngles = new Vector3 (0, 0, _newAngle);
            _unitRingManager.SetTargetAngle(_newAngle);
        }
        public void SetOldAngle(float _oldAngle)
        {
            this._oldAngle = _oldAngle;
        }
        // public void Tick()
        // {

        // }
    }
}
