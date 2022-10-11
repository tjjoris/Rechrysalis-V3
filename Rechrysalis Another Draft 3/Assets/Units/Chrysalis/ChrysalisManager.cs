using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ChrysalisManager : MonoBehaviour
    {
        private int _chrysalisIndex;
        private ChrysalisTimer _chrysalisTimer;
        private float _timeCurrent;
        private float _timeMax;
        private GameObject _unitBuilding;

        public void Initialize(float _timeMax, GameObject _unitBuilding)
        {
            _chrysalisTimer = GetComponent<ChrysalisTimer>();
            _timeCurrent =0;
            this._timeMax = _timeMax;
            this._unitBuilding = _unitBuilding;
        }
        public void Tick(float _timeAmount)
        {
            _timeCurrent += _timeAmount;
            if (_timeCurrent >= _timeMax)
            {
                
            }
        }
    }
}
