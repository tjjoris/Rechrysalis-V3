using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class CompCustomizerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradeButtonPrefab;
        private int _numberOfUpgradesToChoose = 2;
        [SerializeField] private GameObject _upgradeButtonHorizontalLayoutGroupPrefab;
        [SerializeField] private GameObject _upgradeButtonVerticalLayoutGroup;
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        private UpgradeButtonManager[] _upgradeButtonArray;
        
        public void Initialize()
        {
            _upgradeButtonArray = new UpgradeButtonManager[3 * _numberOfUpgradesToChoose];
            UnitStatsSO _basicUnitNotToPick = null;
            UnitStatsSO _advUnitNotToPick = null;
            HatchEffectSO _hatchEffectNotToPick = null;
            for (int _numberOfUpgradesCount = 0; _numberOfUpgradesCount < _numberOfUpgradesToChoose; _numberOfUpgradesCount ++)
            {
                GameObject go = Instantiate (_upgradeButtonHorizontalLayoutGroupPrefab, _upgradeButtonVerticalLayoutGroup.transform);
                UpgradeButtonHorizontalLayoutManager _horizontalManager = go.GetComponent<UpgradeButtonHorizontalLayoutManager>();
                _horizontalManager?.Initialize(_compCustomizerSO, _basicUnitNotToPick, _advUnitNotToPick, _hatchEffectNotToPick);
                _basicUnitNotToPick = _horizontalManager.BasicUnitSO;
                _advUnitNotToPick = _horizontalManager.AdvUnitSO;
                _hatchEffectNotToPick = _horizontalManager.HatchEffectSO;
            }
        }
    }
}
