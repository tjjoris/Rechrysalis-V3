using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.CompCustomizer;

namespace Rechrysalis
{
    public class CompCustomizerMainManager : MonoBehaviour
    {
        [SerializeField] private CompCustomizerManager _compCustomizerManager;
        private void Awake() 
        {
            _compCustomizerManager.Initialize();
        }
    }
}
