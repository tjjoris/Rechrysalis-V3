using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;

namespace Rechrysalis.AdvancedUpgrade
{
    public class BurstHealManager : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        [SerializeField] private float _healAmount;

        public void Initialize(ControllerManager controllerManager, float healAmount)
        {
            _controllerManager = controllerManager;
            _healAmount = healAmount;
        }
        // public void OnEnable()
        // {
            
        // }
        public void Activate()
        {
foreach (ParentHealth parentHealth in _controllerManager.ParentHealths)
            {
                if (parentHealth != null)
                {
                    parentHealth.Heal(_healAmount);
                }
            }
        }
    }
}
