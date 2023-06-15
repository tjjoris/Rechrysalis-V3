using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseDefence : HatchEffectFunctionParent
    {
        [SerializeField] private float _incomingDamageMultBase;
        [SerializeField] private float _incomingDamageMult;

        public override void Initialize(ControllerManager controllerManager, float hatchEffectMult)
        {
            base.Initialize(controllerManager, hatchEffectMult);
            _incomingDamageMult =  _incomingDamageMultBase * hatchEffectMult;
        }
        public float GetIncomingDamageMult()
        {
            return _incomingDamageMult;
        }
    }
}
