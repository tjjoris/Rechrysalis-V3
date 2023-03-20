using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.AdvancedUpgrade
{
    public class BurstHealManager : MonoBehaviour
    {
        private ControllerManager _controllerManager;

        public void Initialize(ControllerManager controllerManager)
        {
            _controllerManager = controllerManager;
        }
        public void OnEnable()
        {
            
        }
    }
}
