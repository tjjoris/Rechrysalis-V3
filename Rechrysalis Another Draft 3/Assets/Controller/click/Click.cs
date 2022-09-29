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
        public void Initialize(GameObject _controllerGO)
        {
            _clickInfo.ControlledController = _controllerGO;
        }        
    }
}
