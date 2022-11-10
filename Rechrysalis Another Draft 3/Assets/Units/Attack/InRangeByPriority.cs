using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class InRangeByPriority : MonoBehaviour
    {
        [SerializeField] private TargetsListSO _targetsList;
        private TargetHolder _targetHolder;
        private Range _range;

        public void Initialize(TargetsListSO _targetsList)
        {
            this._targetsList = _targetsList;
            _range = GetComponent<Range>();
            _targetHolder = GetComponent<TargetHolder>();
        }
        public void CheckPriorityTargetInRange()
        {
            for (int _target = _targetsList.Targets.Count - 1; _target >= 0; _target--)
            {
                if (_targetsList.Targets[_target] != null)
                {
                    if ((_targetsList.Targets[_target].transform.position - gameObject.transform.position).magnitude <= _range.GetRange())
                    {
                        _targetHolder.Target = _targetsList.Targets[_target];
                    }
                }
            }
            // return null;
        }
    }
}
