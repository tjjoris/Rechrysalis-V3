using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using UnityEngine.UI;

namespace Rechrysalis.CompCustomizer
{
    public class UpgradeButtonDisplay : MonoBehaviour
    {
        [SerializeField] private  Image _body;
        [SerializeField] private TMP_Text _nameText;
        private SelectionIndexToSelection _selectionIndexToSelection;
        public void Initialzie()
        {
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
        }

        public void SetButotnDisplay()
        {
            if ((_selectionIndexToSelection.GetThisUpgradeType() == UpgradeTypeClass.UpgradeType.Basic) || (_selectionIndexToSelection.GetThisUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced))
            {
                DisplayForUnit(_selectionIndexToSelection.GetUnitStatsSO());
            }
            else if (_selectionIndexToSelection.GetThisUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
            {
                DisplayForHatchEffect(_selectionIndexToSelection.GetHatchEffectSO());
            }
        }
        public void DisplayForUnit(UnitStatsSO unitStatsSO)
        {
            _body.sprite = unitStatsSO.UnitSprite;
            _nameText.text = unitStatsSO.UnitName;
        }
        public void DisplayForHatchEffect(HatchEffectSO hatchEffectSO)
        {
            _nameText.text = hatchEffectSO.HatchEffectName;
        }
    }
}
