using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class PushBackFromPlayer : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        [SerializeField] private PlayerUnitsSO[] _playerUnitsSO;
        [SerializeField] private ControllerManager _enemyController;

        public void Initialize(ControllerManager _enemyController)
        {
            this._enemyController = _enemyController;
            // this._controllerIndex = _controllerIndex;
            // this._playerUnitsSO = _playerUnitsSO;
            EnableActionsFunction();
        }
        private void OnEnable() {
            EnableActionsFunction();
        }
        private void EnableActionsFunction()
        {            
            if (_enemyController != null)
            {
            CausesPushBack _causesPushBack = _enemyController.GetComponent<CausesPushBack>();
            if (_causesPushBack != null)
            {
            _causesPushBack.playerPushBack += CalledPushBack;
            }
            }
        }
        private void OnDisable()
         {
            _enemyController.GetComponent<CausesPushBack>().playerPushBack -= CalledPushBack;
        }
        public void SendPlayerPosAndYSpeed(Vector2 _playerPos, float _ySpeed, GameObject[] _unitsGO)
        {
            
        }
        private void CalledPushBack(Vector2 _controllerPos, float _ySpeed)
        {
            if ((_ySpeed > 0) && (_controllerPos.y >= gameObject.transform.position.y))
            {
                GetComponent<Mover>().PushBackMovement = _ySpeed;
            }
        }
    }
}
