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
            if (_unitStats == null)
            {
                _info.text = "No unit";
                return;
            }
            string _textToDisplay = _unitStats.UnitName + "\n" + "tier " + _unitStats.TierMultiplier.Tier.ToString() + " range " + _unitStats.BaseRange.ToString();                   
            _info.text = _textToDisplay;
        }
        public void AddHatchText (HatchEffectSO _hatchEffect)
        {
            if (_hatchEffect != null)
            {
                _info.text = _info.text + "\n" + _hatchEffect.HatchEffectName;
            }
        }
        public void DisplayHatchText(HatchEffectSO _hatchEffect)
        {
            string _textToDisplay = _hatchEffect.HatchEffectName;
            _info.text = _textToDisplay;
        }
    }
}
