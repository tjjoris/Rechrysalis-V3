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
        private bool _isChargingUp;
        private bool _isStopped;
        public bool IsStopped{set{_isStopped = value;}}
        [SerializeField] private TargetsListSO _targetsList;
        private InRangeByPriority _inRangeByPriority;  
        private ClosestTarget _closestTarget;  
        private TargetHolder _targetHolder;

        public void Initialize(UnitStatsSO _unitStats)
        {   
            this._unitStats = _unitStats;
            _attackChargeUp = _unitStats.AttackChargeUp;
            _attackWindDown = _unitStats.AttackWindDown;
            _baseDamage = _unitStats.BaseDamage;
            _projectilesPool = GetComponent<ProjectilesPool>();
            _inRangeByPriority = GetComponent<InRangeByPriority>();
            _closestTarget = GetComponent<ClosestTarget>();
            _targetHolder = GetComponent<TargetHolder>();
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
            else if (_isWindingDown)
            {
                _attackChargeCurrent += _timeAmount;
            }
            else
            {            
                GameObject _tempTarget = null;
                if ((_isChargingUp) && (_isStopped) && (_attackChargeCurrent >= _attackChargeUp))
                {
                    _tempTarget = GetTargetInRange();
                    if (_tempTarget != null)
                    {
                        GameObject _projectile = _projectilesPool?.GetPooledObject();
                        if (_projectile != null) 
                        {
                            _projectile.SetActive(true);
                            _projectile.transform.position = gameObject.transform.position;
                            _projectile.GetComponent<ProjectileHandler>()?.TurnOnProjectile(_targetHolder.Target, _unitStats.ProjectileSpeed);
                            _isWindingDown = true;                       
                        }
                    }
                    else 
                    {
                        ResetChargeUp();
                    }
                }
                else if ((!_isWindingDown) && (!_isChargingUp) && (_isStopped) && (GetTargetInRange() != null))
                {
                    _isChargingUp = true;
                    _attackChargeCurrent += _timeAmount;
                }
                else if ((_isChargingUp) && (_isStopped))
                {
                    _attackChargeCurrent += _timeAmount;                
                }            
            }
        }
        private GameObject GetTargetInRange()
        {
            GameObject _tempTarget = _inRangeByPriority?.CheckPriorityTargetInRange();
            if (_tempTarget == null)
            {
                _tempTarget = _targetHolder.Target;
            }
            if (_targetHolder.GetThisTargetInRange(_tempTarget))
            {
                return _tempTarget;
            }
            return null;
        }
        public void CheckToResetChargeUp()
        {
            if ((!_isWindingDown) && (_isChargingUp))
            {
                ResetChargeUp();
            }
        }
        private void ResetChargeUp()
        {
            _attackChargeCurrent = 0;
            _isChargingUp = false;
        }
        public void SetDamage(float _damage)
        {
            _baseDamage = _damage;
        }
        public float getDamage()
        {
            return _baseDamage;
        }
    }
}
