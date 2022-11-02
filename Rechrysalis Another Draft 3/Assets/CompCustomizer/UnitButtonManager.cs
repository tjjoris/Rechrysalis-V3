using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class UnitButtonManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private IconSetBackGColor _iconSetBackGColour;
        public void Initialize(UnitStatsSO _unitStats)
        {
            _body.sprite = _unitStats.UnitSprite;
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
