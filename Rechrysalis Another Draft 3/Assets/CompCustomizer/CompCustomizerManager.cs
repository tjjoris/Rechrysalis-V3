using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;
using UnityEngine.SceneManagement;

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
        [SerializeField] private GameObject _readyButton;
        private UpgradeButtonManager[] _upgradeButtonArray;
        private UnitButtonManager[] _arrayOfUnitButtonManagers;
        private UnitStatsSO[] _appliedUnitsToComp;
        private HatchEffectSO[] _appliedHatchEffectsToComp;
        private UnitButtonManager _compPositionSelected;
        private UpgradeButtonManager _upgradeSelected;
        private List<UpgradeButtonManager> _listOfSetUpgrades;
        
        public void Initialize(CompSO _compSO, Color _basicColour, Color _advColour, Color _hatchColour)
        {
            _readyButton.SetActive(false);
            _listOfSetUpgrades = new List<UpgradeButtonManager>();
            // _appliedUnitsToComp = new UnitStatsSO[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            // _appliedHatchEffectsToComp = new HatchEffectSO[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            _appliedUnitsToComp = _compSO.UnitSOArray;
            _appliedHatchEffectsToComp = _compSO.HatchEffectSOArray;
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
            _upgradeSelected = _upgradeButtonManager;
            _displayManager.DisplayUnitText(_upgradeButtonManager.UnitStats);
            CheckIfCompChanged();
        }
        private void UnitClickedFunction(UnitButtonManager _unitButotnManager)
        {
            _compPositionSelected = _unitButotnManager;
            _displayManager.DisplayUnitText(_unitButotnManager.UnitStats);
            CheckIfCompChanged();
        }
        private void CheckIfCompChanged()
        {
            if ((_compPositionSelected != null) && (_upgradeSelected != null))
            {
                if (_compPositionSelected.AdvUnit == _upgradeSelected.AdvUnit)                
                {
                    if (_listOfSetUpgrades.Contains(_upgradeSelected))
                    {
                        RemoveUpgrade(_listOfSetUpgrades.IndexOf(_upgradeSelected));
                    }
                    else if (_listOfSetUpgrades.Count >= _numberOfUpgradesToChoose)
                    {
                        RemoveUpgrade(0);
                    }
                    _upgradeSelected.CompUnitSetTo = _compPositionSelected;
                    if (_compPositionSelected.UnitStats != null)
                    {
                        _appliedUnitsToComp[_compPositionSelected.CompPosition] = _upgradeSelected.UnitStats;
                        _compPositionSelected.ChangeUnit(_upgradeSelected.UnitStats);
                    }                    
                    _listOfSetUpgrades.Add(_upgradeSelected);
                    _compPositionSelected = null;
                    _upgradeSelected = null;
                    if (_listOfSetUpgrades.Count >= _numberOfUpgradesToChoose)
                    {
                        _readyButton.SetActive(true);
                    }
                }
            }
        }
        private void RemoveUpgrade(int _upgradeIndex)
        {
            _listOfSetUpgrades[_upgradeIndex].CompUnitSetTo.ResetUnit();
            _listOfSetUpgrades[_upgradeIndex].CompUnitSetTo = null;
            _listOfSetUpgrades.RemoveAt(_upgradeIndex);
        }
        public void ReadyClicked()
        {   
            SceneManager.LoadScene(1);
        }
    }
}
