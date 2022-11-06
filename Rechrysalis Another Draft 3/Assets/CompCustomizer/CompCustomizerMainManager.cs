using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.CompCustomizer;
using Rechrysalis.Unit;

namespace Rechrysalis
{
    public class CompCustomizerMainManager : MonoBehaviour
    {

        [SerializeField] private CompsAndUnitsSO _mainCompsAndUnits;
        [SerializeField] private CompCustomizerManager _compCustomizerManager;
        [SerializeField] private CompSO _compSO;
        [SerializeField] private Color _basicColour;
        [SerializeField] private Color _advColour;
        [SerializeField] private Color _hatchColour;
        private void Awake() 
        {
            _compCustomizerManager.Initialize(_compSO, _basicColour, _advColour, _hatchColour, _mainCompsAndUnits.Level);
        }
    }
}
