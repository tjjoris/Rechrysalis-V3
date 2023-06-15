using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public abstract class HatchEffectFunctionParent : MonoBehaviour
    {
        protected ControllerManager _controllerManager;
        protected float _hatchMult;
        public virtual void Initialize(ControllerManager controllerManager, float hatchMult)
        {
            _controllerManager = controllerManager;
            _hatchMult = hatchMult;
        }
        public virtual void Tick(float tickAmount)
        {

        }
        public virtual void Die()
        {
            
        }
    }
}
