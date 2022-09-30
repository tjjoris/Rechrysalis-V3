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
            _enemyController.GetComponent<Mover>().playerPushBack += CalledPushBack;
        }
        private void OnDisable()
         {
            _enemyController.GetComponent<Mover>().playerPushBack -= CalledPushBack;
        }
        public void SendPlayerPosAndYSpeed(Vector2 _playerPos, float _ySpeed, GameObject[] _unitsGO)
        {
            
        }
        private void CalledPushBack(Vector2 _controllerPos, float _ySpeed)
        {

        }
    }
}
