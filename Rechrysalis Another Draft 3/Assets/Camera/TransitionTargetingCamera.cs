using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.CameraControl
{
    
    public class TransitionTargetingCamera : MonoBehaviour
    {
        [SerializeField] private CameraFollowGOManager _cameraFollowGOManager;
        [SerializeField] private Transform _controllerCameraTransform;
        [SerializeField] private bool _inTargetMode;
        public bool InTargetMode => _inTargetMode;
        private bool _debugBool = true;
        public void TransitionToTargeting()
        {
            if (_debugBool)
            {
                Debug.Log($"transition to targeting");
            }
            transform.position = _cameraFollowGOManager.transform.position;
            transform.parent = _cameraFollowGOManager.transform;
            _inTargetMode = true;
        }
        public void TransitionToController()
        {
            if (_debugBool)
            {
                Debug.Log($"transtion to controller");                
            }
            transform.position = _controllerCameraTransform.position;
            transform.parent = _controllerCameraTransform;
            _inTargetMode = false;
        }
    }
}
