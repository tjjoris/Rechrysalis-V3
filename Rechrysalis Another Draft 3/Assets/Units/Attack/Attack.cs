using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class Attack : MonoBehaviour
    {
        private UnitStatsSO _unitStats;
        [SerializeField] private float _attackChargeCurrent;
        [SerializeField] private float _attackChargeUp;
        [SerializeField] private float _attackWindDown;
        [SerializeField] private  float _baseDamage;
        private ProjectilesPool _projectilesPool;
        private bool _isWindingDown;
        private bool _isStopped;
        public bool IsStopped{set{_isStopped = value;}}
        [SerializeField] private TargetsListSO _targetsList;
        private InRangeByPriority _inRangeByPriority;  
        private ClosestTarget _closestTarget;  

        public void Initialize(UnitStatsSO _unitStats)
        {   
            this._unitStats = _unitStats;
            _attackChargeUp = _unitStats.AttackChargeUp;
            _attackWindDown = _unitStats.AttackWindDown;
            _baseDamage = _unitStats.BaseDamage;
            _projectilesPool = GetComponent<ProjectilesPool>();
            _inRangeByPriority = GetComponent<InRangeByPriority>();
            _closestTarget = GetComponent<ClosestTarget>();
            ResetUnit();
        }
        public void ResetUnit()
        {
            _attackChargeCurrent = 0;
            _isWindingDown = false;
        }
        public void Tick(float _timeAmount)
        {
            if (_attackChargeCurrent >= _attackWindDown + _attackChargeUp) 
            {
                _attackChargeCurrent = 0;
                _isWindingDown = false;
            }
            else if ((_isWindingDown) && (_attackChargeCurrent >= _attackChargeUp) && (_attackChargeCurrent < (_attackWindDown + _attackChargeUp)))
            {
                _attackChargeCurrent += _timeAmount;
            }
            else if ((_attackChargeCurrent >= _attackChargeUp) && (_isStopped) && (!_isWindingDown))
            {
                GameObject _targetUnit = _inRangeByPriority?.CheckPriorityTargetInRange();
                if (_targetUnit == null)
                {
                    _targetUnit = _closestTarget.GetNearestEnemyInRange();
                }
                if (_targetUnit != null)
                {
                    GameObject _projectile = _projectilesPool?.GetPooledObject();
                    if (_projectile != null) 
                    {
                        // Debug.Log($"shoot projectile");
                        _projectile.SetActive(true);
                        _projectile.transform.position = gameObject.transform.position;
                        // Debug.Log($"position " + _projectile.transform.position);
                        _projectile.GetComponent<ProjectileHandler>()?.TurnOnProjectile(_targetUnit, _unitStats.ProjectileSpeed);
                        _isWindingDown = true;                       
                    }
                }
            }
            else if ((_attackChargeCurrent < _attackChargeUp) && (_isStopped))
            {
                _attackChargeCurrent += _timeAmount;                
            }            
        }
        public float getDamage()
        {
            return _baseDamage;
        }
    }
}
