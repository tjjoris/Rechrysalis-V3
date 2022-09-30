using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] ControllerManager[] _controllerManager;
        [SerializeField] FreeEnemyControllerManager[] _freeEnemyControllerManager;

        private void Awake() {
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i=0; i<_controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null) 
                    {
                        _controllerManager[i].Initialize();
                    }
                }
            }
            if ((_freeEnemyControllerManager != null) && (_freeEnemyControllerManager.Length > 0))
            {
                for (int i = 0; i < _freeEnemyControllerManager.Length; i++)
                {
                    if (_freeEnemyControllerManager[i] != null)
                    {
                        _freeEnemyControllerManager[i].Initialize();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i = 0; i < _controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null)
                    {
                        _controllerManager[i].Tick();
                    }
                }
            }
            if ((_freeEnemyControllerManager != null) && (_freeEnemyControllerManager.Length > 0))
            {
                for (int i = 0; i < _freeEnemyControllerManager.Length; i++)
                {
                    if (_freeEnemyControllerManager[i] != null)
                    {
                        _freeEnemyControllerManager[i].Tick();
                    }
                }
            }
        }
    }
}
