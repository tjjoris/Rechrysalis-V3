using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class ControllerUnitAttackClosest : MonoBehaviour
    {
        private TargetHolder _targetHolder;
        private Range _range;
        private ClosestTarget _closestTarget;

        public void Initialzie()
        {
            _targetHolder = GetComponent<TargetHolder>();
            _range = GetComponent<Range>();
            _closestTarget = GetComponent<ClosestTarget>();
        }
        
        public void CheckToGetTarget()
        {
            
        }
    }
}
