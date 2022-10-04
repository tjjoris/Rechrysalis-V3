using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

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
            LayerMask _mask = ~LayerMask.GetMask("PlayerController");
            _mask += LayerMask.GetMask("Unit");
            RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector2.zero, _mask);
            if (hit) 
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    Debug.Log($"stop");
                    _clickInfo.ControlledController.GetComponent<Mover>().IsStopped = true;
                }
                else if (hit.collider.gameObject.layer == 7)
                {
                    Debug.Log("unitHit");
                    UnitManager _unitManager = hit.collider.gameObject.GetComponent<UnitManager>();
                    if (_unitManager != null)
                    {
                        Debug.Log($"clicked unit");
                    }
                    if ((_unitManager != null) && (_unitManager.ControllerIndex == 1))
                    {
                        Debug.Log($"click enemy");
                    }
                }
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
