using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseBuildSpeed : HatchEffectFunctionParent
    {
        [SerializeField] private float _increaseBuildSpeed;

        public float GetIncreaseBuildSpeed()
        {
            return _increaseBuildSpeed;
        }
    }
}
