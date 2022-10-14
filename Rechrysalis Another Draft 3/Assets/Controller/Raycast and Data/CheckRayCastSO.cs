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
        private UpgradeRingManager _upgradeRingManager;
        private float _unitRingOuterRadius;
        public enum TouchTypeEnum {nothing, controller, map, friendlyUnit, unitRing, menu, other }
        private TouchTypeEnum[] _touchTypeArray = new TouchTypeEnum[5];
        private int[] _upgradeCountArray;
        private int _unitUpgrading;

        
        public void Initialize(CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRIngManager, UpgradeRingManager _upgradeRingManager, float _unitRingOuterRadius)
        {
            this._upgradeRingManager = _upgradeRingManager;
            this._compsAndUnits = _compsAndUnits;
            _playerTargtList = _compsAndUnits.TargetsLists[0];
            _playerTargtList.Initialize();
            _controller = _compsAndUnits.ControllerManagers[0].gameObject;
            this._unitRingManager = _unitRIngManager;
            this._unitRingOuterRadius = _unitRingOuterRadius;
            _upgradeCountArray = _compsAndUnits.CompsSO[0].UpgradeCountArray;
            for (int _touchTypeIndex = 0; _touchTypeIndex < _touchTypeArray.Length; _touchTypeIndex ++)
            {
                _touchTypeArray[_touchTypeIndex] = TouchTypeEnum.nothing;
            }
        }
        public void CheckRayCastDownFunction(Vector2 _mousePos, int _touchID)
        {
            Vector3 _mousePosV3;
            RaycastHit2D hit;
            CreateRayCastFunction(_mousePos, out _mousePosV3, out hit);
            if (hit)
            {
                if (ControllerMouseOver(hit))
                {
                    _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(true);
                    _touchTypeArray[_touchID] = TouchTypeEnum.controller;
                }
                if ((UnitMouseOver(hit)) && (EnemyUnitHitCollider(hit.collider.gameObject.GetComponent<UnitManager>())))
                {
                    Debug.Log($"click enemy");
                    _playerTargtList.SetNewTarget(hit.collider.gameObject);
                    _touchTypeArray[_touchID] = TouchTypeEnum.other;
                }
            }
            else if (UnitRingMouseOver(_mousePos, _controller.transform.position))
            {
                int _unitInbounds = checkIfIntUnitBounds(_mousePos);
                if (_unitInbounds != -1)
                {
                    _touchTypeArray[_touchID] = TouchTypeEnum.friendlyUnit;
                    Debug.Log($"friendly unit " + _unitInbounds);
                }
            }
            else if (false)
            {
                //menu clicked
                _touchTypeArray[_touchID] = TouchTypeEnum.other;
            }
            else
            {//map clicked
                MapClicked(_mousePos, _touchID);
                _touchTypeArray[_touchID] = TouchTypeEnum.map;
            }
        }

        private void MapClicked(Vector2 _mousePos, int _touchID)
        {
            _clickInfo.FingerIDMove = _touchID;
            Vector2 _direction = _clickInfo.ControlledController.transform.position;
            _direction = _mousePos - _direction;
            _clickInfo.ControlledController.GetComponent<Mover>().Direction = _direction;
            // _clickInfo.ControlledController.GetComponent<Mover>().IsStopped = false;
            // _clickInfo.ControlledController.GetComponent<ControllerManager>().IsStopped = false;
            _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(false);
        }

        private static void CreateRayCastFunction(Vector2 _mousePos, out Vector3 _mousePosV3, out RaycastHit2D hit)
        {
            _mousePosV3 = _mousePos;
            LayerMask _mask = ~LayerMask.GetMask("PlayerController");
            _mask += LayerMask.GetMask("Unit");
            hit = Physics2D.Raycast(_mousePos, Vector2.zero, _mask);
        }
        private bool ControllerMouseOver(RaycastHit2D _hit)
        {
            if (_hit.collider.gameObject.layer == 6)
            {
                return true;
            }
            else return false;
        }
        private bool UnitMouseOver(RaycastHit2D _hit)
        {
            if (_hit.collider.gameObject.layer == 7)
            {
                return true;
            }
            else return false;
        }
        private bool EnemyUnitHitCollider(UnitManager _unitManager)
        {
            if ((_unitManager != null) && (_unitManager.ControllerIndex == 1))
            {
                return true;
            }
            return false;
        }
        private bool UnitRingMouseOver(Vector2 _mousePos, Vector2 _controllerPos)
        {
            if ((_mousePos - _controllerPos).magnitude <= _unitRingOuterRadius)
            {
                return true;
            }
            return false;
        }

        public void CheckRayCastReleaseFunction(Vector2 _mousePos, int _touchID)
        {
            Debug.Log($"release");
            Vector3 _mousePosV3;
            RaycastHit2D hit;
            CreateRayCastFunction(_mousePos, out _mousePosV3, out hit);  
            if (_touchTypeArray[_touchID] == TouchTypeEnum.friendlyUnit)
            {          
                if (ControllerMouseOver(hit));
                else if (UnitRingMouseOver(_mousePos, _controller.transform.position))
                {
                    int _unitToUpgradeTo = CheckIfInUnitBoundsWithAngle(RingAngle(_mousePos), _upgradeCountArray[_unitUpgrading], _upgradeRingManager.CurrentAngle, _unitRingManager.UnitDegreeWidth);
                    Debug.Log($"upgrade to " + _unitToUpgradeTo);
                }
            }
            _touchTypeArray[_touchID] =TouchTypeEnum.nothing;
        }
        private int checkIfIntUnitBounds(Vector3 _mousePos)
        {
            // if (_unitRingManager.GetUnitDegreeWidthArray().Length > 0)
            // {
            //     for (int _parentUnitIndex = 0; _parentUnitIndex < (_unitRingManager.GetUnitDegreeWidthArray().Length / 2); _parentUnitIndex++)
            //     {
            //         if ((RingAngle(_mousePos) >= _unitRingManager.GetUnitDegreeWidthArray()[_parentUnitIndex]) && (RingAngle(_mousePos) <= _unitRingManager.GetUnitDegreeWidthArray()[_parentUnitIndex + 1]))
            //         {
            //             Debug.Log($"Unit clicked");
            //         }
            //     }
            // }
            // return -1;
            return CheckIfInUnitBoundsWithAngle(RingAngle(_mousePos), _compsAndUnits.CompsSO[0].ParentUnitCount, _unitRingManager.UnitRingAngle, _unitRingManager.UnitDegreeWidth);
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
