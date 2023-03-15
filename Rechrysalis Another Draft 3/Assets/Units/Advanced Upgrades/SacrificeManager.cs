using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.AdvancedUpgrade
{
    public class SacrificeManager : MonoBehaviour
    {
        private ControllerHealth _controllerHealth;
        [SerializeField] private float _sacrificeAmount;

        public void Initialize(ControllerHealth controllerHealth, float sacrificeAmount)
        {
            _controllerHealth = controllerHealth;
            _sacrificeAmount = sacrificeAmount;
        }
        private void OnEnable()
        {
            _controllerHealth.TakeDamage(_sacrificeAmount);
        }
    }
}
