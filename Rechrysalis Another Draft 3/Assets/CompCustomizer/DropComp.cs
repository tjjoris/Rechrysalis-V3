using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.CompCustomizer
{
    public class DropComp : DropBackGround
    {
        [SerializeField] private  ControllerHPTokens _controllerHPTokens;
        public ControllerHPTokens ControllerHPTokens {set { _controllerHPTokens = value;}}
        protected override void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {
            // base.DropUpgrade(compUpgradeManager);
            if ((compUpgradeManager.GetUpgradeType() != Unit.UpgradeTypeClass.UpgradeType.SingleHeart) || ((compUpgradeManager.GetUpgradeType() == Unit.UpgradeTypeClass.UpgradeType.SingleHeart) && (_controllerHPTokens.IsMissingTokens())))
            {
                base.DropUpgrade(compUpgradeManager);
                // compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
                // _buttonDropped?.Invoke(compUpgradeManager);
                _controllerHPTokens.AddTokens(compUpgradeManager.GetUpgradeTypeClass().GetControllerHeartUpgrade().HeartCount);
            }
        }
    }
}
