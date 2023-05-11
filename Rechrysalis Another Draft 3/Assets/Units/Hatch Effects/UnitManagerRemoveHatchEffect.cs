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
        public void RemoveHatchEffect(GameObject _hatchEffect)
        {
            if (_unitManager.CurrentHatchEffects.Contains(_hatchEffect))
            {
                _unitManager.CurrentHatchEffects.Remove(_hatchEffect);
            }
            // ReCalculateStatChanges();
            if (_hatchEffect.GetComponent<HEIncreaseDamage>() != null)
            {
                _unitManager.ReCalculateDamageChanges();
            }
            HEIncreaseRange heIncreaseRange = _hatchEffect.GetComponent<HEIncreaseRange>();
            if (heIncreaseRange != null)
            {
                _range?.RemoveRangeHE(heIncreaseRange);
            }
        }
    }
}
