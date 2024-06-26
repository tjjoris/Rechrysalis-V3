using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class InRangeByPriority : MonoBehaviour
    {
        [SerializeField] private TargetsListSO _targetsList;
        // private TargetHolder _targetHolder;
        private Range _range;

        private void Awake()
        {

            _range = GetComponent<Range>();

        }
        public void Initialize(TargetsListSO _targetsList)
        {
            this._targetsList = _targetsList;
            // _targetHolder = GetComponent<TargetHolder>();
        }
        public GameObject CheckPriorityTargetInRange()
        {
            // GameObject _targetToCheck = null;
            for (int _target = _targetsList.Targets.Count - 1; _target >= 0; _target--)
            {
                if (_targetsList.Targets[_target] != null)
                {
                    if ((_targetsList.Targets[_target].transform.position - gameObject.transform.position).magnitude <= _range.GetRange())
                    {
                        return _targetsList.Targets[_target];
                    }
                }
            }
            return null;
        }
    }
}
