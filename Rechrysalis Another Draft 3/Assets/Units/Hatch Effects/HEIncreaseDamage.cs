using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseDamage : HatchEffectFunctionParent
    {
        [SerializeField]private float _damageToAddBase = 20f;
        private float _damageToAdd;

        public override void Initialize(ControllerManager controllerManager, float hatchEffectMult)
        {
            base.Initialize(controllerManager, hatchEffectMult);
            _damageToAdd = _damageToAddBase * hatchEffectMult;
        }

        public float GetDamageToAdd()
        {
            return _damageToAdd;
        }
    }
}
