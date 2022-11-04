using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class CompCustomizerManager : MonoBehaviour
    {
        private CompSO _compSO;
        [SerializeField] private CompWindowManager _compWindowManager;
        [SerializeField] private DisplayManager _displayManager;
        private int _numberOfUpgradesToChoose;
        [SerializeField] private GameObject _upgradeButtonHorizontalLayoutGroupPrefab;
        [SerializeField] private GameObject _upgradeButtonVerticalLayoutGroup;
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        private UpgradeButtonManager[] _upgradeButtonArray;
        private UnitButtonManager[] _arrayOfUnitButtonManagers;
        private GameObject[] _appliedUpgradesToComp;
        
        public void Initialize(CompSO _compSO, Color _basicColour, Color _advColour, Color _hatchColour)
        {
            _appliedUpgradesToComp = new GameObject[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            this._compSO = _compSO;
            _numberOfUpgradesToChoose = _compCustomizerSO.NumberOfUpgrades;
            _upgradeButtonArray = new UpgradeButtonManager[3 * _numberOfUpgradesToChoose];
            UnitStatsSO _basicUnitNotToPick = null;
            UnitStatsSO _advUnitNotToPick = null;
            HatchEffectSO _hatchEffectNotToPick = null;
            for (int _numberOfUpgradesCount = 0; _numberOfUpgradesCount < _numberOfUpgradesToChoose; _numberOfUpgradesCount ++)
            {
                GameObject go = Instantiate (_upgradeButtonHorizontalLayoutGroupPrefab, _upgradeButtonVerticalLayoutGroup.transform);
                UpgradeButtonHorizontalLayoutManager _horizontalManager = go.GetComponent<UpgradeButtonHorizontalLayoutManager>();
                _horizontalManager?.Initialize(_compCustomizerSO, _basicUnitNotToPick, _advUnitNotToPick, _hatchEffectNotToPick, _basicColour, _advColour, _hatchColour);
                _basicUnitNotToPick = _horizontalManager.BasicUnitSO;
                _advUnitNotToPick = _horizontalManager.AdvUnitSO;
                _hatchEffectNotToPick = _horizontalManager.HatchEffectSO;
                for (int _upgradeIndex = 0; _upgradeIndex < 3; _upgradeIndex ++)
                {
                    _upgradeButtonArray[_upgradeIndex + (3 * _numberOfUpgradesCount)] = _horizontalManager.UpgradeButtonManagerArray[_upgradeIndex];
                }
            }            
            _compWindowManager.Initialize(_compSO, _basicColour, _advColour);
            _arrayOfUnitButtonManagers = _compWindowManager.ArrayOfUnitButtonManagers;
            _displayManager.Initialize();
            SubscribeToButtons();
        }
        private void SubscribeToButtons()
        {
            if (_upgradeButtonArray.Length > 0) {
                for (int _upgradeIndex = 0; _upgradeIndex < _upgradeButtonArray.Length; _upgradeIndex ++)
                {
                    _upgradeButtonArray[_upgradeIndex]._upgradeClicked -= UpgradeClickedFunction;
                    _upgradeButtonArray[_upgradeIndex]._upgradeClicked += UpgradeClickedFunction;
                }
            }
            if (_arrayOfUnitButtonManagers.Length > 0) 
            {
                for (int _unitIndex = 0; _unitIndex < _arrayOfUnitButtonManagers.Length; _unitIndex ++)
                {
                    _arrayOfUnitButtonManagers[_unitIndex]._unitButtonClicked -= UnitClickedFunction;
                    _arrayOfUnitButtonManagers[_unitIndex]._unitButtonClicked += UnitClickedFunction;
                }
            }
        }
        private void OnEnable()
        {
            SubscribeToButtons();
        }
        private void OnDisable()
        {
            if (_upgradeButtonArray.Length > 0) {
                for (int _upgradeIndex = 0; _upgradeIndex < _upgradeButtonArray.Length; _upgradeIndex++)
                {
                    _upgradeButtonArray[_upgradeIndex]._upgradeClicked -= UpgradeClickedFunction;
                }
            }
            if (_arrayOfUnitButtonManagers.Length > 0)
            {
                for (int _unitIndex = 0; _unitIndex < _arrayOfUnitButtonManagers.Length; _unitIndex++)
                {
                    _arrayOfUnitButtonManagers[_unitIndex]._unitButtonClicked -= UnitClickedFunction;
                }
            }
        }
        private void UpgradeClickedFunction(UpgradeButtonManager _upgradeButtonManager)
        {
            _displayManager.DisplayUnitText(_upgradeButtonManager.UnitStats);
        }
        private void UnitClickedFunction(UnitButtonManager _unitButotnManager)
        {
            _displayManager.DisplayUnitText(_unitButotnManager.UnitStats);
        }
    }
}