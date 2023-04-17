using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.UI;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionInitializeMain : MonoBehaviour
    {
        [SerializeField] private GameObject _compButtonPrefab;
        [SerializeField] private Transform _selectionContainer;
        private Transform _draggedButtonHolder;
        private SelectionInitialize _selectionInitialize;
        private SelectionInitializeOnlyBasicUnit _selectionInitializeOnlyBasicUnit;
        public SelectionInitialize SelectionInitialize => _selectionInitialize;
        private CompCustomizerSO _compCustomizerSO;
        private CompSO _compSO;
        private MainCompCustomizerManager _mainCompCustomizerManager;
        private InstantiateButton _instantiateButton;
        // private GetRandomUpgradeTypeClassesFromList _getRandomUpgradeTypeClassesFromList;

        public void Initialize(CompCustomizerSO compCustomizerSO, Transform draggedButtonHolder, CompSO compSO, MainCompCustomizerManager mainCompCustomizerManager)
        {
            _mainCompCustomizerManager = mainCompCustomizerManager;
            _compCustomizerSO = compCustomizerSO;
            _draggedButtonHolder = draggedButtonHolder;
            _compSO = compSO;
            _instantiateButton = _mainCompCustomizerManager.InstantiateButton;

            if (!PlayerPrefsInteract.GetCustomizeOnlyHEAndUnit())
            {
                 _selectionInitialize = gameObject.AddComponent<SelectionInitialize>();
                 _selectionInitialize.Initialize(_compCustomizerSO, _draggedButtonHolder, _compSO, _compButtonPrefab, _selectionContainer);
            }
            else 
            {
                _selectionInitializeOnlyBasicUnit = gameObject.AddComponent<SelectionInitializeOnlyBasicUnit>();
                List<UpgradeTypeClass> basicButtonsToCreate = _selectionInitializeOnlyBasicUnit.GetButtons(_compCustomizerSO, 1);
                InstantiateAllButtonsInList(basicButtonsToCreate);
            }
        }
        private void InstantiateAllButtonsInList(List<UpgradeTypeClass> buttonsInList)
        {
            foreach (UpgradeTypeClass upgradeTypeClass in buttonsInList)
            {
                _instantiateButton.CreateSelectionButtons(buttonsInList, _selectionContainer);
            }
        }
    }
}
