using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.Unit
{
    public class ChrysalisTimer : MonoBehaviour
    {
        private bool _debugBool = false;
        private UnitManager _chrysalisUnitManger;
        private ParentUnitManager _parentUnitManager;
        [SerializeField] private float _timerMaxBase;
        [SerializeField] private float _timerMax;
        [SerializeField] private float _timerCurrent;
        public float TimerCurrent {set {_timerCurrent = value;} get {return _timerCurrent;}}
        private  int _nextUnitBuilding;
        private int _subUnitCount;
        [SerializeField] private ProgressBarManager _progressBarManager;
        public Action<int> _startUnit;

        public void Initialize (float timerMax, int _nextUnitBuilding, ProgressBarManager progressBarManager)
        {
            if (_progressBarManager == null)
            {
                _progressBarManager= progressBarManager;
            }
            _chrysalisUnitManger = GetComponent<UnitManager>();
            _parentUnitManager = _chrysalisUnitManger.ParentUnitManager;
            this._timerMaxBase = timerMax;
            _timerMax = _timerMaxBase;
            this._nextUnitBuilding = _nextUnitBuilding;
        }
        public void ApplyTimerMaxMult(float mult)
        {
            _timerMax = _timerMaxBase * mult;
        }
        public void AddTimerMaxBase(float amount)
        {
            _timerMaxBase += amount;
        }
        public void StartThisChrysalis(float _timeToKeep)
        {
            _timerCurrent = 0 + _timeToKeep;     
            CalculateProgressAndStrech();       
        }
        public void Tick (float _timeAmount)
        {
            _timerCurrent += _timeAmount;
            if (_timerCurrent >= _timerMax)
            {
                _startUnit?.Invoke(_nextUnitBuilding);                
                if (_debugBool) Debug.Log($"hatch {_nextUnitBuilding}");
                _parentUnitManager.ChildUnitManagers[_nextUnitBuilding].Hatch?.ActivateHatch();
            }
            CalculateProgressAndStrech();
        }
        public void SetNextUnit(int _nextUnitBuilding)
        {
            if (_nextUnitBuilding <= _subUnitCount)
            {
                this._nextUnitBuilding = _nextUnitBuilding;
            }
        }
        private void CalculateProgressAndStrech()
        {
            float timePercent = GetProgressRatio();
            _progressBarManager.StrechFillByValue(timePercent);
        }
        private float GetProgressRatio()
        {
            if (_timerCurrent <= 0)
            {
                return 0;
            }
            if (_timerMax <= 0)
            {
                return 1;                
            }
            return _timerCurrent / _timerMax;
        }
    }
}
