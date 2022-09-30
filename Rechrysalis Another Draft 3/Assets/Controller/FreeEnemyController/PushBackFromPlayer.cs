using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class PushBackFromPlayer : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        [SerializeField] private PlayerUnitsSO[] _playerUnitsSO;

        public void Initialize(int _controllerIndex, PlayerUnitsSO[] _playerUnitsSO)
        {
            this._controllerIndex = _controllerIndex;
            this._playerUnitsSO = _playerUnitsSO;
            EnableActions();
        }
        private void OnEnable() {
            EnableActions();
        }
        private void EnableActions()
        {
            // foreach ( _playerUnitsSO[GetOppositeController.ReturnOppositeController(_controllerIndex)].ActiveUnits
        }
        public void SendPlayerPosAndYSpeed(Vector2 _playerPos, float _ySpeed, GameObject[] _unitsGO)
        {
            if (_ySpeed > 0)
            {

            }
        }
    }
}
