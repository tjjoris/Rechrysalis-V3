using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class Rechrysalize : MonoBehaviour
    {
        private int _maxSubUnits;
        private int _nextEvolvedUnit;
        private ChrysalisTimer _chryslisTimer;
        public Action<int> _startChrysalis;

        private void Awake()
        {

            _chryslisTimer = GetComponent<ChrysalisTimer>();
        }
        public void Initialize (int _maxSubUnits)
        {
            this._maxSubUnits = _maxSubUnits;
            _nextEvolvedUnit = 0;
        }
        public void ResetRechrysalisSubIndex()
        {
            _nextEvolvedUnit = 0;
        }
        public void UnitDies()
        {
            _startChrysalis?.Invoke(_nextEvolvedUnit);
        }
        public void ChrysalizeUnit(int _nextEvolvedUnit)
        {
            SetNextEvolved(_nextEvolvedUnit);
            this._nextEvolvedUnit = 0;
        }
        public void SetNextEvolved(int _nextEvolvedUnit)
        {
            if (_nextEvolvedUnit <= _maxSubUnits)
            {
                this._nextEvolvedUnit = _nextEvolvedUnit;
            }
        }
    }
}
