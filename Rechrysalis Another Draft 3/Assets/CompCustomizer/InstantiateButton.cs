using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class InstantiateButton : MonoBehaviour
    {
        //this is a new script and only being used by the selection inititalize for 
        //hatch effects and units only
        [SerializeField] private GameObject _compButtonPrefab;
        [SerializeField] private Transform _draggedButtonHolder;
        
        public void CreateSelectionButtons(List<UpgradeTypeClass> buttonsToCreate, Transform parentTransform)
        {
            foreach (UpgradeTypeClass upgradeTypeClass in buttonsToCreate)
            {
                CreateSelectionButton(upgradeTypeClass, parentTransform);
            }
        }
        public void CreateSelectionButton(UpgradeTypeClass buttonToCreate, Transform parentTransform)
        {            
            GameObject _selectionButton = Instantiate(_compButtonPrefab, parentTransform);
            CompUpgradeManager compUpgradeManager = _selectionButton.GetComponent<CompUpgradeManager>();
            compUpgradeManager.Initialize(_draggedButtonHolder);
            // UpgradeTypeClass _randomUpgradeTypeClass = _randomUpgradeSelection.GetRandomUpgradeTypeClass(_upgradeTypeClassesToChooseFrom, _upgradeSelectionCount);
            // _upgradeTypeClassesToChooseFrom[index] = _randomUpgradeTypeClass;
            compUpgradeManager.SetUpgradeTypeClass(buttonToCreate);
            compUpgradeManager.SetDisplay(buttonToCreate);
        }
    }
}
