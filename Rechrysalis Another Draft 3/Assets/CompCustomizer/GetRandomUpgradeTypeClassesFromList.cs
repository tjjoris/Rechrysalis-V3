// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class GetRandomUpgradeTypeClassesFromList : MonoBehaviour
    {
        public List<UpgradeTypeClass> GetRandomListFunc(List<UpgradeTypeClass> listToChooseFrom, int maxToChoose)
        {
            List<UpgradeTypeClass> chosenList = new List<UpgradeTypeClass>();
            int max = listToChooseFrom.Count;
            while ((chosenList.Count < maxToChoose) || (max > 0))
            {
                chosenList.Add(GetRandomFunc(listToChooseFrom, chosenList));
                max --;
            }
            return chosenList;
        }
        public UpgradeTypeClass GetRandomFunc(List<UpgradeTypeClass> listToChooseFrom, List<UpgradeTypeClass> chosenList)
        {
            int randomNumber = Random.Range(0, listToChooseFrom.Count -1);
            UpgradeTypeClass randomUTC = listToChooseFrom[randomNumber];
            if (chosenList.Contains(randomUTC))
            {
                randomUTC = GetRandomFunc(listToChooseFrom, chosenList);
            }
            return randomUTC;
        }
    }
}
