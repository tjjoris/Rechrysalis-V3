using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;
using Rechrysalis.CameraControl;
using Rechrysalis.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Rechrysalis.Controller
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CheckRayCastInstance", menuName = "Controller/Click/CheckRayCast")]
    public class CheckRayCastSO : ScriptableObject
    {
        private bool _debugBool = false;
        private MainManager _mainManager;
        [SerializeField] private CompsAndUnitsSO _compsAndUnits;
        private TargetsListSO _playerTargtList;
        [SerializeField] private ClickInfo _clickInfo;
        public ClickInfo ClickInfo {set {_clickInfo = value;} get{return _clickInfo;}}
        private GameObject _controller;
        private float _ringSize;
        private float _controllerRadius = 0.5f;
        private UnitRingManager _unitRingManager;
        private HilightRingManager _hilightRingManager;
        private UpgradeRingManager _upgradeRingManager;
        private float _unitRingOuterRadius;
        // private float _controllerRadius;
        public enum TouchTypeEnum {nothing, controller, map, friendlyUnit, unitRing, menu, other }
        [SerializeField]private TouchTypeEnum[] _touchTypeArray = new TouchTypeEnum[5];
        [SerializeField] private Vector2[] _touchPosDown = new Vector2[5];
        private int[] _upgradeCountArray;
        private int _unitUpgrading;
        private ControllerManager _controllerManager;
        [SerializeField] private TransitionTargetingCamera _transitionTargetingCamera;
        // [SerializeField] private bool _targetDuringTargetMode;
        // public bool TargetDuringTargetMode { get => _targetDuringTargetMode; set => _targetDuringTargetMode = value; }
        [SerializeField] private PlayerPrefsInteract _plyaerPrefsInteract;
        [SerializeField] private GraphicRaycaster _graphicsRaycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Transform _cameraGOScroll;
        
        private int _controllerIndex = 0;

        
        public void Initialize(CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRIngManager, HilightRingManager _hilightRingManager, UpgradeRingManager _upgradeRingManager, float _unitRingOuterRadius, TransitionTargetingCamera transitionTargetingCamera, MainManager mainManager, GraphicRaycaster graphicRaycaster, Transform cameraGOScroll)
        {
            _cameraGOScroll = cameraGOScroll;
            _graphicsRaycaster = graphicRaycaster;
            _mainManager = mainManager;
            _eventSystem = _mainManager.EventSystem;
            _plyaerPrefsInteract = _mainManager.PlayerPrefsInteract;
            _transitionTargetingCamera = transitionTargetingCamera;
            this._upgradeRingManager = _upgradeRingManager;
            this._compsAndUnits = _compsAndUnits;
            _playerTargtList = _compsAndUnits.TargetsLists[0];
            _playerTargtList.Initialize();
            _controller = _compsAndUnits.ControllerManagers[0].gameObject;
            this._unitRingManager = _unitRIngManager;
            this._hilightRingManager = _hilightRingManager;
            this._unitRingOuterRadius = _unitRingOuterRadius;
            _upgradeCountArray = _compsAndUnits.CompsSO[0].UpgradeCountArray;
            for (int _touchTypeIndex = 0; _touchTypeIndex < _touchTypeArray.Length; _touchTypeIndex ++)
            {
                _touchTypeArray[_touchTypeIndex] = TouchTypeEnum.nothing;
            }
            _controllerManager = _clickInfo.ControlledController.GetComponent<ControllerManager>();            
        }
        private void ResetAllTouchType()
        {
            for (int i=0; i< _touchTypeArray.Length; i++)
            {
                _touchTypeArray[i] = TouchTypeEnum.nothing;
            }
        }
        public void CheckRayCastDownFunction(Vector2 _mousePos, int _touchID)
        {
            if (_mainManager.Paused)
            {
                return;
            }
            Vector3 _mousePosV3;
            RaycastHit2D hit;
            CreateRayCastFunction(_mousePos, out _mousePosV3, out hit);
            // if ((hit) && (ControllerMouseOver(hit)))
            // {
            if (_transitionTargetingCamera.InTargetMode)
            {
                _touchPosDown[_touchID] = _mousePos;
            }
            if (CheckIfInteractingWithInteractableUI());
            else if ((!_transitionTargetingCamera.InTargetMode) && (hit) && (ControllerMouseOver(hit)))
            {
                // _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(true);
                // _clickInfo.ControlledController.GetComponent<Mover>().SetDirection(Vector2.zero);
                _touchTypeArray[_touchID] = TouchTypeEnum.controller;
            }
            else if ((hit) && (UnitMouseOver(hit)) && (hit.collider.gameObject.GetComponent<ParentClickManager>().IsEnemy(_controllerIndex)) && ((!_plyaerPrefsInteract.GetTargetOnlyDuringTargetMode()) || (_transitionTargetingCamera.InTargetMode)))
            {
                // Debug.Log($"click enemy");
                _playerTargtList.SetNewTarget(hit.collider.gameObject);
                _touchTypeArray[_touchID] = TouchTypeEnum.other;
            }
            // }
            else if ((!_transitionTargetingCamera.InTargetMode) && (UnitRingMouseOver(_mousePos, _controller.transform.position)))
            {
                int _unitInbounds = checkIfIntUnitBounds(_mousePos);
                if ((_unitInbounds != -1) && (_controllerManager.ParentUnits[_unitInbounds] != null))
                {
                    _hilightRingManager.SetOldAngle(RingAngle(_mousePos));
                    _unitUpgrading = _unitInbounds;
                    _touchTypeArray[_touchID] = TouchTypeEnum.friendlyUnit;
                    _upgradeRingManager.SetCurrentAngle(_unitRingManager.UnitRingAngle);
                    _upgradeRingManager.SetActiveUpgradeRing(_unitInbounds);
                    _controllerManager.HideUnitText();
                    if (_debugBool)
                    {
                        Debug.Log($"friendly unit " + _unitInbounds);
                    }
                }
                else if (!_transitionTargetingCamera.InTargetMode)
                {
                    _hilightRingManager.SetOldAngle(RingAngle(_mousePos));
                    _touchTypeArray[_touchID] = TouchTypeEnum.unitRing;
                    _hilightRingManager.gameObject.SetActive(true);
                }
                else
                {
                    _touchTypeArray[_touchID] = TouchTypeEnum.other;
                }
            }
            else
            {//map clicked
                if (!_transitionTargetingCamera.InTargetMode)
                {
                    MapClicked(_mousePos, _touchID);
                }
                else 
                {
                    _touchPosDown[_touchID] = _mousePos;
                }
                _touchTypeArray[_touchID] = TouchTypeEnum.map;
            }
            // else
            // {
            //     _touchTypeArray[_touchID] = TouchTypeEnum.other;
            // }
        }

        private bool CheckIfInteractingWithInteractableUI()
        {
            List<RaycastResult> graphicResults = new List<RaycastResult>();
            PointerEventData _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            _graphicsRaycaster.Raycast(_pointerEventData, graphicResults);
            foreach (RaycastResult result in graphicResults)
            {
                if (result.gameObject.layer == 11)
                {
                    return true;
                }
            }
            return false;
        }

        private void MapClicked(Vector2 _mousePos, int _touchID)
        {
            _clickInfo.FingerIDMove = _touchID;
            Vector2 _direction = _clickInfo.ControlledController.transform.position;
            _direction = _mousePos - _direction;
            // _clickInfo.ControlledController.GetComponent<Mover>().Direction = _direction;
            _clickInfo.ControlledController.GetComponent<Mover>().SetDirection(_direction);
            _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(false);
        }

        private static void CreateRayCastFunction(Vector2 _mousePos, out Vector3 _mousePosV3, out RaycastHit2D hit)
        {
            _mousePosV3 = _mousePos;
            LayerMask _mask = LayerMask.GetMask("PlayerController");
            _mask += LayerMask.GetMask("Unit");
            hit = Physics2D.Raycast(_mousePos, Vector2.zero, 2f, _mask);
        }
        private bool ControllerMouseOver(RaycastHit2D _hit)
        {
            if (_hit.collider.gameObject.layer == 6)
            {
                if (_debugBool)
                {
                    Debug.Log($"collided " + _hit.collider.gameObject.name);
                }
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
        // private bool EnemyUnitHitCollider(UnitManager _unitManager)
        // {
        //     if ((_unitManager != null) && (_unitManager.ControllerIndex == 1))
        //     {
        //         return true;
        //     }
        //     return false;
        // }
        private bool InController(Vector2 _mousePos)
        {

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
        public void CheckRayCastMoveFunction(Vector2 _mousePos, int _touchID)
        {
            LayerMask _mask = LayerMask.GetMask("PlayerController");
            // int _layer = 6;
            // LayerMask _mask = 1 << _layer;
            // int _results = 10;
            RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector2.zero, 2f, _mask);
            // if ((_transitionTargetingCamera.InTargetMode) && (_touchTypeArray[_touchID] == TouchTypeEnum.other))
            // {
            //     if (_mousePos.y > _touchPosDown[_touchID].y + _controllerRadius)
            //     {
            //         _transitionTargetingCamera.TransitionToController();
            //     }
            // }
            if ((_transitionTargetingCamera.InTargetMode) && (_touchTypeArray[_touchID] == TouchTypeEnum.map))
            {
                _cameraGOScroll.localPosition = _touchPosDown[_touchID] - _mousePos;
            }
            else if ((_touchTypeArray[_touchID] == TouchTypeEnum.controller) && (_mousePos.y < _controller.transform.position.y - _controllerRadius))
            {
                _transitionTargetingCamera.TransitionToTargeting();
                ResetAllTouchType();
            }
            else if (((_touchTypeArray[_touchID] == TouchTypeEnum.friendlyUnit) || (_touchTypeArray[_touchID] == TouchTypeEnum.unitRing)) && (hit))
            {
                _hilightRingManager.SetAngleToUnitRing();
            }
            else if (_touchTypeArray[_touchID] == TouchTypeEnum.unitRing)
            {
                // else if (_touchTypeArray[_touchID] == TouchTypeEnum.unitRing)
                {
                    // Debug.Log($"mouse pos " + _mousePos + " controller " + _controller.transform.position);
                    // Debug.Log($"calculated angel " + Mathf.Atan2(_mousePos.y - _controller.transform.position.y, _mousePos.x - _controller.transform.position.x) * Mathf.Rad2Deg);
                    if (_debugBool)
                    {
                        Debug.Log($"set ring angle " + RingAngle(_mousePos));
                    }
                    _hilightRingManager.SetAngle(RingAngle(_mousePos));
                }
                // Debug.Log($"mouse angle " + RingAngle(_mousePos));
            }
            else if (_touchTypeArray[_touchID] == TouchTypeEnum.friendlyUnit)
            {
                if (hit);
                else if ((UnitRingMouseOver(_mousePos, _controller.transform.position)) && (CheckIfSingleUpgradeTrue(RingAngle(_mousePos), (_unitRingManager.UnitRingAngle + AnglesMath.UnitAngle(_unitUpgrading, _compsAndUnits.CompsSO[0].ParentUnitCount) + 90f), _unitRingManager.UnitDegreeWidth)));
                else 
                {
                    if (_debugBool)
                    {
                        Debug.Log($"set ring angle " + RingAngle(_mousePos));
                    }
                    _hilightRingManager.SetAngle(RingAngle(_mousePos));
                }
            }
        }
        public void CheckRayCastReleaseFunction(Vector2 _mousePos, int _touchID)
        {
            
            Vector3 _mousePosV3;
            RaycastHit2D hit;
            CreateRayCastFunction(_mousePos, out _mousePosV3, out hit);
            // Debug.Log($"hit" + hit.collider.name);
            // Debug.Log($"controller pos " + _controller.transform.position);
            if ((hit) && (ControllerMouseOver(hit)) && (_touchTypeArray[_touchID] == TouchTypeEnum.controller))
            {
                _clickInfo.ControlledController.GetComponent<ControllerManager>().SetIsStopped(true);
                _clickInfo.ControlledController.GetComponent<Mover>().SetDirection(Vector2.zero);
            } 
            if (_touchTypeArray[_touchID] == TouchTypeEnum.friendlyUnit)
            {      
                // Debug.Log($"mouse pos" + _mousePos);
                // Debug.Log($"controller pos " + _controller.transform.position);
                if ((hit) && (ControllerMouseOver(hit)));
                else if (UnitRingMouseOver(_mousePos, _controller.transform.position))
                {
                    // Debug.Log($" ring angle " + RingAngle(_mousePos) + "unit count " + _upgradeCountArray[_unitUpgrading]);
                    int _unitToUpgradeTo = CheckIfInUnitBoundsWithAngle(RingAngle(_mousePos), _upgradeCountArray[_unitUpgrading], (_upgradeRingManager.CurrentAngle + AnglesMath.UnitAngle(_unitUpgrading, _compsAndUnits.CompsSO[0].ParentUnitCount)), _unitRingManager.UnitDegreeWidth);
                    // Debug.Log($"upgrade to " + _unitToUpgradeTo);
                    // _controllerManager.ActivateChrysalis(_unitUpgrading, _unitToUpgradeTo);
                    if(CheckIfSingleUpgradeTrue(RingAngle(_mousePos), (_unitRingManager.UnitRingAngle + AnglesMath.UnitAngle(_unitUpgrading, _compsAndUnits.CompsSO[0].ParentUnitCount) + 90f), _unitRingManager.UnitDegreeWidth))                            
                    {
                        if (_debugBool)
                        {
                            Debug.Log($"upgrade to adv");
                        }
                    _controllerManager.ActivateChrysalis(_unitUpgrading, 1);
                    }
                }
                // else 
                // {
                //     // int _unitToUpgradeTo = CheckIfInUnitBoundsWithAngle(RingAngle(_mousePos), _upgradeCountArray[_unitUpgrading], (_upgradeRingManager.CurrentAngle + AnglesMath.UnitAngle(_unitUpgrading, _compsAndUnits.CompsSO[0].ParentUnitCount)), _unitRingManager.UnitDegreeWidth);
                //     // _controllerManager.ReserveChrysalis(_unitUpgrading, _unitToUpgradeTo);
                // }
                _upgradeRingManager.SetActiveUpgradeRing(-1);
                _controllerManager.ShowUnitText();
            }
            if ((_touchTypeArray[_touchID] == TouchTypeEnum.unitRing) && (_hilightRingManager.gameObject.activeInHierarchy == true))
            {
                _hilightRingManager.gameObject.SetActive(false);
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
            _mouseAngleCurrent = AnglesMath.LimitAngle(_mouseAngleCurrent - 90);
            // Debug.Log($"mouse angle" + _mouseAngleCurrent + " angle offset " + _angleOffset);
            if (_unitCount > 0)
            {
                for (int _unitIndex = 0; _unitIndex < _unitCount; _unitIndex++)
                {
                    float _angleToCompare = AnglesMath.LimitAngle(((360 / _unitCount) * _unitIndex) - -_angleOffset);
                    // Debug.Log($"angle to compare " + _angleToCompare);
                    float _mouseSubtractAngle = _mouseAngleCurrent - _angleToCompare;
                    if ((Mathf.Abs(_mouseSubtractAngle) < _unitWidthDegrees) || ((Mathf.Abs(_mouseSubtractAngle) > (360 - _unitWidthDegrees))))                    
                    {
                        return _unitIndex;
                    }
                }
            }
            return -1;
        }
        private bool CheckIfSingleUpgradeTrue(float mouseAngleCurrent, float ringAngleOffset, float unitWidthDegrees)
        {
            float angleToCompare = AnglesMath.LimitAngle(ringAngleOffset);
            float mouseSubtractAngle = mouseAngleCurrent - angleToCompare;
            if ((Mathf.Abs(mouseSubtractAngle) < unitWidthDegrees) || ((Mathf.Abs(mouseSubtractAngle) > (360 - unitWidthDegrees))))
            {
                return true;
            }
            return false;
        }
        // private float UnitAngle (int _unitIndex, int _maxUnits)
        // {
        //     return AnglesMath.LimitAngle((360 / _maxUnits) * _unitIndex);
        // }
        private float RingAngle(Vector3 _mousePos)
        {
            Vector3 _mouseDiff = _mousePos - _controller.transform.position;
            return AnglesMath.V2ToDegrees(new Vector2( _mouseDiff.x, _mouseDiff.y));
        }
    }
}
