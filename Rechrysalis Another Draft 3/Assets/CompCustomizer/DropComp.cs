using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class DropComp : DropBackGround
    {

        protected override void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {
            base.DropUpgrade(compUpgradeManager);
            if ((compUpgradeManager.GetUpgradeType() != Unit.UpgradeTypeClass.UpgradeType.SingleHeart) || ((compUpgradeManager.GetUpgradeType() == Unit.UpgradeTypeClass.UpgradeType.SingleHeart) && (_compsAndUnitsSO.ControllerHPTokensCurrent < _compsAndUnitsSO.ControllerHPTokensMax)))

                _buttonDropped?.Invoke(compUpgradeManager);
        }
    }
}
