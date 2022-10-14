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
            transform.eulerAngles = new Vector3 (0, 0, _angle);
            _unitRingManager.SetTargetAngle(_angle);
        }
        // public void Tick()
        // {

        // }
    }
}
