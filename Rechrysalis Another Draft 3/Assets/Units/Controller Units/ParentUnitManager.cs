using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ParentUnitManager : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        [SerializeField] private GameObject[] _subUnits;
        public GameObject[] SubUnits {get {return _subUnits;}set {_subUnits = value;}}
        [SerializeField] private GameObject[] _subChrysalii;
        public GameObject[] SubChrysalii {get{return _subChrysalii;}set {_subChrysalii = value;}}
        private PlayerUnitsSO _theseUnits;

        private bool _isStopped;
        public bool IsStopped 
        {
            set{
                _isStopped = value;
                foreach(GameObject _unit in _subUnits)
                {
                    _unit.GetComponent<UnitManager>().IsStopped = _isStopped;
                }
            }
         }

        public void Initialize(int _controllerIndex, int _parentUnitIndex, CompSO _unitComp, PlayerUnitsSO _theseUnits)
        {
            this._controllerIndex = _controllerIndex;
            this._theseUnits = _theseUnits;
            AddChrysalisActions();
        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            AddChrysalisActions();
        }
        private void AddChrysalisActions()
        {
            foreach (GameObject _chrysalis in _subChrysalii)
            {
                _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= ActivateUnit;
                _chrysalis.GetComponent<ChrysalisTimer>()._startUnit += ActivateUnit;
            }
        }
        private void OnDisable()
        {
            foreach (GameObject _chrysalis in _subChrysalii)
            {
                _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= ActivateUnit;
            }
        }
        public void ActivateUnit(int _unitIndex)
        {
            for (int _indexInSubUnits=0; _indexInSubUnits<_subUnits.Length; _indexInSubUnits++)
            {
                if (_indexInSubUnits == _unitIndex)  
                {
                    _subUnits[_unitIndex].SetActive(true);
                    if (!_theseUnits.ActiveUnits.Contains(_subUnits[_indexInSubUnits]))
                    {
                        _theseUnits.ActiveUnits.Add(_subUnits[_unitIndex]);
                    }
                }
                else 
                {
                    _subUnits[_indexInSubUnits].SetActive(false);   
                    if (_theseUnits.ActiveUnits.Contains(_subUnits[_indexInSubUnits]))
                    {
                        int _indexInActiveUnits = _theseUnits.ActiveUnits.IndexOf(_subUnits[_indexInSubUnits]);
                        _theseUnits.ActiveUnits.Remove(_theseUnits.ActiveUnits[_indexInActiveUnits]);
                    }                                     
                }   
                if (_subChrysalii[_unitIndex].active == true)
                {
                    _subChrysalii[_unitIndex].SetActive(false);
                }
                if (_theseUnits.ActiveUnits.Contains(_subChrysalii[_unitIndex]))
                {
                    _theseUnits.ActiveUnits.Remove(_subChrysalii[_unitIndex]);
                }        
            }
        }
    }
}
