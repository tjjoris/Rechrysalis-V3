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
        private SelectionInitializeOnlyOnHatchEffects _selectionInitializeOnlyHE;
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

            if ((!PlayerPrefsInteract.GetCustomizeOnlyHEAndUnit()) || (GetComponent<AddAllToSelection>()))
            {
                 _selectionInitialize = gameObject.AddComponent<SelectionInitialize>();
                 _selectionInitialize.Initialize(_compCustomizerSO, _draggedButtonHolder, _compSO, _compButtonPrefab, _selectionContainer);
            }
            else 
            {
                _selectionInitializeOnlyBasicUnit = gameObject.AddComponent<SelectionInitializeOnlyBasicUnit>();
                List<UpgradeTypeClass> basicButtonsToCreate = _selectionInitializeOnlyBasicUnit.GetButtons(_compCustomizerSO, 1);
                _instantiateButton.CreateSelectionButtons(basicButtonsToCreate, _selectionContainer);
                _selectionInitializeOnlyHE = gameObject.AddComponent<SelectionInitializeOnlyOnHatchEffects>();
                List<UpgradeTypeClass> onHatchEffectsToCreate = _selectionInitializeOnlyHE.GetButtons(_compCustomizerSO, 1);
                _instantiateButton.CreateSelectionButtons(onHatchEffectsToCreate, _selectionContainer);
            }
        }
    }
}
