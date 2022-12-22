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
        [SerializeField] private CompInitialize _compInitialize;
        
        [SerializeField] private CompSO _compSO;
        public CompSO CompSO { get{ return _compSO; } set{ _compSO = value; } }
        [SerializeField] private bool _debugBool = true;
        [SerializeField] private Transform _movingButtonHolder;
        private void Start()
        {
            _compSO = _compsAndUnitsSO.CompsSO[0];
            _selectionInitialize.Initialize(_compCustomizerSO, _movingButtonHolder.transform);
            _compInitialize.Initialize(_compCustomizerSO, _compsAndUnitsSO.CompsSO[0], _movingButtonHolder);
        }
        public void ContinueClicked()
        {
            if (_compInitialize.CheckIfCanContinue())
            {
                Debug.Log($"continue");
                SetComp();
            }
        }
        private void SetComp()
        {
            _compSO.ParentUnitClassList.Clear();
            for (int i=0; i<_compInitialize.VerticalManagers.Count; i++)
            {                
            _compSO.ParentUnitClassList.Add(_compInitialize.VerticalManagers[i].GetParentUnitClass());
            }
        }
    }
}
