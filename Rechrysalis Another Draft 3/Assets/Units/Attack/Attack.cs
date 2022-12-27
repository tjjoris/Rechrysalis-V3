using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Attacking
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private UnitClass _unitClass;
        private UnitStatsSO _unitStats;
        [SerializeField] private float _attackChargeCurrent;
        [SerializeField] private float _attackChargeUp;
        [SerializeField] private float _attackWindDown;
        [SerializeField] private  float _baseDamage;
        [SerializeField] private float _damage;
        [SerializeField] private float _baseDPS;
        private ProjectilesPool _projectilesPool;
        private bool _isWindingDown;
        private bool _isChargingUp;
        private bool _isStopped;
        public bool IsStopped{set{_isStopped = value;}}
        [SerializeField] private TargetsListSO _targetsList;
        private InRangeByPriority _inRangeByPriority;  
        private ClosestTarget _closestTarget;  
        private TargetHolder _targetHolder;
        private AIAttackChargeUpTimer _aiAttackTimer;

        public void Initialize(UnitClass unitClass)
        {
            _unitClass = unitClass;
            _baseDPS = _unitClass.DPS;
            _attackChargeUp = _unitClass.AttackChargeUp;
            _attackWindDown = _unitClass.AttackWindDown;
            _baseDamage = _unitClass.Damamge;
            _projectilesPool = GetComponent<ProjectilesPool>();
            _inRangeByPriority = GetComponent<InRangeByPriority>();
            _closestTarget = GetComponent<ClosestTarget>();
            _targetHolder = GetComponent<TargetHolder>();
            _aiAttackTimer = GetComponent<AIAttackChargeUpTimer>();
            _aiAttackTimer?.Initialize(_attackChargeUp, _attackWindDown);
            ResetUnitAttack();
        }
        public void InitializeOld(UnitStatsSO _unitStats)
        {   
            this._unitStats = _unitStats;
            _attackChargeUp = _unitStats.AttackChargeUpBasic;
            _attackWindDown = _unitStats.AttackWindDownBasic;
            _baseDamage = _unitStats.BaseDamageBasic;
            _projectilesPool = GetComponent<ProjectilesPool>();
            _inRangeByPriority = GetComponent<InRangeByPriority>();
            _closestTarget = GetComponent<ClosestTarget>();
            _targetHolder = GetComponent<TargetHolder>();
            _aiAttackTimer = GetComponent<AIAttackChargeUpTimer>();
            _aiAttackTimer?.Initialize(_attackChargeUp, _attackWindDown);
            ResetUnitAttack();
        }
        public void ResetUnitAttack()
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
                            _projectile.GetComponent<ProjectileHandler>()?.TurnOnProjectile(_targetHolder.Target);
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
                _aiAttackTimer?.Tick(_timeAmount, _isChargingUp, _isWindingDown);       
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
        public void ReCalculateDamage(List<HEIncreaseDamage> hatchEffects)
        {
            float dps = _baseDPS;
            foreach (HEIncreaseDamage hEIncreaseDamage in hatchEffects)
            {
                // HEIncreaseDamage hEIncreaseDamage = hatchEffect.GetComponent<HEIncreaseDamage>();                
                if (hEIncreaseDamage != null)
                {
                    dps += hEIncreaseDamage.GetDamageToAdd();
                }
            }
            _damage = (dps / (_attackChargeUp + _attackWindDown));
        }
        public float getDamage(List<GameObject> hatchEffects)
        {
            return _damage;
        }
    }
}
