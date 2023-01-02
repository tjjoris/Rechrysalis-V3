using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseDamage : MonoBehaviour
    {
        [SerializeField]private float _damageToAddBase = 20f;
        private float _damageToAdd;

        public void Initialize(float hatchEffectMult)
        {
            _damageToAdd = _damageToAddBase * hatchEffectMult;
        }

        public float GetDamageToAdd()
        {
            return _damageToAdd;
        }
    }
}
