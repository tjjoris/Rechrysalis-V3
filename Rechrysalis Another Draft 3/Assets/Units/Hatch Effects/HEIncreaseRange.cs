using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseRange : HatchEffectFunctionParent
    {
        [SerializeField] private float _rangeToAdd;

        public float GetRangeToAdd()
        {
            return _rangeToAdd;
        }
    }
}
