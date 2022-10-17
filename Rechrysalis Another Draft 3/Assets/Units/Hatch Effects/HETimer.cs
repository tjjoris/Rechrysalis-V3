using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HETimer : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _timeCurrent;
        private int _unitIndex;
        public int UnitIndex {set {_unitIndex = value;} get {return _unitIndex;}}
        private bool _allUnits;
        public bool AllUnits {set {_allUnits = value;} get {return _allUnits;}}

        public void Initialize(int _unitIndex, bool _allunits)
        {
            this._unitIndex = _unitIndex;
            this._allUnits = _allunits;
        }
        public void Tick(float _timeAmount)
        {
            _timeCurrent += _timeAmount;
        }

        public bool CheckIsExpired()
        {
            if (_timeCurrent >= _duration)
            {
                return true;
            }
            return false;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
