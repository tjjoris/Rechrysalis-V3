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
        
        public void CheckRayCastDownFunction(Vector2 _mousePos, int _touchID)
        {
                LayerMask _stopMask = LayerMask.GetMask("PlayerController");
                RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector2.zero, _stopMask);
                if (hit)
                {
                    _clickInfo.ControlledController.GetComponent<Mover>().IsStopped = true;
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
                _clickInfo.FingerIDMove = _touchID;
                Vector2 _direction = _clickInfo.ControlledController.transform.position;
                _direction = _mousePos - _direction;
                _clickInfo.ControlledController.GetComponent<Mover>().Direction = _direction;
                _clickInfo.ControlledController.GetComponent<Mover>().IsStopped = false;
            }
        }
    }
}
