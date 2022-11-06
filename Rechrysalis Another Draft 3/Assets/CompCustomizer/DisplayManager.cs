using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField]private TMP_Text _info;
        private string _initialText = "Pick a unit or hatch effect and apply it to a comp slot twice.  At least one must be a unit.";
        public void Initialize()
        {
            _info.text = _initialText;
        }
        public void DisplayUnitText(UnitStatsSO _unitStats)
        {
            // _info.text = _unitStats.UnitName
            string _textToDisplay = _unitStats.UnitName + "\n" + "tier " + _unitStats.TierMultiplier.Tier.ToString() + "\n" + "range " + _unitStats.BaseRange.ToString();        
            _info.text = _textToDisplay;
        }
        public void DisplayHatchText(HatchEffectSO _hatchEffect)
        {
            string _textToDisplay = _hatchEffect.HatchEffectName;
            _info.text = _textToDisplay;
        }
    }
}
