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

        public void Initialize(GameObject _controllerGO)
        {
            _clickInfo.ControlledController = _controllerGO;
            _checkRayCast.ClickInfo = _clickInfo;
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
