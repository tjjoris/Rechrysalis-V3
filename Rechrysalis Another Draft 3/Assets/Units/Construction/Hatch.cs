using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    public class Hatch : MonoBehaviour
    {
        private bool _debugBool = true;
        private ControllerManager _controllerManager;
        private ParentUnitManager _parentUnitManager;
        private UnitManager _unitManager;
        private ParentUnitHatchEffects _parentUnitHatchEffects;
        private void Awake()
        {
            _unitManager = GetComponent<UnitManager>();
        }
        public void Initialize(ControllerManager controllerManager, ParentUnitManager parentUnitManager)
        {
            _controllerManager = controllerManager;
            _parentUnitManager = parentUnitManager;
            _parentUnitHatchEffects = _parentUnitManager.GetComponent<ParentUnitHatchEffects>();
        }
        public void ActivateHatch()
        {
            Rechrysalis.UI.DebugTextStatic.DebugText.DisplayText("in unit hatch script, activate hatch");
            Debug.Log($"activate hatch in  hatch");
            _unitManager.MoveSpeedAddManager?.Activate();
            _unitManager.BurstHealManager?.Activate();
            _unitManager.HatchAdjustBuildTimerMaxBase?.Activate();
            ActivateHatchEffects(_unitManager.ChildUnitIndex);
            Instantiate(_unitManager.ParticleEffectPrefab, transform.position, Quaternion.identity, transform.parent);            
        }
        private void ActivateHatchEffects(int unitIndex)
        {
            UI.DebugTextStatic.DebugText.DisplayText("activate hatch effects func in hatch.");
            if (_debugBool) Debug.Log($"activate hatch effects in hatch called");         
            if (_unitManager.UnitClass.HatchEffectClasses.Count == 0) return;
            if (_unitManager == null) UI.DebugTextStatic.DebugText.DisplayText("unit manager == null");
            if (_unitManager.UnitClass == null) UI.DebugTextStatic.DebugText.DisplayText("unitmanager.Unit class == null");
            if (_unitManager.UnitClass.HatchEffectClasses == null) UI.DebugTextStatic.DebugText.DisplayText("unit manager.unit class.HEclasses == null");
            if (_unitManager.UnitClass.HatchEffectClasses.Count == 0) UI.DebugTextStatic.DebugText.DisplayText("no hatch effects stored on unit class in unit manager for unit");
            foreach (HatchEffectClass hatcheffectClass in _unitManager.UnitClass.HatchEffectClasses)
            {
                if (hatcheffectClass == null) 
                {
                    UI.DebugTextStatic.DebugText.DisplayText("hatcheffectclass == null");
                    continue;
                }
                if (hatcheffectClass.HatchEffectManager.HEHealth == null) continue;                
                _parentUnitHatchEffects.CreateHatchEffect(hatcheffectClass.HatchEffectPrefab, _parentUnitManager.ParentIndex, unitIndex, true);
            }

        }
    }
}
