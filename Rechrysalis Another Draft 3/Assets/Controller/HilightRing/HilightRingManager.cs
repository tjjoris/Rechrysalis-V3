using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class HilightRingManager : MonoBehaviour
    {
        private float _oldAngle;
        private float _unitRingOldAngle;
        private UnitRingManager _unitRingManager;
        [SerializeField] private HilightRingParentCreator _hilightRingParentCreator;
        
        private void Awake()
        {

            _hilightRingParentCreator = GetComponent<HilightRingParentCreator>();
        }
        public void Initialize(UnitRingManager _unitRingManager)
        {
            this._unitRingManager = _unitRingManager;   
            _hilightRingParentCreator?.Initialize(transform);        
        }
        public void SetAngleToUnitRing()
        {
            float _newAngle = AnglesMath.LimitAngle((_unitRingManager.UnitRingAngle));
            transform.eulerAngles = new Vector3 (0, 0, _newAngle);
        }
        public void SetAngle(float _angle)
        {
            float _newAngle = AnglesMath.LimitAngle((_angle - 90 - _oldAngle) );
            // Debug.Log($"old angle " + _oldAngle + "mouse _angle " + (_angle-90) );
            transform.eulerAngles = new Vector3 (0, 0, _newAngle);
            // _unitRingManager.SetTargetAngle(_newAngle);
            // _unitRingManager.SetTargetTransform(transform);
        }
        public void SetOldAngle(float _mouseAngle)
        {
            _unitRingOldAngle = _unitRingManager.UnitRingAngle;
            this._oldAngle = AnglesMath.LimitAngle(_mouseAngle - 90 - _unitRingOldAngle);
            // transform.eulerAngles = new Vector3 (0, 0, _unitRingOldAngle);
            
        }
        public void ResetToOldAngle()
        {
            transform.eulerAngles = new Vector3(0, 0, _unitRingOldAngle);
            // _unitRingManager.SetTargetAngle(_oldAngle);
        }
        // public void CreateHilightRingParent(int index, int maxParents, Vector2 parentUnitOffset)
        // {
        //     _hilightRingParentCreator?.CreateHilightRingParent(index, maxParents, parentUnitOffset);
        // }
    }
}
