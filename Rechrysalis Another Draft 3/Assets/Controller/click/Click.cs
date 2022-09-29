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
        private void OnMouseDown() {
            Debug.Log("check");
            _checkRayCast.CheckRayCastFunction(Input.mousePosition);
        }
    }
}
