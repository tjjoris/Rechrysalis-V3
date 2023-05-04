using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    [RequireComponent(typeof(GetRandomUpgradeTypeClassesFromList))]
    public class SelectionInitializeOnlyBasicUnit : MonoBehaviour
    {
        private bool _debugBool = false;
        private int _numberOfupgrades;
        private List<UpgradeTypeClass> _chosenList;
        private List<UpgradeTypeClass> _listToChooseFrom;
        private CompCustomizerSO _compCustomizerSO;
        private GetRandomUpgradeTypeClassesFromList _getRandomUpgradeTypeClassesFromList;
        private void Awake()
        {
            _getRandomUpgradeTypeClassesFromList = GetComponent<GetRandomUpgradeTypeClassesFromList>();
        }
        public List<UpgradeTypeClass> GetButtons(CompCustomizerSO compCustomierSO, int maxChosen)
        {
            _getRandomUpgradeTypeClassesFromList = GetComponent<GetRandomUpgradeTypeClassesFromList>();
            _compCustomizerSO = compCustomierSO;
            _listToChooseFrom = new List<UpgradeTypeClass>();
            foreach (UnitStatsSO basicUnit in _compCustomizerSO.BasicUnitArray)
            {
                _listToChooseFrom.Add(basicUnit.UpgradeTypeClass);
            }
            List<UpgradeTypeClass> randomSelection = _getRandomUpgradeTypeClassesFromList.GetRandomListFunc(_listToChooseFrom, maxChosen);
            if (_debugBool) Debug.Log($"random seleciton " + randomSelection[0].GetUpgradeType());
            return randomSelection;
        }
        
    }
}
