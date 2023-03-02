using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class RandomUpgradeSelection : MonoBehaviour
    {
        private bool _debugBool = false;
        private SelectionIndexToSelection _selectionIndexToSelection;
        private UpgradeTypeClass _upgradeTypeClassToCompare;
        private int _randomIndex;        
        private int upgradeSelectionCount;
        private CheckForUTCDuplicates _checkForDuplicates;
        public void Initialize()
        {
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
            _checkForDuplicates = GetComponent<CheckForUTCDuplicates>();
        }
        public UpgradeTypeClass GetRandomUpgradeTypeClass(List<UpgradeTypeClass> upgradeTypeClassesCurrent, int selectionCount)
        {
            if (upgradeTypeClassesCurrent != null)
            {
                for (int i=0; i<upgradeTypeClassesCurrent.Count; i++)
                {
                    UpgradeTypeClass randomUpgradeClass = _selectionIndexToSelection.GetUpgradeTypeClassFromIndex(GetRandomNumber(selectionCount));
                    // if ((_checkForDuplicates != null) && (!_checkForDuplicates.IsDupliatesFunc(upgradeTypeClassesCurrent, randomUpgradeClass)))
                    // {
                    CheckIfDuplicates  (upgradeTypeClassesCurrent, ref randomUpgradeClass, selectionCount);
                    return randomUpgradeClass;
                    // }
                }
            }
            return null;
        }
        private int GetRandomNumber(int selectionCount)
        {
            return Random.Range(0, selectionCount);
        }
        private void IfDuplicateGetNewRandom(List<UpgradeTypeClass> upgradeTypeClassesCurrent, int selectionCount)
        {

        }
        private void CheckIfDuplicates(List<UpgradeTypeClass> upgradeTypeClassesCurrent, ref UpgradeTypeClass randomUpgradeClass, int selectionCount)
        {
            if (_debugBool)
            Debug.Log($"checking duplicates");
            for (int i=0; i<upgradeTypeClassesCurrent.Count; i++)
            {
                if (upgradeTypeClassesCurrent[i] != null)
                {
                    if (upgradeTypeClassesCurrent[i] == randomUpgradeClass)
                    {
                        randomUpgradeClass = _selectionIndexToSelection.GetUpgradeTypeClassFromIndex(GetRandomNumber(selectionCount));
                        CheckIfDuplicates(upgradeTypeClassesCurrent, ref randomUpgradeClass, selectionCount);
                        return;
                    }
                }
            }
        }
    }
}
