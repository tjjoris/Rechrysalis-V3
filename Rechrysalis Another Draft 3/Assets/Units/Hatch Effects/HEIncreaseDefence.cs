using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseDefence : MonoBehaviour
    {
        [SerializeField] private float _incomingDamageMultBase;
        [SerializeField] private float _incomingDamageMult;

        public void Initialize(float hatchEffectMult)
        {
            _incomingDamageMult = _incomingDamageMultBase / hatchEffectMult;
        }
        public float GetIncomingDamageMult()
        {
            return _incomingDamageMult;
        }
    }
}
