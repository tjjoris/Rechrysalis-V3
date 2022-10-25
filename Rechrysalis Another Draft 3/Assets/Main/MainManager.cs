using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;
using Rechrysalis.Attacking;
using Rechrysalis.Background;

namespace Rechrysalis
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] CompsAndUnitsSO _compsAndUnitsSO;        
        [SerializeField] ControllerManager[] _controllerManager;
        [SerializeField] PlayerUnitsSO[] _playerUnitsSO;  
        [SerializeField] CompSO[] _compSO;   
        [SerializeField] ProjectilesHolder _projectilesHolder;
        [SerializeField] BackgroundManager _backGroundManager;

        private void Awake() {
            // _compsAndUnitsSO.CompsSO = _compSO;
            _compSO = _compsAndUnitsSO.CompsSO;
            _compsAndUnitsSO.ControllerManagers = _controllerManager;
            GameMaster.GetSingleton().ReferenceManager.CompsAndUnitsSO = _compsAndUnitsSO;
            _projectilesHolder.Initialize();
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i=0; i<_controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null) 
                    {
                        _controllerManager[i].Initialize(i, _playerUnitsSO, _compSO[i], _controllerManager[GetOppositeController.ReturnOppositeController(i)], _compsAndUnitsSO);
                    }
                }
            }
            _backGroundManager?.Initialize();
        }

        private void FixedUpdate()
        {
            ResetTick();
            Tick();
        }
        private void ResetTick()
        {
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i = 0; i < _controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null)
                    {
                        _controllerManager[i].ResetTick();
                    }
                }
            }
        }
        private void Tick()
        {
            float _timeAmount = Time.fixedDeltaTime;
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i = 0; i < _controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null)
                    {
                        _controllerManager[i].FixedTick(_timeAmount);
                    }
                }
            }
            _projectilesHolder?.Tick(_timeAmount);
        }
    }
}
