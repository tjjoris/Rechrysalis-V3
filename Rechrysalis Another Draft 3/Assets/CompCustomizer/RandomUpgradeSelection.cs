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
        private UpgradeTypeClass _upgradeTypeClassToCompare;
        private int _randomIndex;        
        private int upgradeSelectionCount;
        private bool _debugBool = false;
        public void Initialize()
        {
            _selectionIndexToSelection = GetComponent<SelectionIndexToSelection>();
        }
        public int GetRandomSelection(CompCustomizerSO compCustomizerSO, int[] upgradeSelectionIndexArray, int selectionCount)
        {
            GetRandomNumberAndUpgradeClass(selectionCount);
            CheckIfDuplicate(upgradeSelectionIndexArray, selectionCount);
            return _randomIndex;
            
        }
        private void GetRandomNumberAndUpgradeClass(int selectionCount)
        {
            _randomIndex = Random.Range(0, selectionCount);
            if (_debugBool)
            Debug.Log($"random number " + _randomIndex);
            _upgradeTypeClassToCompare = _selectionIndexToSelection.GetUpgradeTypeClassFromIndex(_randomIndex);
        }
        private void CheckIfDuplicate(int[] upgradeSelectionIndexArray, int selectionCount)
        {
            // _selectionIndexToSelection.UpgradeFromIndex(_randomIndex);
            
            HatchEffectSO hatchEffectSOToCompare = null;
            UnitStatsSO unitStatsSOToCompare = null;
            UpgradeTypeClass.UpgradeType upgradeTypeToCompare = UpgradeTypeClass.UpgradeType.Error;
            // bool isDuplicate = false;
            for (int i = 0; i < upgradeSelectionIndexArray.Length; i++)
            {
                // if (_upgradeSelectionIndex[i] == _randomIndex)
                // if (_selectionIndexToSelection.GetThisUpgradeType() == UpgradeTypeClass.UpgradeType.HatchEffect) 
                // {
                _selectionIndexToSelection.GetUpgradeTypeWithoutChanging(ref upgradeSelectionIndexArray[i], ref unitStatsSOToCompare, ref hatchEffectSOToCompare, ref upgradeTypeToCompare);
                if (_upgradeTypeClassToCompare.GetUpgradeType() == upgradeTypeToCompare)
                {
                    if ((upgradeTypeToCompare == UpgradeTypeClass.UpgradeType.Basic) || (upgradeTypeToCompare == UpgradeTypeClass.UpgradeType.Advanced))
                    {
                        if (unitStatsSOToCompare == _upgradeTypeClassToCompare.GetUnitStatsSO())
                        {
                            DuplicateFoundGetNew(selectionCount, upgradeSelectionIndexArray);
                        }
                    }
                    if (upgradeTypeToCompare == UpgradeTypeClass.UpgradeType.HatchEffect)
                    {
                        if (hatchEffectSOToCompare == _upgradeTypeClassToCompare.GetHatchEffectSO())
                        {
                            DuplicateFoundGetNew(selectionCount, upgradeSelectionIndexArray);
                        }
                    }
                }
                    // (_selectionIndexToSelection.GetHatchEffectSO() == _selectionIndexToSelection.GetUpgradeTypeWithoutChanging(_randomIndex, unitStatsSOToCompare, hatchEffectSOToCompare, upgradeTypeToCompare));
            // }
                // {
                //     // isDuplicate = true;
                //     GetRandomNumber(selectionCount);
                //     CheckIfDuplicate(_upgradeSelectionIndex, selectionCount);
                //     return;
                // }
            }
        }
        private void DuplicateFoundGetNew(int selectionCount, int[] upgradeSelectionIndex)
        {
            if (_debugBool)
            Debug.Log($"duplicate found");
            GetRandomNumberAndUpgradeClass(selectionCount);
            CheckIfDuplicate(upgradeSelectionIndex, selectionCount);
            return;
        }
        public int GetRandomIndex()
        {
            return _randomIndex;            
        }
    }
}
