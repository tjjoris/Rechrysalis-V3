using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using TMPro;

namespace Rechrysalis.CompCustomizer
{
    public class UnitButtonManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private IconSetBackGColor _iconSetBackGColour;
        [SerializeField] private TMP_Text _name;
        public void Initialize(UnitStatsSO _unitStats)
        {
            _body.sprite = _unitStats.UnitSprite;
            _name.text = _unitStats.UnitName;
        }
        public void ClickUnitButton()
        {

        }
        public void SetBackGColour(Color _colour)
        {
            _iconSetBackGColour.SetBackGColour(_colour);   
        }
    }
}
