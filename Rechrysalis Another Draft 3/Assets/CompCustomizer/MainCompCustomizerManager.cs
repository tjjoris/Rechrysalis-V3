using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class MainCompCustomizerManager : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        public CompsAndUnitsSO CompsAndUnitsSO { get{ return _compsAndUnitsSO; } set{ _compsAndUnitsSO = value; } }
        [SerializeField] private SelectionInitialize _selectionInitialize;
        [SerializeField] private CompInitialize _compInitialize;
        
        [SerializeField] private CompSO _compSO;
        public CompSO CompSO { get{ return _compSO; } set{ _compSO = value; } }
        private CompSO _changedComp;
        [SerializeField] private UpgradeButtonManager _upgradeButtonManager;
        [SerializeField] private CompUpgradeManager _compUpgradeManager;
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        [SerializeField] private int _upgradeIndex;
        [SerializeField] private bool _debugBool = true;
        
        private void OnEnable()
        {
            SubscribeToButtons();
        }
        private void OnDisable()
        {
            _selectionInitialize._onUpgradeButtonClicked -= SelectorButtonClicked;
            _compInitialize._onCompUpgradeClicked -= CompButtonClicked;
        }
        private void SubscribeToButtons()
        {
            if (_selectionInitialize != null)
            {
                _selectionInitialize._onUpgradeButtonClicked -= SelectorButtonClicked;
                _selectionInitialize._onUpgradeButtonClicked += SelectorButtonClicked;
            }
            if (_compInitialize != null)
            {
                _compInitialize._onCompUpgradeClicked -= CompButtonClicked;
                _compInitialize._onCompUpgradeClicked += CompButtonClicked;
            }
        }
        private void Start()
        {
            _compSO = _compsAndUnitsSO.CompsSO[0];
            ResetChangedComp();
            _selectionInitialize.Initialize(_compCustomizerSO);
            _compInitialize.Initialize(_compCustomizerSO, _compsAndUnitsSO.CompsSO[0]);
        }
        private void ResetChangedComp()
        {
            _changedComp = _compSO;
        }
        private void SelectorButtonClicked(UpgradeButtonManager upgradeButtonManager)
        {
            if (_debugBool)
            Debug.Log($"selector button clicked");
            _upgradeButtonManager = upgradeButtonManager;
            CheckIfCompToChange();
        }
        private void CompButtonClicked(CompUpgradeManager compUpgradeManager)
        {
            if (_debugBool)
            Debug.Log($"comp button clicked");
            _compUpgradeManager = compUpgradeManager;
            CheckIfCompToChange();
        }
        private void CheckIfCompToChange()
        {
            if ((_upgradeButtonManager != null) && (_compUpgradeManager != null))
            {
                if (_upgradeTypeClass == null)
                {
                    _upgradeTypeClass = new UpgradeTypeClass();
                }
                _upgradeTypeClass = _upgradeButtonManager.GetSelectionIndexToSelection().GetUpgradeTypeClass();
                ChangeUnitInComp(_compUpgradeManager.ParentIndex, 0, _upgradeTypeClass);
                _upgradeButtonManager = null;
                _compUpgradeManager = null;
            }
        }
        private void ChangeUnitInComp(int parentIndex, int childIndex, UpgradeTypeClass upgradeTypeClass)
        {
            // _compInitialize.SetCompUpgradeDisplay(parentIndex, childIndex, upgradeTypeClass);
        }
    }
}
