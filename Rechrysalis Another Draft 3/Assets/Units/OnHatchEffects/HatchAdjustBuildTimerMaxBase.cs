using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class HatchAdjustBuildTimerMaxBase : MonoBehaviour
    {
        private bool _debugBool = true;
        private bool _buildTimeHasBeenAdjusted;
        private ControllerManager _controllerManager;
        private float _buildTimeToAdd;
        [SerializeField] private float _currentTime;
        [SerializeField] private float _maxTime = 4;
        public void Initialize(ControllerManager controllerManager, float buildTimeToAdd)
        {
            _buildTimeToAdd = buildTimeToAdd;
            _controllerManager = controllerManager;
        }
        public void Activate()
        {
            if (_debugBool) Debug.Log($"add negative build time");
            _currentTime = 0;
            _buildTimeHasBeenAdjusted = true;
            AddBuildTimeMax(_buildTimeToAdd);
        }
        public void Tick(float timeAmount)
        {
            if (!_buildTimeHasBeenAdjusted) return;
            _currentTime += timeAmount;
            if (_currentTime >= _maxTime)
            {
                RemoveBuildTimeAdjustment();
            }
        }
        private void OnDisable()
        {
            if (!_buildTimeHasBeenAdjusted) return;
            RemoveBuildTimeAdjustment();
        }
        private void RemoveBuildTimeAdjustment()
        {
            if (_debugBool) Debug.Log($"remove negative build time");
            _buildTimeHasBeenAdjusted = false;
            AddBuildTimeMax(-_buildTimeToAdd);
        }
        private void AddBuildTimeMax(float timeToAdd)
        {
            if (_controllerManager.ParentUnitManagers == null) return;
            LoopParentUnitManagersAndAddToBuildTimerMaxBase(timeToAdd);
         }
         private void LoopParentUnitManagersAndAddToBuildTimerMaxBase(float timeToAdd)
         {
            foreach (ParentUnitManager parentUnitManager in _controllerManager.ParentUnitManagers)
            {
                if (parentUnitManager == null) continue;
                LoopChrysalisManagersAndAddToBuildTimerMaxBase(parentUnitManager, timeToAdd);
            }
         }
         private void LoopChrysalisManagersAndAddToBuildTimerMaxBase(ParentUnitManager parentUnitManager, float timeToAdd)
         {
            foreach (UnitManager chrysaliiManager in parentUnitManager.ChildChrysaliiUnitManagers)
            {
                chrysaliiManager.ChryslaisTimer.AddTimerMaxBase(timeToAdd);
            }
         }
    }
}
