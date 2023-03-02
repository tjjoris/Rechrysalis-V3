using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CheckForUTCDuplicates : MonoBehaviour
    {
        public bool IsDupliatesFunc(List<UpgradeTypeClass> upgradetypeClassesList, UpgradeTypeClass upgradeTypeClass)
        {
            for (int i=0; i < upgradetypeClassesList.Count; i++)
            {
                if ((upgradetypeClassesList[i] != null) && (upgradetypeClassesList[i] == upgradeTypeClass))
                {
                    return true;
                }
            }
            return false;
        }
        // private void CheckIfDuplicates(List<UpgradeTypeClass> upgradeTypeClassesCurrent, ref UpgradeTypeClass randomUpgradeClass, int selectionCount)
        // {
        //     if (_debugBool)
        //         Debug.Log($"checking duplicates");
        //     for (int i = 0; i < upgradeTypeClassesCurrent.Count; i++)
        //     {
        //         if (upgradeTypeClassesCurrent[i] != null)
        //         {
        //             if (upgradeTypeClassesCurrent[i] == randomUpgradeClass)
        //             {
        //                 randomUpgradeClass = _selectionIndexToSelection.GetUpgradeTypeClassFromIndex(GetRandomNumber(selectionCount));
        //                 CheckIfDuplicates(upgradeTypeClassesCurrent, ref randomUpgradeClass, selectionCount);
        //                 return;
        //             }
        //         }
        //     }
        // }
    }
}
