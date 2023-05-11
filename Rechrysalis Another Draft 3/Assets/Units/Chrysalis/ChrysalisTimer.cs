using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.HatchEffect;

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
        [SerializeField] private float _timerSpeedBase = 1f;
        [SerializeField] private float _timerSpeedCurrent = 1f;
        public float TimerCurrent {set {_timerCurrent = value;} get {return _timerCurrent;}}
        private List<HEIncreaseBuildSpeed> _heIncreaseBuildSpeedList;
        private  int _nextUnitBuilding;
        private int _subUnitCount;
        [SerializeField] private ProgressBarManager _progressBarManager;
        public Action<int> _startUnit;

        private void Awake()
        {
            _chrysalisUnitManger = GetComponent<UnitManager>();
            _heIncreaseBuildSpeedList = new List<HEIncreaseBuildSpeed>();
        }
        public void Initialize (float timerMax, int _nextUnitBuilding, ProgressBarManager progressBarManager, ParentUnitManager parentUnitManager)
        {
            if (_progressBarManager == null)
            {
                _progressBarManager= progressBarManager;
            }
            _parentUnitManager = parentUnitManager;
            this._timerMaxBase = timerMax;
            _timerMax = _timerMaxBase;
            this._nextUnitBuilding = _nextUnitBuilding;
        }
        public void AddHEIncreaseBuildSpeedAndChangeSpeed(HEIncreaseBuildSpeed heIncreaseBuildSpeed)
        {
            AddHeIncreseBuildSpeed(heIncreaseBuildSpeed);
            SetCurrentBuildSpeedBasedOnHE();
        }
        public void RemoveHEIncreaseBuildSpeedAndChangeSpeed(HEIncreaseBuildSpeed heIncreaseBuildSpeed)
        {
            RemoveHEIncreaseBuildSpeed(heIncreaseBuildSpeed);
            SetCurrentBuildSpeedBasedOnHE();
        }
        private void AddHeIncreseBuildSpeed(HEIncreaseBuildSpeed heIncreaseBuildSpeed)
        {
            if (_heIncreaseBuildSpeedList.Contains(heIncreaseBuildSpeed)) return;
            _heIncreaseBuildSpeedList.Add(heIncreaseBuildSpeed);
        }
        private void RemoveHEIncreaseBuildSpeed(HEIncreaseBuildSpeed heIncreaseBuildSpeed)
        {
            if (!_heIncreaseBuildSpeedList.Contains(heIncreaseBuildSpeed)) return;
            _heIncreaseBuildSpeedList.Remove(heIncreaseBuildSpeed);
        }
        private void SetCurrentBuildSpeedBasedOnHE()
        {
            _timerCurrent = _timerSpeedBase;
            foreach (HEIncreaseBuildSpeed heIncreaseBuildSpeed in _heIncreaseBuildSpeedList)
            {
                if (heIncreaseBuildSpeed == null) continue;
                _timerCurrent += heIncreaseBuildSpeed.GetIncreaseBuildSpeed();
            }
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
            _timerCurrent += (_timeAmount * _timerSpeedCurrent);
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
