using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class AIAlwaysPreferClosest : MonoBehaviour
    {
        private TargetHolder _targetHolder;
        private Range _range;
        private ClosestTarget _closestTarget;
        public void Initialize()
        {
            _targetHolder = GetComponent<TargetHolder>();
            _range = GetComponent<Range>();
            _closestTarget = GetComponent<ClosestTarget>();
        }

        public void CheckIfTargetInRange()
        {
            if (!_targetHolder.IsTargetInRange())
            {
                // Debug.Log($"get closest");
                _closestTarget.GetNearestEnemy();
            }
        }
    }
}
