using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class ParentFreeEnemyManager : MonoBehaviour
    {
        private UnitClass _unitClass;
        private ParentHealth _parentHealth;
        private FreeEnemyApproach _freeApproach;
        private Mover _mover;
        private Attack _attack;
        private AIAttackChargeUpTimer _aiAttackTimer;
        private bool _aiCanMove = true;
        private AIAlwaysPreferClosest _aiAlwaysPreferClosest;
        private FreeEnemyKiteMaxRange _freeEnemyKiteMaxRange;
        private CompsAndUnitsSO _compsAndUnits;
        [SerializeField] private UnitManager _unitManager;
        public UnitManager UnitManager {get {return _unitManager;}}

        public void InitializeOld(int _controllerIndex,UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _unitInWaveIndex, PlayerUnitsSO _ownUnits)
        {
            this._compsAndUnits = _compsAndUnits;
            _parentHealth = GetComponent<ParentHealth>();
            _unitManager?.InitializeOld(_controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex, null);
            _unitManager?.SetUnitName(_unitStats.UnitName);
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<ParentClickManager>().Initialize(_controllerIndex);
            GetComponent<ParentHealth>().CurrentUnit = _unitManager;
            _mover = GetComponent<Mover>();
            _mover.IsStopped = false;
            _mover?.SetSpeed(_compsAndUnits.Speed);
            _attack = _unitManager.GetComponent<Attack>();
            _aiAttackTimer = _unitManager.GetComponent<AIAttackChargeUpTimer>();
            _freeApproach = GetComponent<FreeEnemyApproach>();
            _freeApproach?.Initialize(_ownUnits, _unitManager.GetComponent<Range>());
            _aiAlwaysPreferClosest = _unitManager.GetComponent<AIAlwaysPreferClosest>();
            _aiAlwaysPreferClosest.Initialize();
            _freeEnemyKiteMaxRange = GetComponent<FreeEnemyKiteMaxRange>();
            _freeEnemyKiteMaxRange?.Initialize(_unitManager.GetComponent<TargetHolder>());
        }
        public void Initialize(UnitClass unitClass)
        {
            _unitClass = unitClass;
            _attack.Initialize(unitClass);
        }
        private void OnEnable()
        {
            // if (_mover != null)
            if (GetComponent<Mover>() != null)
            GetComponent<Mover>()._resetChargeUp += ResetChargeUp;
            if (_unitManager.GetComponent<AIAttackChargeUpTimer>() != null)
            {
                Debug.Log($"subscribe to ai attack timer");
            _unitManager.GetComponent<AIAttackChargeUpTimer>()._changeCanMove += AICanMove;
            }
        }
        private void OnDisable()
        {
            if (_mover != null)
                _mover._resetChargeUp -= ResetChargeUp;
            if (GetComponent<AIAttackChargeUpTimer>() != null)
                GetComponent<AIAttackChargeUpTimer>()._changeCanMove += AICanMove;
        }
        public void Tick (float _timeAmount)
        {   
            bool _isRetreating = false;
            _aiAlwaysPreferClosest.CheckIfTargetInRange();
            if (_freeEnemyKiteMaxRange != null)
            {
                _freeEnemyKiteMaxRange.Tick(_aiCanMove);            
                _isRetreating = _freeEnemyKiteMaxRange.GetRetreating();
            }
            if (_freeApproach != null)
            {
                _freeApproach?.Tick(_isRetreating, _aiCanMove);
                if ((!_isRetreating) && (!_freeApproach.GetIsApproaching()))
                {
                    _mover.SetDirection(Vector2.zero);
                }
            }
            _unitManager.IsStopped = _mover.IsStopped;
        }
        private void AICanMove(bool _aiCanMove)
        {
            // Debug.Log($"change can move" + _aiCanMove);
            this._aiCanMove = _aiCanMove;
        }
        private void ResetChargeUp()
        {
            // Debug.Log($" reset to charge up called");
            _attack?.CheckToResetChargeUp();
        }
    }
}
