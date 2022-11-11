using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class AIAttackChargeUpTimer : MonoBehaviour
    {
        private bool _aiImperfectionChargingUp;
        private bool _aiImperfectionWindingDown;
        private float _aiImperfectionCurrent;
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

        public void Initialize(float _chargeUp, float _windDown)
        {
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
        public void Tick(float _chargeCurrent, bool _chargingUp, bool _windingDown)
        {
            if ((!_aiImperfectionChargingUp) && (_windingDown))
            {
                if (_chargeCurrent >= (_chargeUp + _windDown + _currentStart))
                {
                    SetChargUpTrue();
                }
            }
            if ((!_aiImperfectionChargingUp) && (_chargingUp))
            {
                if (_chargeCurrent >= (_currentStart))
                {
                    SetChargUpTrue();
                }
            }
            if ((!_aiImperfectionWindingDown) && (_chargingUp))
            {
                if (_chargeCurrent >= (_chargeUp + _endMinus))
                {
                    SetWindDownTrue();
                }
            }
            if ((!_aiImperfectionWindingDown) && (_windingDown))
            {
                if (_chargeCurrent >= (_endPlus))
                {
                    SetWindDownTrue();
                }
            }
        }
        private void SetChargUpTrue()
        {
            _aiImperfectionChargingUp = true;
            _aiImperfectionWindingDown = false;
            _changeCanMove?.Invoke(false);
        }
        private void SetWindDownTrue()
        {
            _aiImperfectionWindingDown = true;
            _aiImperfectionChargingUp = false;
            SetCurrentStartAndEnd();
            _changeCanMove?.Invoke(true);
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
