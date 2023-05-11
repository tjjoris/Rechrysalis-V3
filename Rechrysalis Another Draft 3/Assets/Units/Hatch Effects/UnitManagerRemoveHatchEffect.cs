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
        private UnitManagerHEDamageAddRemove _unitManagerHEDamageAddRemove;
        private void Awake()
        {
            _unitManager = GetComponent<UnitManager>();
            _range = GetComponent<Range>();
            _unitManagerHEDamageAddRemove = GetComponent<UnitManagerHEDamageAddRemove>();
        }
        public void RemoveHatchEffect(GameObject hatchEffect)
        {
            RemoveHEFromUnitManagerList(hatchEffect);
            _unitManagerHEDamageAddRemove?.HEContainsDamageChangeDamage(hatchEffect);
            HEContainsRangeRemoveRange(hatchEffect);
        }
        private void RemoveHEFromUnitManagerList(GameObject hatchEffect)
        {
            if (!_unitManager.CurrentHatchEffects.Contains(hatchEffect)) return;
            _unitManager.CurrentHatchEffects.Remove(hatchEffect);
        }
        private void HEContainsRangeRemoveRange(GameObject hatchEffect)
        {

            HEIncreaseRange heIncreaseRange = hatchEffect.GetComponent<HEIncreaseRange>();
            if (heIncreaseRange == null) return;
            _range?.RemoveRangeHE(heIncreaseRange);
        }
    }
}
