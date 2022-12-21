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

        public void SetButotnDisplay(UpgradeTypeClass upgradeTypeClass)
        {
            if ((upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic))
            {
                DisplayForUnit(upgradeTypeClass.GetUnitStatsSO());
            }
            else if (upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect)
            {
                DisplayForHatchEffect(upgradeTypeClass.GetHatchEffectSO());
            }
            else if (upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced)
            {
                DisplayForAdvUnit(upgradeTypeClass.GetAdvUnitModifierSO());
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
        public void DisplayForAdvUnit(AdvUnitModifierSO advUnitModifierSO)
        {
            _nameText.text = advUnitModifierSO.UpgradeName;
        }
    }
}
