using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="TouchInstance", menuName ="Controller/Touch/TouchSO")]
    public class TouchSO : ScriptableObject
    {
        [SerializeField] private ClickInfo _clickInfo;
        [SerializeField] private CheckRayCastSO _checkRayCast;
        [SerializeField] private CompsAndUnitsSO _compsAndUnits;
        public void Initialize(GameObject _controllerGO, CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRingManager, CheckRayCastSO _checkRayCast)
        {
            _clickInfo.ControlledController = _controllerGO;
            _checkRayCast.ClickInfo = _clickInfo;
            this._compsAndUnits = _compsAndUnits;
            // _checkRayCast?.Initialize(_compsAndUnits, _unitRingManager);
            this._checkRayCast = _checkRayCast;
        }
        public void Tick()
        {
            CheckTouch();            
        }
        private void CheckTouch()
        {
            int i=0;
            while (i<Input.touchCount)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                {
                    _checkRayCast.CheckRayCastDownFunction(Camera.main.ScreenToWorldPoint(t.position), t.fingerId);
                }   
                else if ((t.phase == TouchPhase.Moved) || (t.phase == TouchPhase.Stationary))             
                {
                    _checkRayCast.CheckRayCastMoveFunction(Camera.main.ScreenToWorldPoint(t.position), t.fingerId);                    
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    _checkRayCast.CheckRayCastReleaseFunction(Camera.main.ScreenToWorldPoint(t.position), t.fingerId);
                }
                i++;
            }
        }
    }
}
