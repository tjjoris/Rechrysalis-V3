using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ParentFreeEnemyManager : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        private bool _debugBool = false;
        private ParentUnitManager _parentUnitManager;
        private UnitClass _unitClass;
        private ParentHealth _parentHealth;
        private FreeEnemyApproach _freeApproach;
        private Mover _mover;
        private Attack _attack;
        // private AIAttackChargeUpTimer _aiAttackTimer;
        private bool _aiCanMove = true;
        private AIAlwaysPreferClosest _aiAlwaysPreferClosest;
        private FreeEnemyKiteMaxRange _freeEnemyKiteMaxRange;
        private CompsAndUnitsSO _compsAndUnits;
        private TargetScoreValue _targetScoreValue;
        [SerializeField] private UnitManager _unitManager;
        public UnitManager UnitManager {get {return _unitManager;}}
        private AIFlawedUpdate _aiFlawedUpdate;
        private int _controllerIndex;
        [SerializeField] private UnitManager _basicUnitManager;
        public UnitManager BasicUnitManager {get {return _basicUnitManager;}}
        [SerializeField] private UnitManager _chrysalisUnitManager;
        public UnitManager ChrysalisUnitManager => _chrysalisUnitManager;

        public void InitializeOld(int controllerIndex,UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _unitInWaveIndex, PlayerUnitsSO _ownUnits)
        {
            _parentUnitManager = GetComponent<ParentUnitManager>();
            this._compsAndUnits = _compsAndUnits;
            _controllerIndex = controllerIndex;
            _parentHealth = GetComponent<ParentHealth>();
            _aiFlawedUpdate = GetComponent<AIFlawedUpdate>();
            _unitManager?.InitializeOld(controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex, null);
            _unitManager?.SetUnitName(_unitStats.UnitName);
            GetComponent<Die>()?.Initialize(_compsAndUnits, controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(controllerIndex)], _controllerManager, _parentUnitManager);
            GetComponent<ParentClickManager>().Initialize(controllerIndex);
            GetComponent<ParentHealth>().CurrentUnit = _unitManager;
            _mover = GetComponent<Mover>();
            _mover.IsStopped = false;
            _mover?.SetBaseSpeed(_compsAndUnits.Speed);
            _attack = _unitManager.GetComponent<Attack>();
            // _aiAttackTimer = _unitManager.GetComponent<AIAttackChargeUpTimer>();
            _freeApproach = GetComponent<FreeEnemyApproach>();
            _freeApproach?.Initialize(_ownUnits, _compsAndUnits.ControllerManagers[GetOppositeController.ReturnOppositeController(_controllerIndex)].GetComponent<Mover>());
            _aiAlwaysPreferClosest = _unitManager.GetComponent<AIAlwaysPreferClosest>();
            _aiAlwaysPreferClosest.Initialize();
            _freeEnemyKiteMaxRange = GetComponent<FreeEnemyKiteMaxRange>();
            _freeEnemyKiteMaxRange?.Initialize();
            // _parentUnitManager.ParentUnitClass = new ParentUnitClass();
            // _parentUnitManager.ParentUnitClass. = _unitStats;


        }
        public void Initialize(ControllerManager controllerManager, UnitClass unitClass, int freeUnitIndex, CompsAndUnitsSO compsAndUnitSO, int controllerIndex)
        {
            _controllerManager = controllerManager;
            _controllerIndex = controllerIndex;
            _parentUnitManager = GetComponent<ParentUnitManager>();
            _unitClass = unitClass;
            // _attack.Initialize(unitClass);
            _unitManager?.Initialize(controllerManager, _controllerIndex, unitClass, freeUnitIndex,  compsAndUnitSO, false);
            _parentUnitManager.CurrentSubUnit = _unitManager.gameObject;
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _targetScoreValue?.Initialize();
            _targetScoreValue?.SetCurrentUnit(_unitManager.GetComponent<Attack>());

            this._compsAndUnits = compsAndUnitSO;
            _controllerIndex = controllerIndex;
            _parentHealth = GetComponent<ParentHealth>();
            _aiFlawedUpdate = GetComponent<AIFlawedUpdate>();
            // _unitManager?.InitializeOld(controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex, null);
            _unitManager?.SetUnitName(unitClass.UnitName);
            GetComponent<Die>()?.Initialize(_compsAndUnits, controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(controllerIndex)], _controllerManager, _parentUnitManager);
            GetComponent<ParentClickManager>()?.Initialize(controllerIndex);
            GetComponent<ParentHealth>().CurrentUnit = _unitManager;
            _mover = GetComponent<Mover>();
            _mover.IsStopped = false;
            _mover?.SetBaseSpeed(_compsAndUnits.Speed);
            _attack = _unitManager.GetComponent<Attack>();
            // _aiAttackTimer = _unitManager.GetComponent<AIAttackChargeUpTimer>();
            _freeApproach = GetComponent<FreeEnemyApproach>();
            _freeApproach?.Initialize(compsAndUnitSO.PlayerUnits[_controllerIndex], _compsAndUnits.ControllerManagers[GetOppositeController.ReturnOppositeController(_controllerIndex)].GetComponent<Mover>());
            _aiAlwaysPreferClosest = _unitManager.GetComponent<AIAlwaysPreferClosest>();
            _aiAlwaysPreferClosest?.Initialize();
            _freeEnemyKiteMaxRange = GetComponent<FreeEnemyKiteMaxRange>();
            _freeEnemyKiteMaxRange?.Initialize();
        }
        // private void OnEnable()
        // {
        //     // if (_mover != null)
        //     // if (GetComponent<Mover>() != null)
        //     // GetComponent<Mover>()._resetChargeUp += ResetChargeUp;
        //     if (_unitManager.GetComponent<AIAttackChargeUpTimer>() != null)
        //     {
        //         if (_debugBool)
        //         {
        //             Debug.Log($"subscribe to ai attack timer");
        //         }
        //     // _unitManager.GetComponent<AIAttackChargeUpTimer>()._changeCanMove += AICanMove;
        //     }
        // }
        // private void OnDisable()
        // {
        //     // if (_mover != null)
        //     //     _mover._resetChargeUp -= ResetChargeUp;
        //     if (GetComponent<AIAttackChargeUpTimer>() != null)
        //         GetComponent<AIAttackChargeUpTimer>()._changeCanMove += AICanMove;
        // }
        public void Tick (float _timeAmount)
        {   
            _aiFlawedUpdate?.Tick(_timeAmount);
            // bool _isRetreating = false;
            // if (_freeEnemyKiteMaxRange != null)
            // {
            //     _freeEnemyKiteMaxRange.Tick(_aiCanMove);            
            //     _isRetreating = _freeEnemyKiteMaxRange.GetRetreating();
            // }
            // if (_freeApproach != null)
            // {
            //     _freeApproach?.Tick(_isRetreating, _aiCanMove);
            //     if ((!_isRetreating) && (!_freeApproach.GetIsApproaching()))
            //     {
            //         _mover.SetDirection(Vector2.zero);
            //     }
            // }

            _aiAlwaysPreferClosest?.CheckIfTargetInRange();
            // _unitManager.IsStopped = _mover.IsStopped;
        }
        private void AICanMove(bool _aiCanMove)
        {
            // Debug.Log($"change can move" + _aiCanMove);
            this._aiCanMove = _aiCanMove;
        }
        // private void ResetChargeUp()
        // {
        //     // Debug.Log($" reset to charge up called");
        //     _attack?.CheckToResetChargeUp();
        // }
    }
}
