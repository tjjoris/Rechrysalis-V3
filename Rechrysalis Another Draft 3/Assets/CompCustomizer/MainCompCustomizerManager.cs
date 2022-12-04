using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class MainCompCustomizerManager : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        public CompsAndUnitsSO CompsAndUnitsSO { get{ return _compsAndUnitsSO; } set{ _compsAndUnitsSO = value; } }
        [SerializeField] private SelectionInitialize _selectionInitialize;
        
        [SerializeField] private CompSO _compSO;
        public CompSO CompSO { get{ return _compSO; } set{ _compSO = value; } }
        
        private void Start()
        {
            _selectionInitialize.Initialize(_compCustomizerSO);
        }
    }
}
