using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;


namespace Rechrysalis.Attacking
{
    public class Range : MonoBehaviour
    {
        private float _baseRange;

        public void Initialize(UnitStatsSO _unitStats)
        {
            _baseRange = _unitStats.BaseRange;
        }
        public float GetRange()
        {
            return _baseRange;
        }
    }
}
