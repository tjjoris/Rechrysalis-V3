using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="TouchInstance", menuName ="Controller/Touch/Touch")]
    public class Touch : ScriptableObject
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
            
        }
    }
}
