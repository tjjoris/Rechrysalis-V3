using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.Unit
{
    public class ChrysalisTimer : MonoBehaviour
    {
        private float _timerMax;
        private float _timerCurrent;
        public float TimerCurrent {set {_timerCurrent = value;} get {return _timerCurrent;}}
        private  int _nextUnitBuilding;
        private int _subUnitCount;
        private ProgressBarManager _progressBarManager;
        public Action<int> _startUnit;

        public void Initialize (float _timerMax, int _nextUnitBuilding, ProgressBarManager progressBarManager)
        {
            _progressBarManager= progressBarManager;
            this._timerMax = _timerMax;
            this._nextUnitBuilding = _nextUnitBuilding;
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
            float timePercent = _timerCurrent / _timerMax;
            _progressBarManager.StrechFillByValue(timePercent);
        }
    }
}
