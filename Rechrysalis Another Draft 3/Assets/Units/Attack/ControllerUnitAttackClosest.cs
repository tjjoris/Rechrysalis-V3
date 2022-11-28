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
        private InRangeByPriority _inRangeByPriority;

        public void Initialzie()
        {
            _targetHolder = GetComponent<TargetHolder>();
            _range = GetComponent<Range>();
            _closestTarget = GetComponent<ClosestTarget>();
            _inRangeByPriority = GetComponent<InRangeByPriority>();
        }
        
        public void CheckToGetTarget()
        {
            GameObject _tempTarget = _inRangeByPriority.CheckPriorityTargetInRange();
            if (_tempTarget != null)
            {
                _targetHolder.Target = _tempTarget;
                return;
            }
            if (!_targetHolder.IsTargetInRange())
            {
                _closestTarget.GetNearestEnemyInRange();
            }
        }
    }
}
