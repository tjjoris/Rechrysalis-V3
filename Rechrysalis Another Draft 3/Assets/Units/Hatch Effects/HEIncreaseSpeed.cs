using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseSpeed : HatchEffectFunctionParent
    {
        [SerializeField] private float _speedToAdd;
        [SerializeField] private HatchEffectManager _hatchEffectManager;
        [SerializeField] private UnitManager _unitManager;
        private void Awake()
        {
            _hatchEffectManager = GetComponent<HatchEffectManager>();
        }
        private void Start()
        {
            _unitManager = _hatchEffectManager.UnitManager;
        }
        public float GetSpeedToAdd()
        {
            return _speedToAdd;
        }
    }
}
