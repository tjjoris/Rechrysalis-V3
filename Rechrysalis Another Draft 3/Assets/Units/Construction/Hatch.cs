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
        public void Initialize(ControllerManager controllerManager, ParentUnitManager parentUnitManager)
        {
            _controllerManager = controllerManager;
            _parentUnitManager = parentUnitManager;
            _unitManager = GetComponent<UnitManager>();
            _parentUnitHatchEffects = _parentUnitManager.ParentUnitHatchEffects;
        }
        public void ActivateHatch()
        {
            Debug.Log($"activate hatch in  hatch");
            _unitManager.MoveSpeedAddManager?.Activate();
            _unitManager.BurstHealManager?.Activate();
            _unitManager.HatchAdjustBuildTimerMaxBase?.Activate();
            ActivateHatchEffects(_unitManager.ChildUnitIndex);
            Instantiate(_unitManager.ParticleEffectPrefab, transform.position, Quaternion.identity, transform.parent);            
        }
        private void ActivateHatchEffects(int unitIndex)
        {
            if (_debugBool) Debug.Log($"activate hatch effects in hatch called");         
            if (_unitManager.UnitClass.HatchEffectClasses.Count == 0) return;
            foreach (HatchEffectClass hatcheffectClass in _unitManager.UnitClass.HatchEffectClasses)
            {
                if (hatcheffectClass == null) continue;
                if (hatcheffectClass.HatchEffectManager.HEHealth == null) continue;                
                _parentUnitHatchEffects.CreateHatchEffect(hatcheffectClass.HatchEffectPrefab, _parentUnitManager.ParentIndex, unitIndex, true);
            }

        }
    }
}
