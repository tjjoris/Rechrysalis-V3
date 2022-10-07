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
        // public GameObject TargetUnit {set{_targetUnit = value;} get {return _targetUnit;}}
        [SerializeField] private float _speed;
        public float Speed {set{_speed = value;}get{return _speed;}}
        private float _minDistToDisable = 0.5f;

    public void SetTarget (GameObject _targetUnit)
    {
        this._targetUnit = _targetUnit;
    }
    public void Tick (float _timeAmount)
    {
        Vector3 _direction = Vector3.MoveTowards(gameObject.transform.position, _targetUnit.transform.position, _speed);
        _direction.z = 0;
        gameObject.transform.Translate(_direction);
        if ((_targetUnit.transform.position - gameObject.transform.position).magnitude <= _minDistToDisable)
        {
            gameObject.SetActive(false);
        }
    }
    }
}
