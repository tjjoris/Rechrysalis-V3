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
        private ParentHealth _parentHealth;
        private FreeEnemyApproach _freeApproach;
        private Mover _mover;
        private AIAlwaysPreferClosest _aiAlwaysPreferClosest;
        private FreeEnemyKiteMaxRange _freeEnemyKiteMaxRange;
        private CompsAndUnitsSO _compsAndUnits;
        [SerializeField] private UnitManager _unitManager;
        public UnitManager UnitManager {get {return _unitManager;}}

        public void Initialize(int _controllerIndex,UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _unitInWaveIndex, PlayerUnitsSO _ownUnits)
        {
            this._compsAndUnits = _compsAndUnits;
            _parentHealth = GetComponent<ParentHealth>();
            _unitManager?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex, null);
            _unitManager?.SetUnitName(_unitStats.UnitName);
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<ParentClickManager>().Initialize(_controllerIndex);
            GetComponent<ParentHealth>().CurrentUnit = _unitManager;
            _mover = GetComponent<Mover>();
            _mover.IsStopped = false;
            _mover?.SetSpeed(_compsAndUnits.Speed);
            _freeApproach = GetComponent<FreeEnemyApproach>();
            _freeApproach?.Initialize(_ownUnits, _unitManager.GetComponent<Range>());
            _aiAlwaysPreferClosest = _unitManager.GetComponent<AIAlwaysPreferClosest>();
            _aiAlwaysPreferClosest.Initialize();
            _freeEnemyKiteMaxRange = GetComponent<FreeEnemyKiteMaxRange>();
            _freeEnemyKiteMaxRange?.Initialize(_unitManager.GetComponent<TargetHolder>());
        }
        public void Tick (float _timeAmount)
        {   
            bool _isRetreating = false;
            bool _isStopped = false;
            _aiAlwaysPreferClosest.CheckIfTargetInRange();
            if (_freeEnemyKiteMaxRange != null)
            {
            _freeEnemyKiteMaxRange.Tick();            
            _isRetreating = _freeEnemyKiteMaxRange.GetRetreating();
            }
            _freeApproach?.Tick(_isRetreating);
            // if (_mover != null)
            // {
            //     // _mover?.Tick(_timeAmount);
            //     _isStopped = _mover.IsStopped;
            // }
        }
    }
}
