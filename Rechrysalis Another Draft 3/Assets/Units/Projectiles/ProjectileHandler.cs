using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class ProjectileHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _parentUnit;
        public GameObject ParentUnit {get{return _parentUnit;}set{_parentUnit = value;}}
        [SerializeField] private GameObject _targetUnit;
        public GameObject TargetUnit {set{_targetUnit = value;} get {return _targetUnit;}}
        [SerializeField] private float _speed;
        public float Speed {set{_speed = value;}get{return _speed;}}

    }
}
