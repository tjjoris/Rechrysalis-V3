using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class TargetHolder : MonoBehaviour
    {
        private GameObject _target;
        public GameObject Target {set {_target = value;} get {return _target;}}
        private Range _range;
        
        private void Start() {
            _range = GetComponent<Range>();            
        }

        public bool IsTargetInRange()
        {
            if ((_target != null) && (Mathf.Abs((_target.transform.position - transform.position).magnitude) >= _range.GetRange()))
            {
                return true;
            }
            return false;
        }

    }
}
