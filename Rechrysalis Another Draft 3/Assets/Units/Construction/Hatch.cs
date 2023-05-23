using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    public class Hatch : MonoBehaviour
    {
        private bool _debugBool = false;
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
            _parentUnitHatchEffects = parentUnitManager.GetComponent<ParentUnitHatchEffects>();
        }
        public void UnitActivateHatch()
        {
            if (_unitManager.UnitClass.HatchEffectClasses[0].HatchEffectManager.IsActivatedOnUnit) return;
            ActivateHatch();
        }

        private void ActivateHatch()
        {
            Rechrysalis.UI.DebugTextStatic.DebugText.DisplayText("in unit hatch script, activate hatch");
            if (_debugBool) Debug.Log($"activate hatch in  hatch");
            _unitManager.MoveSpeedAddManager?.Activate();
            _unitManager.BurstHealManager?.Activate();
            _unitManager.HatchAdjustBuildTimerMaxBase?.Activate();
            ActivateHatchEffects(_unitManager.ChildUnitIndex);
            Instantiate(_unitManager.ParticleEffectPrefab, transform.position, Quaternion.identity, transform.parent);
        }

        public void ChrysalisActivateHatch()
        {
            if (!_unitManager.UnitClass.HatchEffectClasses[0].HatchEffectManager.IsActivatedOnUnit) return;
            ActivateHatch();
        }
        private void ActivateHatchEffects(int unitIndex)
        {
            UI.DebugTextStatic.DebugText.DisplayText("activate hatch effects func in hatch.");
            if (_parentUnitHatchEffects == null) UI.DebugTextStatic.DebugText.DisplayText("parent unit hatch effects == null");
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
                Debug.Log($"hem " + hatcheffectClass.HatchEffectManager + " hem is activated on unit " + hatcheffectClass.HatchEffectManager.IsActivatedOnUnit);
                if (hatcheffectClass.HatchEffectManager.IsActivatedOnUnit) {continue;}
                if (hatcheffectClass.HatchEffectPrefab.GetComponent<HatchEffectHealth>() == null) 
                {
                    UI.DebugTextStatic.DebugText.DisplayText("he health == null");
                    continue;                
                }
                if (hatcheffectClass.HatchEffectPrefab == null) 
                {
                    UI.DebugTextStatic.DebugText.DisplayText("hatch effect prefab == null");
                    continue;
                }
                UI.DebugTextStatic.DebugText.DisplayText("in activateHatchEffects func, about to call create hatch");                
                _parentUnitHatchEffects.CreateHatchEffect(hatcheffectClass.HatchEffectPrefab, _parentUnitManager.ParentIndex, unitIndex, true);
            }

        }
    }
}
