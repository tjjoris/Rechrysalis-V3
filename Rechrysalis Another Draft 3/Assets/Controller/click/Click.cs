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

        public void Initialize(GameObject _controllerGO, CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRingManager)
        {
            _clickInfo.ControlledController = _controllerGO;
            _checkRayCast.ClickInfo = _clickInfo;
            this._compsAndUnits = _compsAndUnits;
            _checkRayCast.Initialize(_compsAndUnits, _unitRingManager);
        }        
        public void Tick()
        {
            CheckIfMouseDown();
        }
        private void CheckIfMouseDown() {
            if (Input.GetMouseButtonDown(0))
            {
            Debug.Log("check");
            _checkRayCast.CheckRayCastDownFunction(Camera.main.ScreenToWorldPoint(Input.mousePosition), -1);
            }
        }
    }
}
