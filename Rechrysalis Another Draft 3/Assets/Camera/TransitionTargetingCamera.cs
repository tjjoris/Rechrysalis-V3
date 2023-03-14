using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.CameraControl
{
    
    public class TransitionTargetingCamera : MonoBehaviour
    {
        [SerializeField] private CameraFollowGOManager _cameraFollowGOManager;
        [SerializeField] private Transform _cameraFollowerParentTransform;
        [SerializeField] private Transform _controllerCameraTransform;
        [SerializeField] private bool _inTargetMode;
        [SerializeField] private ControllerManager _enemyControllerManager;
        [SerializeField] private MainManager _mainManager;
        public bool InTargetMode => _inTargetMode;
        private bool _debugBool = true;
        public void TransitionToTargeting()
        {
            if (_debugBool)
            {
                Debug.Log($"transition to targeting");
            }
            _mainManager.Paused = true;
            float xSum = 0;
            float ySum = 0;
            float count = 0;
            for (int i=0; i<_enemyControllerManager.ParentUnitManagers.Count; i++)
            {
                if (_enemyControllerManager.ParentUnitManagers[i].gameObject.activeInHierarchy)
                {
                    xSum += _enemyControllerManager.ParentUnitManagers[i].transform.position.x;
                    ySum += _enemyControllerManager.ParentUnitManagers[i].transform.position.y;
                    count ++;
                }
            }
            xSum /= count;
            ySum /= count;
            Vector2 cameraFollowPos = new Vector2(xSum, ySum);
            _cameraFollowerParentTransform.position = cameraFollowPos;
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
            _mainManager.Paused = false;
            transform.position = _controllerCameraTransform.position;
            transform.parent = _controllerCameraTransform;
            _inTargetMode = false;
        }
    }
}
