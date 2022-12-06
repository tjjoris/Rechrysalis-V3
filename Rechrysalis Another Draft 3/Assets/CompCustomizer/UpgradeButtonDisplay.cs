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
                DisplayForUnit();
            }
            else if (_selectionIndexToSelection.GetThisUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
            {
                DisplayForHatchEffect();
            }
        }
        private void DisplayForUnit()
        {
            _body.sprite = _selectionIndexToSelection.GetUnitStatsSO().UnitSprite;
            _nameText.text = _selectionIndexToSelection.GetUnitStatsSO().UnitName;
        }
        private void DisplayForHatchEffect()
        {
            _nameText.text = _selectionIndexToSelection.GetHatchEffectSO().HatchEffectName;
        }
    }
}
