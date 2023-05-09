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
                DisplayForHatchEffect(upgradeTypeClass);            
            }
            else if (upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Advanced)
            {
                DisplayForAdvUnit(upgradeTypeClass.GetAdvUnitModifierSO());
            }
            else if (upgradeTypeClass.GetUpgradeType() == UpgradeTypeClass.UpgradeType.SingleHeart)
            {
                DisplayForHeart(upgradeTypeClass.GetControllerHeartUpgrade());
            }
        }
        public void DisplayForUnit(UnitStatsSO unitStatsSO)
        {
            _body.sprite = unitStatsSO.UnitSprite;
            _nameText.text = unitStatsSO.UnitName;
        }
        public void DisplayForHatchEffect(UpgradeTypeClass upgradeTypeClass)
        {
            _nameText.text = upgradeTypeClass.ButtonName;
        }
        public void DisplayForAdvUnit(AdvUnitModifierSO advUnitModifierSO)
        {
            _nameText.text = advUnitModifierSO.UpgradeName;
        }
        public void DisplayForHeart(ControllerHeartUpgrade controllerHeartUpgrade)
        {
            _body.sprite = controllerHeartUpgrade.Image;
            _nameText.text = controllerHeartUpgrade.HeartName;
        }
    }
}
