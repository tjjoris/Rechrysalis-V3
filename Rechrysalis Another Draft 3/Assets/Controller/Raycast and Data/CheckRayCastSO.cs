using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;

namespace Rechrysalis.Controller
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CheckRayCastInstance", menuName = "Controller/Click/CheckRayCast")]
    public class CheckRayCastSO : ScriptableObject
    {
        [SerializeField] private CompsAndUnitsSO _compsAndUnits;
        private TargetsListSO _playerTargtList;
        [SerializeField] private ClickInfo _clickInfo;
        public ClickInfo ClickInfo {set {_clickInfo = value;} get{return _clickInfo;}}
        private GameObject _controller;
        private float _ringSize;
        private UnitRingManager _unitRingManager;
        
        public void Initialize(CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRIngManager)
        {
            this._compsAndUnits = _compsAndUnits;
            _playerTargtList = _compsAndUnits.TargetsLists[0];
            _playerTargtList.Initialize();
            _controller = _compsAndUnits.ControllerManagers[0].gameObject;
            this._unitRingManager = _unitRIngManager;
        }
        public void CheckRayCastDownFunction(Vector2 _mousePos, int _touchID)
        {
            Vector3 _mousePosV3 = _mousePos;
            LayerMask _mask = ~LayerMask.GetMask("PlayerController");
            _mask += LayerMask.GetMask("Unit");
            RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector2.zero, _mask);
            if (hit) 
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    Debug.Log($"stop");
                    // _clickInfo.ControlledController.GetComponent<Mover>().IsStopped = true;
                    // _clickInfo.ControlledController.GetComponent<ControllerManager>().IsStopped = true;
                    _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(true);
                }
                else if (hit.collider.gameObject.layer == 7)
                {
                    UnitManager _unitManager = hit.collider.gameObject.GetComponent<UnitManager>();
                    if (_unitManager != null)
                    {
                        Debug.Log($"clicked unit");
                    }
                    if ((_unitManager != null) && (_unitManager.ControllerIndex == 1))
                    {
                        Debug.Log($"click enemy");
                        _playerTargtList.SetNewTarget(hit.collider.gameObject);
                    }
                }
            }
            else if ((_mousePosV3 - _controller.transform.position).magnitude < 10)           
            {
                //clicked inside ring
                checkIfIntUnitBounds(_mousePos);
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
                // _clickInfo.ControlledController.GetComponent<Mover>().IsStopped = false;
                // _clickInfo.ControlledController.GetComponent<ControllerManager>().IsStopped = false;
                _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(false);
            }
        }
        public void CheckRayCastReleaseFunction(Vector2 _mousPos, int _touchID)
        {
            
        }
        private int checkIfIntUnitBounds(Vector3 _mousePos)
        {
            if (_unitRingManager.GetUnitDegreeWidthArray().Length > 0)
            {
                for (int _parentUnitIndex = 0; _parentUnitIndex < (_unitRingManager.GetUnitDegreeWidthArray().Length / 2); _parentUnitIndex++)
                {
                    if ((RingAngle(_mousePos) >= _unitRingManager.GetUnitDegreeWidthArray()[_parentUnitIndex]) && (RingAngle(_mousePos) <= _unitRingManager.GetUnitDegreeWidthArray()[_parentUnitIndex + 1]))
                    {
                        Debug.Log($"Unit clicked");
                    }
                }
            }
            return -1;
        }
        private int CheckIfInUnitBoundsWithAngle(float _mouseAngleCurrent, int _unitCount, float _angleOffset, float _unitWidthDegrees)
        {
            if (_unitCount > 0)
            {
                for (int _unitIndex = 0; _unitIndex < _unitCount; _unitIndex++)
                {
                    if ((_mouseAngleCurrent >= (((360 / _unitCount) * _unitIndex) - _unitWidthDegrees + _angleOffset)) && (_mouseAngleCurrent <= (((360 / _unitCount) * _unitIndex) + _unitWidthDegrees + _angleOffset)))
                    {
                        return _unitIndex;
                    }
                }
            }
            return -1;
        }
        private float RingAngle(Vector3 _mousePos)
        {
            Vector3 _mouseDiff = _mousePos - _controller.transform.position;
            return AnglesMath.V2ToDegrees(new Vector2( _mouseDiff.x, _mouseDiff.y));
        }
    }
}
