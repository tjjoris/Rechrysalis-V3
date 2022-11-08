using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;
// using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class ParentFreeEnemyManager : MonoBehaviour
    {
        private ParentHealth _parentHealth;
        private FreeEnemyApproach _freeApproach;
        private Mover _mover;
        private CompsAndUnitsSO _compsAndUnits;
        [SerializeField] private UnitManager _unitManager;
        public UnitManager UnitManager {get {return _unitManager;}}

        public void Initialize(int _controllerIndex,UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _unitInWaveIndex, PlayerUnitsSO _ownUnits)
        {
            this._compsAndUnits = _compsAndUnits;
            _parentHealth = GetComponent<ParentHealth>();
            _unitManager?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex);
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
        }
        public void Tick (float _timeAmount)
        {
            _freeApproach?.Tick();
            _mover?.Tick(_timeAmount);
        }
    }
}
