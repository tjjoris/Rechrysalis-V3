using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class AIAttackChargeUpTimer : MonoBehaviour
    {
        public ParentUnitManager _parentUnitManager;
        private bool _aiImperfectionChargingUp;
        private bool _aiImperfectionWindingDown;
        private float _aiImperfectionCurrent;
        private float _newFlaw = 0.5f;
        private float _largeFlaw = 0.5f;
        private float _smallFlaw = 0.25f;
        private float _startMinus = -0.3f;
        private float _startPlus = 0.1f;
        private float _endMinus = -0.1f;
        private float _endPlus = 0.3f;
        private float _chargeUp;
        private float _windDown;
        private float _currentStart;
        private float _currentEnd;
        public System.Action<bool> _changeCanMove;

        public void Initialize(float _chargeUp, float _windDown, ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            this._chargeUp = _chargeUp;
            this._windDown = _windDown;
            SetCurrentStartAndEnd();
        }
        private void SetCurrentStartAndEnd()
        {            
            _startMinus = _largeFlaw;
            _startPlus = _smallFlaw;
            _endMinus = _smallFlaw;
            _endPlus = _largeFlaw;
            _currentStart = UnityEngine.Random.Range(_startMinus, _startPlus);
            _currentEnd = UnityEngine.Random.Range(_endMinus, _endPlus);
            
        }
        public void Tick(float timeAmount, bool chargingUp, bool windingDown)
        {
            _aiImperfectionCurrent += timeAmount;
            if (_aiImperfectionCurrent >= _newFlaw)
            {
                _aiImperfectionCurrent = 0;
                _aiImperfectionChargingUp = chargingUp;
                _aiImperfectionWindingDown = windingDown;
                if ((windingDown == false) && (chargingUp == true))
                {
                    _parentUnitManager.AICanMove = false;
                    // _changeCanMove?.Invoke(false);
                }
                else 
                {
                    _parentUnitManager.AICanMove = true;
                    // _changeCanMove?.Invoke(true);
                }

            }
        }
        public void TickOld(float _timeAmount, bool _chargingUp, bool _windingDown)
        {
            _aiImperfectionCurrent += _timeAmount;
            if ((!_aiImperfectionChargingUp) && (_windingDown))
            {
                if (_aiImperfectionCurrent >= (_chargeUp + _windDown + _currentStart))
                {
                    SetChargUpTrue();
                }
            }
            if ((!_aiImperfectionChargingUp) && (_chargingUp))
            {
                if (_aiImperfectionCurrent >= (_currentStart))
                {
                    SetChargUpTrue();
                }
            }
            if ((!_aiImperfectionWindingDown) && (!_windingDown))
            {
                if (_aiImperfectionCurrent >= (_chargeUp + _endMinus))
                {
                    SetWindDownTrue();
                }
            }
            if ((!_aiImperfectionWindingDown) && (_windingDown))
            {
                if (_aiImperfectionCurrent >= (_endPlus))
                {
                    SetWindDownTrue();
                }
            }
        }
        private void SetChargUpTrue()
        {
            _aiImperfectionChargingUp = true;
            _aiImperfectionWindingDown = false;
            _parentUnitManager.AICanMove = false;
            // _changeCanMove?.Invoke(false);
        }
        private void SetWindDownTrue()
        {
            _aiImperfectionWindingDown = true;
            _aiImperfectionChargingUp = false;
            _aiImperfectionCurrent = 0;
            SetCurrentStartAndEnd();
            _parentUnitManager.AICanMove = true;
            // _changeCanMove?.Invoke(true);
        }
        public bool GetCharginUp()
        {
            return _aiImperfectionChargingUp;
        }
        public bool GetWindingDown()
        {
            return _aiImperfectionWindingDown;
        }
    }
}
