using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class Hatch : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        private ParentUnitManager _parentUnitManager;
        private UnitManager _unitManager;
        public void Initialize(ControllerManager controllerManager, ParentUnitManager parentUnitManager)
        {
            _controllerManager = controllerManager;
            _parentUnitManager = parentUnitManager;
            _unitManager = GetComponent<UnitManager>();
        }
        public void ActivateHatch()
        {
            _unitManager.MoveSpeedAddManager?.Activate();
            _unitManager.BurstHealManager?.Activate();
            _unitManager.HatchAdjustBuildTimerMaxBase?.Activate();
            Instantiate(_unitManager.ParticleEffectPrefab, transform.position, Quaternion.identity, transform.parent);
        }
    }
}
