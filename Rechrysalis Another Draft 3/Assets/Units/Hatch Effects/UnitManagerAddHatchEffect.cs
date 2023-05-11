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
        private Range _range;
        private UnitManagerHEDamageAddRemove _unitManagerHEDamageAddRemove;

        private void Awake()
        {
            _range = GetComponent<Range>();
            _unitManager = GetComponent<UnitManager>();
            _unitManagerHEDamageAddRemove = GetComponent<UnitManagerHEDamageAddRemove>();
        }
        public void AddHatchEffect(GameObject hatchEffect)
        {
            AddHatchEffectToUnitManagerList(hatchEffect);
            _unitManagerHEDamageAddRemove?.HEContainsDamageChangeDamage(hatchEffect);
            HEContainsRangeAddRange(hatchEffect);
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
        
    }
}
