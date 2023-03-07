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
        [SerializeField] private float _currentDPS;
        public float CurrentDPS => _currentDPS;
        private ProjectilesPool _projectilesPool;
        private bool _isWindingDown;
        private bool _isChargingUp;
        // private bool _isStopped;
        // public bool IsStopped{set{_isStopped = value;}}
        [SerializeField] private TargetsListSO _targetsList;
        private InRangeByPriority _inRangeByPriority;  
        private ClosestTarget _closestTarget;  
        private TargetHolder _targetHolder;
        private AIAttackChargeUpTimer _aiAttackTimer;
        private ParentUnitManager _parentUnitManager;
        private ProgressBarManager _progressBarManager;


        public void Initialize(UnitClass unitClass, ParentUnitManager parentUnitManager)
        {
            // _parentUnitManager = transform.parent.GetComponent<ParentUnitManager>();
            _parentUnitManager = parentUnitManager;
            _progressBarManager = _parentUnitManager.GetComponent<ProgressBarManager>();
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
            _aiAttackTimer?.Initialize(_attackChargeUp, _attackWindDown, _parentUnitManager);
            CalculateDamage(_baseDPS);
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
            _aiAttackTimer?.Initialize(_attackChargeUp, _attackWindDown, _parentUnitManager);
            ResetUnitAttack();
        }
        public void ResetUnitAttack()
        {
            _attackChargeCurrent = 0;
            _isWindingDown = false;
        }
        public void Tick(float _timeAmount)
        {
            if ((!_isChargingUp) && (_attackChargeCurrent >= _attackWindDown + _attackChargeUp)) 
            {
                _attackChargeCurrent = 0;
                _isWindingDown = false;
            }
            else if ((_isWindingDown))
            {
                _attackChargeCurrent += _timeAmount;
            }
            else
            {            
                GameObject _tempTarget = null;
                if ((_isChargingUp) && (_parentUnitManager.IsStopped) && (_attackChargeCurrent >= _attackChargeUp))
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
                            _isChargingUp = false;   
                            _progressBarManager?.TintWindDown();             
                        }
                    }
                    // else 
                    // {
                    //     // ResetChargeUp();
                    // }
                }
                else if ((!_isWindingDown) && (!_isChargingUp))
                {
                    _isChargingUp = true;
                    _attackChargeCurrent += _timeAmount;
                    _progressBarManager?.TintChargeUp();
                }
                else if ((_isChargingUp) && (_parentUnitManager.IsStopped))
                {
                    _attackChargeCurrent += _timeAmount;                
                }
            }
            _aiAttackTimer?.Tick(_timeAmount, _isChargingUp, _isWindingDown);
            CalculateProgressAndDisplay();
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
            // _isChargingUp = false;
        }
        public void SetDamage(float damage)
        {
            _baseDamage = damage;
            _damage = damage;
        }
        public void SetDPS(float dps) {
            {
                _baseDPS = dps;
            }
        }
        public float GetDPS()
        {
            return _baseDPS;
        }
        public void ReCalculateDamageWithHE(List<HEIncreaseDamage> hatchEffects)
        {
            _currentDPS = _baseDPS;
            foreach (HEIncreaseDamage hEIncreaseDamage in hatchEffects)
            {
                // HEIncreaseDamage hEIncreaseDamage = hatchEffect.GetComponent<HEIncreaseDamage>();                
                if (hEIncreaseDamage != null)
                {
                    _currentDPS += hEIncreaseDamage.GetDamageToAdd();
                }
            }
            // _damage = (dps / (_attackChargeUp + _attackWindDown));
            CalculateDamage(_currentDPS);
        }
        private void CalculateDamage(float dps)
        {
            _damage = (dps * (_attackChargeUp + _attackWindDown));
        }
        // public float GetDamage(List<GameObject> hatchEffects)
        public float GetDamage()
        {
            return _damage;
        }
        public bool IsAttackReady()
        {
            if ((_attackChargeCurrent >= _attackChargeUp) && (!_isWindingDown))
            return true;
            return false;
        }
        private void CalculateProgressAndDisplay()
        {
            if (_progressBarManager != null)
            {
                float progressPercent = 0;
                if (!_isWindingDown)
                {
                    progressPercent = _attackChargeCurrent / _attackChargeUp;
                }
                else 
                {
                    progressPercent = _attackChargeCurrent / (_attackWindDown + _attackChargeUp);
                }
                _progressBarManager.StrechFillByValue(progressPercent);
            }

        }
    }
}
