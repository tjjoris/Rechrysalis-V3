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

        public void Initialize(CompCustomizerSO compCustomizerSO, CompSO playerComp, GameObject compButtonPrefab)
        {
            _compCustomizerSO = compCustomizerSO;  

        }
        private void LoopVerticals(CompSO playerComp, GameObject compButtonPrefab)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {

            }
        }
        private void SetUpEachVertical(CompSO playerComp, GameObject compButtonPrefab, int parentIndex)
        {
            _verticalMangers[parentIndex]?.Initialize(playerComp, parentIndex, compButtonPrefab);
        }
    }
}
