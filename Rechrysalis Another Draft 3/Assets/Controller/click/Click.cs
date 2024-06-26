using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Click", menuName = "Controller/Click/Click")]
    public class Click : ScriptableObject
    {
        [SerializeField] ClickInfo _clickInfo;
        [SerializeField] CheckRayCastSO _checkRayCast;
        [SerializeField] CompsAndUnitsSO _compsAndUnits;
        private bool _mouseButtonDown;

        public void Initialize(GameObject _controllerGO, CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRingManager, CheckRayCastSO _checkRayCast)
        {
            _mouseButtonDown = false;
            _clickInfo.ControlledController = _controllerGO;
            _checkRayCast.ClickInfo = _clickInfo;
            this._compsAndUnits = _compsAndUnits;
            // _checkRayCast.Initialize(_compsAndUnits, _unitRingManager);
            this._checkRayCast = _checkRayCast;
        }        
        public void Tick()
        {
            CheckIfMouseIsHeld();
            CheckIfMouseDown();
            CheckIfMouseUp();
        }
        private void CheckIfMouseIsHeld()
        {
            if ((Input.GetMouseButton(0)) && (_mouseButtonDown))
            {
                _checkRayCast.CheckRayCastMoveFunction(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, Input.mousePosition);
            }
        }
        private void CheckIfMouseDown() {
            if ((Input.GetMouseButtonDown(0)) && (!_mouseButtonDown))
            {
            // Debug.Log("check");
            _checkRayCast.CheckRayCastDownFunction(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, Input.mousePosition);
            _mouseButtonDown = true;
            }
        }
        private void CheckIfMouseUp()
        {
            if ((_mouseButtonDown) && (Input.GetMouseButtonUp(0)))
            {
                _mouseButtonDown = false;
                _checkRayCast.CheckRayCastReleaseFunction(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0);
                
            }
        }
    }
}
