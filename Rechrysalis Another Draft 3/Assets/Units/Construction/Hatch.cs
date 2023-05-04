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
            _unitManager.MoveSpeedAddManager?.Activate();
            _unitManager.BurstHealManager?.Activate();
            _unitManager.HatchAdjustBuildTimerMaxBase?.Activate();
            ActivateHatchEffects(_unitManager.FreeUnitIndex);
            Instantiate(_unitManager.ParticleEffectPrefab, transform.position, Quaternion.identity, transform.parent);            
        }
        private void ActivateHatchEffects(int unitIndex)
        {
            if (_debugBool) Debug.Log($"activate hatch effects in hatch called");
            // if (_unitManager.UnitIndex == 1)
            {
                if (_parentUnitManager.ParentUnitClass.AdvUnitClass.HatchEffectPrefab.Count > 0)
                {
                    foreach (HatchEffectClass hatchEffectClass in _parentUnitManager.ParentUnitClass.AdvUnitClass.HatchEffectClasses)
                    {
                        if (hatchEffectClass != null)
                        {
                            _parentUnitHatchEffects.CreateHatchEffect(hatchEffectClass.HatchEffectPrefab, _parentUnitManager.ParentIndex, unitIndex, true, hatchEffectClass.HatchEffectHealth);
                        }
                    }
                }
            }
            // _progressBarManager?.TintChargeUp();
        }
    }
}
