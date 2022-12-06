using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class RandomUpgradeSelection : MonoBehaviour
    {
        private SelectionIndexToSelection _selectionIndexToSelection;
        private int _randomIndex;        
        private int upgradeSelectionCount;
        public void Initialize()
        {
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
        }
        public int GetRandomSelection(CompCustomizerSO _compCustomizerSO, int[] _upgradeSelectionIndex, int selectionCount)
        {
            GetRandomNumber(selectionCount);
            CheckIfDuplicate(_upgradeSelectionIndex, selectionCount);
            return _randomIndex;
            
        }
        private void GetRandomNumber(int selectionCount)
        {
            _randomIndex = Random.Range(0, selectionCount);
        }
        private void CheckIfDuplicate(int[] _upgradeSelectionIndex, int selectionCount)
        {
            _selectionIndexToSelection.UpgradeFromIndex(_randomIndex);
            HatchEffectSO hatchEffectSOToCompare = null;
            UnitStatsSO unitStatsSOToCompare = null;
            UpgradeTypeClass.UpgradeType upgradeTypeToCompare = UpgradeTypeClass.UpgradeType.Error;
            // bool isDuplicate = false;
            for (int i = 0; i < _upgradeSelectionIndex.Length; i++)
            {
                // if (_upgradeSelectionIndex[i] == _randomIndex)
                if ((_selectionIndexToSelection.GetThisUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect) && (_selectionIndexToSelection.GetHatchEffectSO == _selectionIndexToSelection.GetUpgradeTypeWithoutChanging(_randomIndex )))
                {
                    // isDuplicate = true;
                    GetRandomNumber(selectionCount);
                    CheckIfDuplicate(_upgradeSelectionIndex, selectionCount);
                    return;
                }
            }
        }
        public int GetRandomIndex()
        {
            return _randomIndex;
        }
    }
}
