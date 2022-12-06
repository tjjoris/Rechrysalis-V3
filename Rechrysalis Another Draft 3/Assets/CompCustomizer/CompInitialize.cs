using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class CompInitialize : MonoBehaviour
    {
        private CompCustomizerSO _compCustomizerSO;
        [SerializeField] private GameObject _compButtonPrefab;
        [SerializeField] private GameObject[] _verticalGOs;

        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _compCustomizerSO = compCustomizerSO;
        }
    }
}
