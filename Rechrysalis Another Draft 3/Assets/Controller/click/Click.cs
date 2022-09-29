using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class Click : MonoBehaviour
    {
        [SerializeField] ClickInfo _clickInfo;
        public void Initialize(GameObject _controllerGO)
        {
            _clickInfo.ControlledController = _controllerGO;
        }        
    }
}
