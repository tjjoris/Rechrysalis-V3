using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using System;
using TMPro;

namespace Rechrysalis.CompCustomizerOld
{
    public class UpgradeButtonManager : MonoBehaviour
    {
        private int _indexOfUpgradeButton;
        public int IndexOfUpgradeButton;
        private bool _advUnit;
        public bool AdvUnit {get {return _advUnit;}}
        private UnitButtonManager _compUnitSetTo;
        public UnitButtonManager CompUnitSetTo {set { _compUnitSetTo = value;} get {return _compUnitSetTo;}}
        private UnitStatsSO _unitStats;
        public UnitStatsSO UnitStats {get {return _unitStats;}}
        private HatchEffectSO _hatchEffect;
        public HatchEffectSO HatchEffect {get {return _hatchEffect;}}

        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private IconSetBackGColor _iconSetBackGColour;
        [SerializeField] private TMP_Text _name;
        public Action<UpgradeButtonManager> _upgradeClicked;
        public void Initialize(UnitStatsSO _unitStats, HatchEffectSO _hatchEffect, bool _advUnit, int _indexOfUpgradeButton)
        {
            this._indexOfUpgradeButton = _indexOfUpgradeButton;
            Debug.Log($"index upgrade button " + this._indexOfUpgradeButton);
            this._advUnit = _advUnit;
            if (_hatchEffect != null)
            {
                this._hatchEffect = _hatchEffect;
                _name.text = _hatchEffect.HatchEffectName;
            }
            if (_unitStats != null)
            {
                _body.sprite = _unitStats.UnitSprite;
                _name.text = _unitStats.UnitName;
                this._unitStats = _unitStats;
                // _unitStats.Initialize();
            }
        }
        public void ClickUpgradeButton()
        {
            Debug.Log($"clicked");
            _upgradeClicked?.Invoke(this);            
        }
        public void SetBackGColour(Color _colour)
        {
            _iconSetBackGColour.SetBackGColour(_colour);
        }
    }
}
