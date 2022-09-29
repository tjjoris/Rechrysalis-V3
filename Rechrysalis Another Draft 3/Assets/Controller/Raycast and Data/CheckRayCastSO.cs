using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CheckRayCastInstance", menuName = "Controller/Click/CheckRayCast")]
    public class CheckRayCastSO : ScriptableObject
    {
        [SerializeField] private ClickInfo _clickInfo;
        public ClickInfo ClickInfo {set {_clickInfo = value;} get{return _clickInfo;}}
        
        public void CheckRayCastFunction(Vector2 _mousePos, int _touchID)
        {
            Debug.Log("called check");
            if (false)
            {
                //controller click to stop
            }
            else if (false)
            {
                //unit click to focus fire or upgrade
            }
            else if (false)
            {
                //ring to rotate
            }
            else if (false)
            {
                //menu clicked
            }
            else 
            {//map clicked
                Debug.Log("map clicked " + _mousePos.ToString());
                _clickInfo.TouchID = _touchID;
            }
        }
    }
}
