using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;
using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class UnitManagerAddHatchEffect : MonoBehaviour
    {
        private UnitManager _unitManager;
        private ChrysalisTimer _chrysalisTimer;
        private Range _range;
        private UnitManagerHEDamageAddRemove _unitManagerHEDamageAddRemove;

        private void Awake()
        {
            _range = GetComponent<Range>();
            _unitManager = GetComponent<UnitManager>();
            _unitManagerHEDamageAddRemove = GetComponent<UnitManagerHEDamageAddRemove>();
            _chrysalisTimer = GetComponent<ChrysalisTimer>();
        }
        public void AddHatchEffect(GameObject hatchEffect)
        {
            AddHatchEffectToUnitManagerList(hatchEffect);
            _unitManagerHEDamageAddRemove?.HEContainsDamageChangeDamage(hatchEffect);
            HEContainsRangeAddRange(hatchEffect);
            HEContainsBuildSpeedAddBuildSPeed(hatchEffect);
        }
        private void AddHatchEffectToUnitManagerList(GameObject hatchEffect)
        {
            if (_unitManager.CurrentHatchEffects.Contains(hatchEffect)) return;
            _unitManager.CurrentHatchEffects.Add(hatchEffect);
        }
        private void HEContainsRangeAddRange(GameObject hatchEffect)
        {
            HEIncreaseRange heIncreaseRange = hatchEffect.GetComponent<HEIncreaseRange>();
            if (heIncreaseRange == null) return;
            _range?.AddRangeHE(heIncreaseRange);
        }
        private void HEContainsBuildSpeedAddBuildSPeed(GameObject hatchEffect)
        {
            HEIncreaseBuildSpeed heIncreaseBuildSpeed = hatchEffect.GetComponent<HEIncreaseBuildSpeed>();
            if (heIncreaseBuildSpeed == null) return;
            Debug.Log($"he increase build speed exists");
            _chrysalisTimer?.AddHEIncreaseBuildSpeedAndChangeSpeed(heIncreaseBuildSpeed);
        }
        
    }
}
