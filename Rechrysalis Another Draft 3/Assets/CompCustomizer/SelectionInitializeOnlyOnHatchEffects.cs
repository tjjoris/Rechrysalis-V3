using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    [RequireComponent(typeof(GetRandomUpgradeTypeClassesFromList))]

    public class SelectionInitializeOnlyOnHatchEffects : MonoBehaviour
    {
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
            foreach (AdvUnitModifierSO onHatchEffect in _compCustomizerSO.OnHatchEffectSelectionArray)
            {
                _listToChooseFrom.Add(onHatchEffect.UpgradeTypeClass);
            }
            foreach (HatchEffectSO hatchEffect in _compCustomizerSO.HatchEffectSelectionArray)
            {
                _listToChooseFrom.Add(hatchEffect.UpgradeTypeClass);
            }
            List<UpgradeTypeClass> randomSelection = _getRandomUpgradeTypeClassesFromList.GetRandomListFunc(_listToChooseFrom, maxChosen);
            return randomSelection;
        }
    }
}
