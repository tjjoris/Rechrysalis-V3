using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class UpgradeButtonManager : MonoBehaviour
    {
        private UnitStatsSO _unitStats;
        private HatchEffectSO _hatchEffect;
        private SpriteRenderer _body;
        public Action<GameObject> _upgradeClicked;
        public void Initialize(UnitStatsSO _unitStats, HatchEffectSO _hatchEffect)
        {
            this._hatchEffect = _hatchEffect;
            this._unitStats = _unitStats;
            if (_hatchEffect != null)
            {
                // _body.sprite = _hatchEffect
            }
            if (_unitStats != null)
            {
                _body.sprite = _unitStats.UnitSprite;
            }
        }
        public void ClickUpgradeButton()
        {
            _upgradeClicked?.Invoke(this.gameObject);
        }
    }
}
