using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Attacking
{
    public class PriorityScoreHatchEffect : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private ParentUnitHatchEffects _parentUnitHatchEffects;

        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            _parentUnitManager = GetComponent<ParentUnitManager>();
            _parentUnitHatchEffects = GetComponent<ParentUnitHatchEffects>();

        }
        public float GenerateScore()
        {
            if ((_parentUnitHatchEffects != null) && (_parentUnitHatchEffects.HatchEffects.Count > 0))
            {
                GameObject hatchEffect = _parentUnitHatchEffects.HatchEffects[0];
                if (hatchEffect != null)
                {
                    HatchEffectManager hatchEffectManager = hatchEffect.GetComponent<HatchEffectManager>();
                    if (hatchEffectManager != null)
                    {
                        return hatchEffectManager.HatchMult * 15f;
                    }
                }
            }
            return 0;
        }
    }
}
