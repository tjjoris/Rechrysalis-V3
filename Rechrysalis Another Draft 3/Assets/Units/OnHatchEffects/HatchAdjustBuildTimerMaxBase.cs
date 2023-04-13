using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class HatchAdjustBuildTimerMaxBase : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        private float _buildTimeToAdd;
        [SerializeField] private float _currentTime;
        [SerializeField] private float _maxTime = 2;
        public void Initialize(ControllerManager controllerManager, float buildTimeToAdd)
        {
            _buildTimeToAdd = buildTimeToAdd;
            _controllerManager = controllerManager;
            AddBuildTimeMax(_buildTimeToAdd);
        }
        public void Tick(float timeAmount)
        {
            _currentTime += timeAmount;
            if (_currentTime >= _maxTime)
            {
                Destroy(this);
            }
        }
        private void OnDisable()
        {
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
