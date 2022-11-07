using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.UI
{
    public class ControllerHPBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _hpFill;
        private float _fillMax;
        private float _fillCurrent;
        private float _hpMax;
        private float _hpCurrent;

        public void Initialize(float _hpMax)
        {
            _fillMax = _hpFill.rect.width;
            _fillCurrent = _fillMax;
            this._hpMax = _hpMax;
        }
        public void ChangeHPBar(float _currentHP)
        {
            float _hpPercent = _currentHP / _hpMax;
            _hpFill.localScale = new Vector3(_hpPercent,1,1);
        }
    }
}
