using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;


namespace Rechrysalis.Attacking
{
    public class Range : MonoBehaviour
    {
        private float _rangeDisToAccountForMovement;
        private float _baseRange;

        public void InitializeOld(UnitStatsSO _unitStats)
        {
            _baseRange = _unitStats.BaseRangeBasic;
        }
        public void Initialize(UnitClass unitClass)
        {
            _rangeDisToAccountForMovement = ((unitClass.AttackChargeUp) * 1f);
            // _rangeDisToAccountForMovement = 0;
            _baseRange = unitClass.Range;
            // _baseRange = baseRange;
        }
        public float GetRange()
        {
            return _baseRange;
        }
        public float GetRangeDistToAccountForMovement()
        {
            return _rangeDisToAccountForMovement;
        }
    }
}
