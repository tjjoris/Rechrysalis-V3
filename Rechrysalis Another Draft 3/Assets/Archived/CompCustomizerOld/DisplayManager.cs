using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizerOld
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField]private TMP_Text _info;
        private UnitStatsSO _unitStats;
        private HatchEffectSO _hatchEffect;
        private float _manaCost;
        private string _initialText = "Pick a unit or hatch effect and apply it to a comp slot twice.  At least one must be a unit.";
        public void Initialize()
        {
            _info.text = _initialText;
        }
        public void DisplayText(UnitStatsSO _unitStats, HatchEffectSO _hatchEffect)
        {            
            // if (_unitStats == null)
            // {
            //     this._unitStats = _unitStats;
            //     _info.text = "No unit";
            //     return;
            // }
            this._hatchEffect = _hatchEffect;
            this._unitStats = _unitStats;
            string _textToDisplay = StringOfUnitInfo(_unitStats, _hatchEffect);
            _info.text = _textToDisplay;
        }
        public void AddHatchText (HatchEffectSO _hatchEffect)
        {
            if (_hatchEffect != null)
            {
                string _textToDisplay = StringOfUnitInfo(_unitStats, _hatchEffect);
                _info.text = _textToDisplay + "\n" + _hatchEffect.HatchEffectName;
            }
        }
        public void DisplayHatchText(HatchEffectSO _hatchEffect)
        {
            string _textToDisplay = _hatchEffect.HatchEffectName;
            _info.text = _textToDisplay;
        }
        private string StringOfUnitInfo(UnitStatsSO _unitStats, HatchEffectSO _hatchEffect)
        {
            Debug.Log($"unit stats " +_unitStats + " hatch effect "+ _hatchEffect);
            float _manaCost = 0;
            string _textToDisplay = "";
            if (_unitStats != null)
            {
                _textToDisplay +=  _unitStats.UnitName + "\n";
                _manaCost = _unitStats.Mana;
            }
            else
            {
                _textToDisplay += "\n";
            }
            if (_hatchEffect != null)
            {
                _textToDisplay += _hatchEffect.HatchEffectName;
                if (_unitStats == null)
                {
                    if (_hatchEffect.ManaMultiplier.Length > 0)
                    {
                        _manaCost = _hatchEffect.ManaMultiplier[0];
                    }
                }
                else 
                {                    
                    if (_hatchEffect.ManaMultiplier.Length >= _unitStats.TierMultiplier.Tier)
                    {
                        _manaCost *= _hatchEffect.ManaMultiplier[_unitStats.TierMultiplier.Tier - 1];
                    }
                }
            }
            _textToDisplay += "\n" + Mathf.Floor(_manaCost).ToString();
            return _textToDisplay;
        }
    }
}
