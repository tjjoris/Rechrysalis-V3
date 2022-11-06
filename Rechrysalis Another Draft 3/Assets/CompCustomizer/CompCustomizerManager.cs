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
        [SerializeField] private UnitStatsSO _emptyUnitStatsSO;
        private UpgradeButtonManager[] _upgradeButtonArray;
        private UnitButtonManager[] _arrayOfUnitButtonManagers;
        private UnitStatsSO[] _appliedUnitsToComp;
        private HatchEffectSO[] _appliedHatchEffectsToComp;
        private UnitButtonManager _compPositionSelected;
        private UpgradeButtonManager _upgradeSelected;
        private List<UpgradeButtonManager> _listOfSetUpgrades;
        
        public void Initialize(CompSO _compSO, Color _basicColour, Color _advColour, Color _hatchColour, int _level)
        {
            this._compSO = _compSO;
            if (_level == 0)
            {
                ResetWholeComp();
            }
            _readyButton.SetActive(false);
            _listOfSetUpgrades = new List<UpgradeButtonManager>();
            // _appliedUnitsToComp = new UnitStatsSO[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            // _appliedHatchEffectsToComp = new HatchEffectSO[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            _appliedUnitsToComp = _compSO.UnitSOArray;
            _appliedHatchEffectsToComp = _compSO.HatchEffectSOArray;
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
            _compWindowManager.Initialize(_compSO, _basicColour, _advColour, _emptyUnitStatsSO);
            _arrayOfUnitButtonManagers = _compWindowManager.ArrayOfUnitButtonManagers;
            _displayManager.Initialize();
            SubscribeToButtons();
            CheckToMakeEmptyUnits();
            CheckIfCompIsFullToEnableReady();
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
        private void ResetWholeComp()
        {
            _compSO.UnitSOArray = new UnitStatsSO[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            _compSO.HatchEffectSOArray = new HatchEffectSO[_compSO.ParentUnitCount * _compSO.ChildUnitCount];

            for (int _unitIndex = 0; _unitIndex < _compSO.UnitSOArray.Length; _unitIndex++)
            {
                _compSO.UnitSOArray[_unitIndex] = null;
                _compSO.HatchEffectSOArray[_unitIndex] = null;
            }           
        }
        private void UpgradeClickedFunction(UpgradeButtonManager _upgradeButtonManager)
        {
            _upgradeSelected = _upgradeButtonManager;
            if (_upgradeSelected.UnitStats != null)
            {
            _displayManager.DisplayUnitText(_upgradeButtonManager.UnitStats);
            }
            if (_upgradeSelected.HatchEffect != null)
            {
                _displayManager.DisplayHatchText(_upgradeButtonManager.HatchEffect);
            }
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
                    if (_upgradeSelected.UnitStats != null)
                    {
                        // _appliedUnitsToComp[_compPositionSelected.CompPosition] = _upgradeSelected.UnitStats;
                        // _compPositionSelected.ChangeUnit(_upgradeSelected.UnitStats);
                        ChangeUnit(_compPositionSelected, _upgradeSelected.UnitStats);
                    }            
                    else if (_upgradeSelected.HatchEffect != null)
                    {
                        _appliedHatchEffectsToComp[_compPositionSelected.CompPosition] = _upgradeSelected.HatchEffect;
                        _compPositionSelected.ChangeHatchEffect(_upgradeSelected.HatchEffect);
                    }        
                    _listOfSetUpgrades.Add(_upgradeSelected);
                    _compPositionSelected = null;
                    _upgradeSelected = null;
                    if (_listOfSetUpgrades.Count >= _numberOfUpgradesToChoose)
                    {
                        _readyButton.SetActive(true);
                    }
                    CheckToMakeEmptyUnits();
                    CheckIfCompIsFullToEnableReady();
                }
            }
        }
        // private void CheckToSetEmptyUnitWhenChangingHatchEffect()
        // {
        //     if (_compPositionSelected.UnitStats == null)
        //     {
        //         ChangeUnit(_compPositionSelected, _emptyUnitStatsSO);
        //     }
        // }
        private void ChangeUnit(UnitButtonManager _compPosition, UnitStatsSO _newUnit)
        {
             _appliedUnitsToComp[_compPosition.CompPosition] = _newUnit;
            _compPosition.ChangeUnit(_newUnit);
        }
        private void RemoveUpgrade(int _upgradeIndex)
        {
            _listOfSetUpgrades[_upgradeIndex].CompUnitSetTo.ResetUnit();
            int _oldUnitIndexInComp = _listOfSetUpgrades[_upgradeIndex].CompUnitSetTo.IndexInComp;
            _appliedUnitsToComp[_listOfSetUpgrades[_upgradeIndex].CompUnitSetTo.IndexInComp] = null;
            // _listOfSetUpgrades[_upgradeIndex].CompUnitSetTo = null;
            _listOfSetUpgrades.RemoveAt(_upgradeIndex);
        }
        private void CheckToMakeEmptyUnits()
        {
            for (int _parentIndex = 0; _parentIndex < _compSO.ParentUnitCount; _parentIndex ++)
            {
                bool _childUpgradePresent = false;
                for (int _childIndex = 0; _childIndex < _compSO.ChildUnitCount; _childIndex ++)
                {
                    int _unitIndex = (_parentIndex * _compSO.ParentUnitCount) + _childIndex;
                    if ((_childIndex != 0))
                    {
                        if (_appliedUnitsToComp[(_parentIndex * _compSO.ParentUnitCount)] == null)
                        {
                            if ((_appliedUnitsToComp[_unitIndex] != null) || (_appliedHatchEffectsToComp[_unitIndex] != null))
                            {
                                Debug.Log($"changed basic to empty because advanced");
                                ChangeUnit(_arrayOfUnitButtonManagers[_parentIndex * _compSO.ParentUnitCount], _emptyUnitStatsSO);
                            }
                        }                    
                        if ((_appliedUnitsToComp[_unitIndex] == null) && (_appliedHatchEffectsToComp[_unitIndex] != null))
                        {
                            ChangeUnit(_arrayOfUnitButtonManagers[_unitIndex], _emptyUnitStatsSO);
                        }
                    }
                    if ((_childIndex != 0) && (_appliedUnitsToComp[_unitIndex] != null) && (_appliedUnitsToComp[_unitIndex].UnitName == "Empty") && (_appliedHatchEffectsToComp[_unitIndex] == null))
                    {
                        ChangeUnit(_arrayOfUnitButtonManagers[_unitIndex], null);
                    }
                    if (((_appliedUnitsToComp[_unitIndex] != null) && (_appliedUnitsToComp[_unitIndex].UnitName != "Empty")) || (_appliedHatchEffectsToComp[_unitIndex] != null))
                    {
                        if (_appliedUnitsToComp[_unitIndex] != null) Debug.Log($" name " + _appliedUnitsToComp[_unitIndex].UnitName + " index " + _unitIndex);                        
                        _childUpgradePresent = true;
                    }
                }
                Debug.Log($"child or unit hatch " + _childUpgradePresent + " parent " + _parentIndex);
                if ((_appliedUnitsToComp[_parentIndex * _compSO.ParentUnitCount] != null) && (_appliedUnitsToComp[_parentIndex * _compSO.ParentUnitCount].UnitName == "Empty") && (_childUpgradePresent == false))                
                {
                    Debug.Log($"clear unit");
                    ChangeUnit(_arrayOfUnitButtonManagers[_parentIndex * _compSO.ParentUnitCount], null);
                }
            }
        }
        public void ReadyClicked()
        {   
            _compSO.UnitSOArray = _appliedUnitsToComp;
            _compSO.HatchEffectSOArray = _appliedHatchEffectsToComp;
            SceneManager.LoadScene(1);
        }
        private void CheckIfCompIsFullToEnableReady()
        {            
            if (LoopCompAndCheckIfReady())
            {
                _readyButton.SetActive(true);
            }
        }
        private bool LoopCompAndCheckIfReady()
        {
            for (int _compIndex = 0; _compIndex < _appliedUnitsToComp.Length; _compIndex++)
            {
                if (_appliedUnitsToComp[_compIndex] == null)
                {
                    return false;
                }
                if (_appliedHatchEffectsToComp[_compIndex] == null)
                {
                    return false;
                }
                if (_appliedUnitsToComp[_compIndex].UnitName == "Empty")
                {
                    return false;
                }
                if (_appliedHatchEffectsToComp[_compIndex].HatchEffectName == "Empty")
                {
                    return false;
                }
            }   
            return true;
        }
        
    }
}
