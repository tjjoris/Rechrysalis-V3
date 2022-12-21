using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Unit;
// using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionInitialize : MonoBehaviour
    {
        [SerializeField] private SelectionContainer _selectionContainer;
        private bool debugBool = true;
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private GameObject _upgradeButtonPrefab;
        private Transform _movingButtonHolder;
        private int _numberOfUpgrades = 3;
        [SerializeField] private UpgradeButtonManager[] _upgradebuttonManager;
        public UpgradeButtonManager[] UpgradeButtonManager { get{ return _upgradebuttonManager; } set{ _upgradebuttonManager = value; } }
        [SerializeField] private CompUpgradeManager[] _compUpgradeManagers;
        [SerializeField] private int[] _upgradeButtonIndex;
        private SelectionIndexToSelection _selectionIndexToSelection;
        private int _upgradeSelectionCount;
        private RandomUpgradeSelection _randomUpgradeSelection;
        private UpgradeTypeClass[] _upgradeTypeClassesToChooseFrom;
        public Action<UpgradeButtonManager> _onUpgradeButtonClicked;
        
        
        public void Initialize(CompCustomizerSO compCustomizerSO, Transform movingButtonHolder)
        {
            _movingButtonHolder = movingButtonHolder;
            _compUpgradeManagers = new CompUpgradeManager[_numberOfUpgrades];
            _upgradebuttonManager = new UpgradeButtonManager[_numberOfUpgrades];
            _upgradeButtonIndex = new int[_numberOfUpgrades];
            _compCustomizerSO = compCustomizerSO;
            _upgradeTypeClassesToChooseFrom = new UpgradeTypeClass[_numberOfUpgrades];
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
            _selectionIndexToSelection.Initialize(compCustomizerSO);
            _randomUpgradeSelection= GetComponent<RandomUpgradeSelection>();
            _randomUpgradeSelection.Initialize();
            CalculateUpgradeSelectionCount();
            CreateAllSelectionButtons();   
            // SubscribeToUpgradeButtons();  
        }
        private void CreateSelectionButton(int index)
        {            
            GameObject _selectionButton = Instantiate(_upgradeButtonPrefab, _selectionContainer.transform);
            _compUpgradeManagers[index] = _selectionButton.GetComponent<CompUpgradeManager>();
            _compUpgradeManagers[index].InitializeOldStillUsed(-1, -1, _movingButtonHolder);
            UpgradeTypeClass _randomUpgradeTypeClass = _randomUpgradeSelection.GetRandomUpgradeTypeClass(_upgradeTypeClassesToChooseFrom, _upgradeSelectionCount);
            _upgradeTypeClassesToChooseFrom[index] = _randomUpgradeTypeClass;
            _compUpgradeManagers[index].SetUpgradeTypeClass(_randomUpgradeTypeClass);
            _compUpgradeManagers[index].SetDisplay(_randomUpgradeTypeClass);
            
            // _upgradebuttonManager[index] = _selectionButton.GetComponent<UpgradeButtonManager>();
            // _upgradebuttonManager[index]?.Initialize(_compCustomizerSO);
            // _upgradebuttonManager[index]?.GetRandomSelection(_compCustomizerSO, _upgradeButtonIndex, _upgradeSelectionCount);
            // _upgradeButtonIndex[index] = _upgradebuttonManager[index].GetRandomUpgradeSelection().GetRandomIndex();
        }
        private void CreateAllSelectionButtons()
        {
            for (int _index = 0; _index < _numberOfUpgrades; _index ++)
            {
                CreateSelectionButton(_index);
            }
        }
        public void CalculateUpgradeSelectionCount()
        {
            _upgradeSelectionCount = _compCustomizerSO.BasicUnitArray.Length + _compCustomizerSO.AdvancedUnitSelectionT1Array.Length + _compCustomizerSO.HatchEffectSelectionArray.Length;
        }
        public void OnDisable()
        {
            // for (int _index=0; _index < _upgradebuttonManager.Length; _index++)
            // {
            //     if (_upgradebuttonManager[_index] != null)
            //     {
            //         _upgradebuttonManager[_index]._onUpgradeButtonClicked -= UpgradeButtonClicked;
            //     }
            // }
        }
        public void OnEnable()
        {
            // SubscribeToUpgradeButtons();
        }
        public void SubscribeToUpgradeButtons()
        {
            if (_upgradebuttonManager != null)
            {
                for (int _index = 0; _index < _upgradebuttonManager.Length; _index ++)
                {
                    if (_upgradebuttonManager[_index] != null)
                    {
                        SubscribeToUpgradeButton(_upgradebuttonManager[_index]);
                    }
                }
            }
        }
        public void SubscribeToUpgradeButton(UpgradeButtonManager upgradeButtonManager)
        {
            upgradeButtonManager._onUpgradeButtonClicked -= UpgradeButtonClicked;
            upgradeButtonManager._onUpgradeButtonClicked += UpgradeButtonClicked;
        }
        public void UpgradeButtonClicked(UpgradeButtonManager upgradeButtonManager)
        {
            _onUpgradeButtonClicked?.Invoke(upgradeButtonManager);
        }

        // public void OnDrop(PointerEventData eventData)
        // {
        //     if (debugBool)
        //         Debug.Log($"selection ondrop called");
        //     GameObject dropped = eventData.pointerDrag;
        //     CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
        //     compUpgradeManager.ParentAfterDrag = transform;
        //     // _buttonDropped?.Invoke(compUpgradeManager);
        // }
    }
}
