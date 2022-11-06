using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using TMPro;
using System;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class UnitButtonManager : MonoBehaviour
    {
        private int _compPosition;
        public int CompPosition {get {return _compPosition;}}
        private bool _advUnit;
        public bool AdvUnit {get {return _advUnit;}}
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private IconSetBackGColor _iconSetBackGColour;
        [SerializeField] private TMP_Text _name;
        private UnitStatsSO _unitStats;
        public UnitStatsSO UnitStats {get {return _unitStats;}}
        private UnitStatsSO _newUnit;
        private HatchEffectSO _hatchEffect;
        private HatchEffectSO _newHatchEffect;
        public Action<UnitButtonManager> _unitButtonClicked;
        public void Initialize(UnitStatsSO _unitStats, HatchEffectSO _hatchEffect, int _compPosition, bool _advUnit, UnitStatsSO _emptyUnitSO)
        {
            this._advUnit = _advUnit;
            // if (_unitStats == null)
            // {
            //     _unitStats = _emptyUnitSO;
            // }
            if (_unitStats != null)
            {
            this._unitStats = _unitStats;
            _newUnit = _unitStats;
            SetButtonAppearanceToUnit();
            _unitStats.Initialize();
            }
            if (_hatchEffect != null)
            {
                this._hatchEffect = _hatchEffect;
                _newHatchEffect = _hatchEffect;
            }
            this._compPosition = _compPosition;
        }
        public void ChangeUnit(UnitStatsSO _newUnit)
        {
            this._newUnit = _newUnit;
            SetButtonAppearanceToUnit();
        }
        public void ResetUnit()
        {
            _newUnit = _unitStats;
            SetButtonAppearanceToUnit();
        }
        public void ChangeHatchEffect(HatchEffectSO _newHatchEffect)
        {
            this._newHatchEffect = _newHatchEffect;            
        }
        public void ResetHatchEffect()
        {
            _newHatchEffect = _hatchEffect;
        }
        private void SetButtonAppearanceToUnit()
        {
            if (_newUnit == null)
            {
                _body.sprite = null;
                _name.text = "";
                return;
            }
            _body.sprite = _newUnit.UnitSprite;
            _name.text = _newUnit.UnitName;
        }
        public void ClickUnitButton()
        {
            Debug.Log($"unit clicked");
            _unitButtonClicked?.Invoke(this);
        }
        public void SetBackGColour(Color _colour)
        {
            _iconSetBackGColour.SetBackGColour(_colour);   
        }
    }
}
