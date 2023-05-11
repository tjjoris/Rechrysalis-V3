using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;
using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class UnitManagerRemoveHatchEffect : MonoBehaviour
    {
        private UnitManager _unitManager;
        private Range _range;
        private void Awake()
        {
            _unitManager = GetComponent<UnitManager>();
            _range = GetComponent<Range>();
        }
        public void RemoveHatchEffect(GameObject hatchEffect)
        {
            RemoveHEFromUnitManagerList(hatchEffect);
            HEContainsDamageChangeDamage(hatchEffect);
            HEContainsRangeRemoveRange(hatchEffect);
        }
        private void RemoveHEFromUnitManagerList(GameObject hatchEffect)
        {
            if (!_unitManager.CurrentHatchEffects.Contains(hatchEffect)) return;
            _unitManager.CurrentHatchEffects.Remove(hatchEffect);
        }
        private void HEContainsDamageChangeDamage(GameObject hatchEffect)
        {
            if (hatchEffect.GetComponent<HEIncreaseDamage>() == null) return;
            _unitManager.ReCalculateDamageChanges();
        }
        private void HEContainsRangeRemoveRange(GameObject hatchEffect)
        {

            HEIncreaseRange heIncreaseRange = hatchEffect.GetComponent<HEIncreaseRange>();
            if (heIncreaseRange == null) return;
            _range?.RemoveRangeHE(heIncreaseRange);
        }
    }
}
