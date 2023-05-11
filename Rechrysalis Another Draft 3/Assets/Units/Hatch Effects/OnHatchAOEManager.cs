using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public class OnHatchAOEManager : MonoBehaviour
    {
        [SerializeField] private HatchEffectManager _hatchEffectManager;
        [SerializeField] private ControllerManager _controllerManager;
        [SerializeField] private float _damage;
        [SerializeField] private float _tickRate;
        [SerializeField] private float _tickCurrent;

        
        private void Awake()
        {
            _hatchEffectManager = GetComponent<HatchEffectManager>();
        }
        public void Initialize(ControllerManager controllerManager)
        {
            _controllerManager = controllerManager;
        }
        public void Tick(float timeAmount)
        {
            _tickCurrent += timeAmount;
            if (_tickCurrent >= _tickRate)
            {
                _tickCurrent -= _tickRate;
                DealDamage();
            }
        }
        private void DealDamage()
        {
            Debug.Log($"HE aoe damage");
        }
    }
}
