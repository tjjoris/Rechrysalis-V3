using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using TMPro;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class UnitButtonManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private IconSetBackGColor _iconSetBackGColour;
        [SerializeField] private TMP_Text _name;
        private UnitStatsSO _unitStats;
        public UnitStatsSO UnitStats {get {return _unitStats;}}
        public Action<UnitButtonManager> _unitButtonClicked;
        public void Initialize(UnitStatsSO _unitStats)
        {
            if (_unitStats != null)
            {
                this._unitStats = _unitStats;
                _unitStats.Initialize();
            }
            _body.sprite = _unitStats.UnitSprite;
            _name.text = _unitStats.UnitName;
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
