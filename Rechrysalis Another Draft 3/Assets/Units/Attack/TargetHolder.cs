using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class TargetHolder : MonoBehaviour
    {
        // [SerializeField] private GameObject _focusTarget;
        // public GameObject FocusTarget { get => _focusTarget; set => _focusTarget = value; }
        
        [SerializeField] private GameObject _target;
        public GameObject Target {set {_target = value;} get {return _target;}}
        private Range _range;
        
        private void Awake()
        {
            _range = GetComponent<Range>();
        }

        public bool IsTargetInRange()
        {
            // if ((_target != null) && (_target.activeInHierarchy) && (Mathf.Abs((_target.transform.position - transform.position).magnitude) <= _range.GetRange()))
            // {
            //     return true;
            // }
            // return false;
            return GetThisTargetInRange(_target);
        }
        public bool IsTargetInRangePlusValue(float value)
        {
            if ((_target != null) && (_target.activeInHierarchy) && (Mathf.Abs((_target.transform.position - transform.position).magnitude) + value) <= _range.GetRange())
            {
                return true;
            }
            return false;
        }
        public bool IsTargetMinusChargeDistMinusDistInRange(float extraAmount)
        {            
            if ((_target != null) && (_target.activeInHierarchy) && (((Mathf.Abs((_target.transform.position - transform.position).magnitude) + _range.GetRangeDistToAccountForMovement() + extraAmount) <= _range.GetRange())))
            {
                return true;
            }
            return false;
        }
        public bool GetThisTargetInRange(GameObject _thisTarget)
        {
            if ((_thisTarget != null) && (_thisTarget.activeInHierarchy) && (Mathf.Abs((_thisTarget.transform.position - transform.position).magnitude) <= _range.GetRange()))
            {
                return true;
            }
            return false;
        }

    }
}
