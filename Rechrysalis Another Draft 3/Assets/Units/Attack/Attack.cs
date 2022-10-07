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
        private float _baseRange;

        public void Initialize(UnitStatsSO _unitStats, TargetsListSO _targetsList)
        {   
            this._targetsList = _targetsList;
            this._unitStats = _unitStats;
            this._baseRange = _unitStats.BaseRange;
            _attackChargeUp = _unitStats.AttackChargeUp;
            _attackWindDown = _unitStats.AttackWindDown;
            _baseDamage = _unitStats.BaseDamage;
            _projectilesPool = GetComponent<ProjectilesPool>();
            ResetUnit();
        }
        public void ResetUnit()
        {
            _attackChargeCurrent = 0;
            _isWindingDown = false;
        }
        public void Tick(float _timeAmount)
        {
            if (_attackChargeCurrent >= _attackWindDown) 
            {
                _attackChargeCurrent = 0;
                _isWindingDown = false;
            }
            if ((_isWindingDown) && (_attackChargeCurrent >= _attackChargeUp) && (_attackChargeCurrent < _attackWindDown))
            {
                _attackChargeCurrent += _timeAmount;
            }
            if ((_attackChargeCurrent >= _attackChargeUp) && (_isStopped))
            {
                _isWindingDown = true;
            }
            if ((_attackChargeCurrent < _attackChargeUp) && (_isStopped))
            {
                Debug.Log($"increae time");
                _attackChargeCurrent += _timeAmount;                
            }            
        }
        private GameObject ChooseBestTarget()
        {
            for (int _target = 0; _target < _targetsList.Targets.Count; _target ++)
                {
                    if (_targetsList.Targets[_target] != null) {
                        if ((_targetsList.Targets[_target].transform.position - gameObject.transform.position).magnitude <= GetRange())
                        {
                            return _targetsList.Targets[_target];                        
                        }
                    }
                }
            {

            }
            return null;
        }
        private float GetRange()
        {
            return _baseRange;
        }
    }
}
