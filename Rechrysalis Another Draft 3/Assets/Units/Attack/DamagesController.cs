using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.Attacking
{
    public class DamagesController : MonoBehaviour
    {
        public Action<float> _damagesControllerAction;
        public void DamagesControllerFunction(float _damageAmount)
        {
            _damagesControllerAction.Invoke(_damageAmount);
        }
    }
}
