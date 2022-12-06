using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class CompInitialize : MonoBehaviour
    {
        private CompCustomizerSO _compCustomizerSO;

        public void Initialize(CompCustomizerSO compCustomizerSO)
        {
            _compCustomizerSO = compCustomizerSO;
        }
    }
}
