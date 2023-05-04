using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectsStoredOnSubUnit : MonoBehaviour
    {
        [SerializeField] private List<HatchEffectSO> _hatchEffectsStored;
        public List<HatchEffectSO> HatchEffectsStored => _hatchEffectsStored;

        public void SetHatchEffectsStored(UnitClass unitClass)
        {
            // foreach (HatchEffectSO hatchEffect in unitClass.)
            {

            }
        }

    }
}
