using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.CameraControl
{
    
    public class TransitionTargetingCamera : MonoBehaviour
    {
        private bool _debugBool = false;
        [SerializeField] private CameraFollowGOManager _cameraFollowGOManager;
        [SerializeField] private Transform _cameraFollowerParentTransform;
        [SerializeField] private Transform _controllerCameraTransform;
        [SerializeField] private bool _inTargetMode;
        [SerializeField] private ControllerManager _enemyControllerManager;
        [SerializeField] private MainManager _mainManager;
        [SerializeField] private PauseScript _pauseScript;
        [SerializeField] private GameObject _returnToControllerButton;
        public bool InTargetMode => _inTargetMode;
        public void TransitionToTargeting()
        {
            if (_debugBool)
            {
                if (_debugBool) Debug.Log($"transition to targeting");
            }
            _pauseScript.SetTargetingPause(true);
            float xSum, ySum, count;
            FindPositionSumsOfUnitLocationsAndCount(out xSum, out ySum, out count);
            xSum /= count;
            ySum /= count;
            Vector2 cameraFollowPos = new Vector2(xSum, ySum);
            _cameraFollowerParentTransform.position = cameraFollowPos;
            transform.position = _cameraFollowGOManager.transform.position;
            transform.parent = _cameraFollowGOManager.transform;
            _inTargetMode = true;
            _returnToControllerButton.SetActive(true);
        }

        private void FindPositionSumsOfUnitLocationsAndCount(out float xSum, out float ySum, out float count)
        {
            xSum = 0;
            ySum = 0;
            count = 0;
            LoopEnemyParentUnitManagers(ref xSum, ref ySum, ref count);
        }

        private void LoopEnemyParentUnitManagers(ref float xSum, ref float ySum, ref float count)
        {
            for (int i = 0; i < _enemyControllerManager.ParentUnitManagers.Count; i++)
            {
                AddPositionSumAndCountForThisPUM(ref xSum, ref ySum, ref count, i);
            }
        }

        private void AddPositionSumAndCountForThisPUM(ref float xSum, ref float ySum, ref float count, int i)
        {
            if (!_enemyControllerManager.ParentUnitManagers[i].gameObject.activeInHierarchy)
                { return; }
                xSum += _enemyControllerManager.ParentUnitManagers[i].transform.position.x;
            ySum += _enemyControllerManager.ParentUnitManagers[i].transform.position.y;
            count++;
        }

        public void TransitionToController()
        {
            if (_debugBool)
            {
                if(_debugBool) Debug.Log($"transtion to controller");                
            }
            _pauseScript.SetTargetingPause(false);
            transform.position = _controllerCameraTransform.position;
            transform.parent = _controllerCameraTransform;
            _inTargetMode = false;
            _returnToControllerButton.SetActive(false);
        }
    }
}
