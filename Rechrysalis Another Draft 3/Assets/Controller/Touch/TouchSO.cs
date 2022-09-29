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
        public void Initialize(GameObject _controllerGO)
        {
            _clickInfo.ControlledController = _controllerGO;
            _checkRayCast.ClickInfo = _clickInfo;
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
                    _checkRayCast.CheckRayCastFunction(Camera.main.ScreenToWorldPoint(t.position), t.fingerId);
                }
                i++;
            }
        }
    }
}
