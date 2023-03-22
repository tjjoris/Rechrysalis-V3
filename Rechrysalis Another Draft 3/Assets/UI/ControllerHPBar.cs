using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.UI
{
    public class ControllerHPBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _hpFill;
        private float _fillMax;
        private float _fillCurrent;
        // private float _hpMax;
        private float _hpCurrent;
        private ControllerHealth _controllerHealth;

        public void Initialize(ControllerHealth controllerHealth)
        {
            _controllerHealth = controllerHealth;
            _fillMax = _hpFill.rect.width;
            _fillCurrent = _fillMax;
            // this._hpMax = _hpMax;
        }
        public void ChangeHPBar(float _currentHP)
        {
            float _hpPercent = _currentHP / _controllerHealth.HealthMax;
            _hpFill.localScale = new Vector3(_hpPercent,1,1);
        }
    }
}
