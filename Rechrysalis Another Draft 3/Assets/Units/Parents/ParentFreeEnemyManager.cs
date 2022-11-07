using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Movement;

namespace Rechrysalis.Unit
{
    public class ParentFreeEnemyManager : MonoBehaviour
    {
        private ParentHealth _parentHealth;
        [SerializeField] private UnitManager _unitManager;
        public UnitManager UnitManager {get {return _unitManager;}}

        public void Initialize(int _controllerIndex,UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _unitInWaveIndex)
        {
            _parentHealth = GetComponent<ParentHealth>();
            _unitManager?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex);
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<ParentClickManager>().Initialize(_controllerIndex);
        }
        public void Tick (float _timeAmount)
        {
            GetComponent<Mover>().Tick(_timeAmount);
        }
    }
}
