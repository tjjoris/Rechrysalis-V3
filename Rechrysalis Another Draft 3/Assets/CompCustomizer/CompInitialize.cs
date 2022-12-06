using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CompInitialize : MonoBehaviour
    {
        private CompCustomizerSO _compCustomizerSO;
        [SerializeField] private GameObject _compButtonPrefab;
        [SerializeField] private CompVerticalManager[] _verticalMangers;

        public void Initialize(CompCustomizerSO compCustomizerSO, CompSO playerComp)
        {
            _compCustomizerSO = compCustomizerSO;  
            LoopVerticals(playerComp);
        }
        private void LoopVerticals(CompSO playerComp)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {
                SetUpEachVertical(playerComp, parentIndex);
            }
        }
        private void SetUpEachVertical(CompSO playerComp, int parentIndex)
        {
            _verticalMangers[parentIndex]?.Initialize(playerComp, parentIndex, _compButtonPrefab);
        }
    }
}
