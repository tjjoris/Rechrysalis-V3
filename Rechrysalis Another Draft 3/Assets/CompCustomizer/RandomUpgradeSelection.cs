using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class RandomUpgradeSelection : MonoBehaviour
    {
        private int _randomIndex;        
        private int upgradeSelectionCount;
        public int GetRandomSelection(CompCustomizerSO _compCustomizerSO, int[] _upgradeSelectionIndex, int _upgradeSelectionCount)
        {
            upgradeSelectionCount = _upgradeSelectionCount;
            GetRandomNumber();
            CheckIfDuplicate(_upgradeSelectionIndex, upgradeSelectionCount);
            return _randomIndex;
            
        }
        private void GetRandomNumber()
        {
            _randomIndex = Random.Range(0, upgradeSelectionCount);
        }
        private void CheckIfDuplicate(int[] _upgradeSelectionIndex, int _upgradeSelectionCount)
        {
            // bool isDuplicate = false;
            for (int i = 0; i < _upgradeSelectionIndex.Length; i++)
            {
                if (_upgradeSelectionIndex[i] == _randomIndex)
                {
                    // isDuplicate = true;
                    GetRandomNumber();
                    CheckIfDuplicate(_upgradeSelectionIndex, _upgradeSelectionCount);
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
