using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;


namespace Rechrysalis.Attacking
{
    public class Range : MonoBehaviour
    {
        private float _rangeDisToAccountForMovement;
        private float _baseRange;
        [SerializeField] private float _currentRange;
        private List<HEIncreaseRange> _heRangeList;

        private void Awake()
        {
            _heRangeList = new List<HEIncreaseRange>();
        }

        public void InitializeOld(UnitStatsSO _unitStats)
        {
            _baseRange = _unitStats.BaseRangeBasic;
            _currentRange = _baseRange;
        }
        public void Initialize(UnitClass unitClass)
        {
            _rangeDisToAccountForMovement = ((unitClass.AttackChargeUp) * 1.5f);
            // _rangeDisToAccountForMovement = 0;
            _baseRange = unitClass.Range;
            _currentRange = _baseRange;
            // _baseRange = baseRange;
        }
        public float GetRange()
        {
            return _currentRange;
        }
        public float GetRangeDistToAccountForMovement()
        {
            return _rangeDisToAccountForMovement;
        }
        public void AddRangeHE(HEIncreaseRange heIncreaseRange)
        {
            _heRangeList.Add(heIncreaseRange);
            ReCalculateRange();
        }
        public void RemoveRangeHE(HEIncreaseRange heIncreaseRange)
        {
            RemoveRangeHEFromList(heIncreaseRange);
            ReCalculateRange();
        }
        private void RemoveRangeHEFromList(HEIncreaseRange heIncreaseRange)
        {
            if (!_heRangeList.Contains(heIncreaseRange)) return;
            _heRangeList.Remove(heIncreaseRange);
        }
        private void ReCalculateRange()
        {
            _currentRange = _baseRange;
            foreach (HEIncreaseRange heIncreaseRange in _heRangeList)
            {
                if (heIncreaseRange == null) continue;
                _currentRange += heIncreaseRange.GetRangeToAdd();
            }
        }
    }
}
