using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class ProjectileHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _parentUnit;
        public GameObject ParentUnit {get{return _parentUnit;}set{_parentUnit = value;}}
        [SerializeField] private GameObject _targetUnit;
        // public GameObject TargetUnit {set{_targetUnit = value;} get {return _targetUnit;}}
        [SerializeField] private float _speed;
        public float Speed {set{_speed = value;}get{return _speed;}}
        private float _minDistToDisable = 0.2f;

    public void Initialize(Sprite _sprite)
    {
        _spriteRenderer.sprite = _sprite;
    }
    public void TurnOnProjectile (GameObject _targetUnit, float _speed)
    {
        this._speed = _speed;
        this._targetUnit = _targetUnit;
    }
    public void Tick (float _timeAmount)
    {
        // Debug.Log($"speed " + _speed + " time " + Time.deltaTime + " fixed time " + Time.fixedDeltaTime);
        
        Vector3 _direction = Vector2.MoveTowards(gameObject.transform.position, _targetUnit.transform.position, _speed);
            // Debug.Log($"direction" + _direction);
        // _direction.z = 0;
        // gameObject.transform.Translate(_direction * _timeAmount);
        gameObject.transform.position = _direction;
        if ((_targetUnit.transform.position - gameObject.transform.position).magnitude <= _minDistToDisable)
        {
            gameObject.SetActive(false);
            // Health _targetHealth = _targetUnit.GetComponent<Health>();
            ParentHealth _targetHealth = _targetUnit.GetComponent<ParentHealth>();
            _targetHealth?.TakeDamage(_parentUnit.GetComponent<Attack>().getDamage());
        }
    }
    }
}
