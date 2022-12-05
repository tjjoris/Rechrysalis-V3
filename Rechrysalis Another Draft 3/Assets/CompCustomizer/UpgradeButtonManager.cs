using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UpgradeButtonManager", menuName = "CompCustomizer/UpgradeButtonManager")]

    public class UpgradeButtonManager : MonoBehaviour
    {
        [SerializeField] private UnitStatsSO _unitStatsSO;
        public UnitStatsSO UnitStatsSO { get{ return _unitStatsSO; } set{ _unitStatsSO = value; } }
        [SerializeField] private HatchEffectSO _hatchEffectSO;
        public HatchEffectSO HatchEffectSO { get{ return _hatchEffectSO; } set{ _hatchEffectSO = value; } }
        private RandomUpgradeSelection _randomUpgradeSelection;
        
        public void GetRandomSelection(CompCustomizerSO _compCustomizerSO, int[] _upgradeSelectionIndex, int _upgradeSelectionCount)
        {
            _randomUpgradeSelection = GetComponent<RandomUpgradeSelection>();
            _randomUpgradeSelection.GetRandomSelection(_compCustomizerSO, _upgradeSelectionIndex, _upgradeSelectionCount);
        }
        public RandomUpgradeSelection GetRandomUpgradeSelection()
        {
            return _randomUpgradeSelection;
        }
    }
}
