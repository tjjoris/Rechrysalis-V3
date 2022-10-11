using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class Rechrysalize : MonoBehaviour
    {
        private int _maxSubUnits;
        private int _nextEvolvedUnit;

        public void Initialize (int _maxSubUnits)
        {
            this._maxSubUnits = _maxSubUnits;
            _nextEvolvedUnit = 0;
        }
        public void UnitDies()
        {
            _nextEvolvedUnit = 0;
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
