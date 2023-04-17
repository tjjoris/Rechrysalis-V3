using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionInitializeOnlyBasicUnit : MonoBehaviour
    {
        private int _numberOfupgrades;
        private List<UpgradeTypeClass> _chosenList;
        private List<UpgradeTypeClass> _listToChooseFrom;
        private CompCustomizerSO _compCustomizerSO;

        public List<UpgradeTypeClass> GetButtons(CompCustomizerSO compCustomierSO, int maxChosen)
        {
            _compCustomizerSO = compCustomierSO;
            _listToChooseFrom = new List<UpgradeTypeClass>();
            foreach (UnitStatsSO basicUnit in _compCustomizerSO.BasicUnitArray)
            {
                _listToChooseFrom.Add(basicUnit.UpgradeTypeClass);
            }

            return _listToChooseFrom;
        }
    }
}
