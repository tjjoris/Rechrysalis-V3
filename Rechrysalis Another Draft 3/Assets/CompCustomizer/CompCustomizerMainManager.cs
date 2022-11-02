using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.CompCustomizer;
using Rechrysalis.Unit;

namespace Rechrysalis
{
    public class CompCustomizerMainManager : MonoBehaviour
    {
        [SerializeField] private CompCustomizerManager _compCustomizerManager;
        [SerializeField] private CompSO _compSO;
        private void Awake() 
        {
            _compCustomizerManager.Initialize(_compSO);
        }
    }
}
