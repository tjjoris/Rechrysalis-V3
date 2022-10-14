using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;

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
        private GameObject _currentSubUnit;

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
            // AddChrysalisAndUnitActions();
        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            AddChrysalisAndUnitActions();
        }
        public void AddChrysalisAndUnitActions()
        {
            // foreach (GameObject _chrysalis in _subChrysalii)
            // {
            //     _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= ActivateUnit;
            //     _chrysalis.GetComponent<ChrysalisTimer>()._startUnit += ActivateUnit;
            // }
            // foreach (GameObject _unit in _subUnits)
            // {
            //     _unit.GetComponent<Rechrysalize>()._startChrysalis -= ActivateChrysalis;
            //     _unit.GetComponent<Rechrysalize>()._startChrysalis += ActivateChrysalis; 
            // }
            foreach (Transform _child in transform)
            {                
                ChrysalisTimer _chryslisTimer = _child.GetComponent<ChrysalisTimer>();
                if (_chryslisTimer != null)
                {
                    // Debug.Log($"chryslis timer " + _child);
                    _chryslisTimer._startUnit -= ActivateUnit;
                    _chryslisTimer._startUnit += ActivateUnit;
                }
                Rechrysalize _rechrysalize = _child.GetComponent<Rechrysalize>();
                if (_rechrysalize != null)
                {
                    // Debug.Log($"rechrysalize " + _child);
                    _rechrysalize._startChrysalis -= ActivateChrysalis;
                    _rechrysalize._startChrysalis += ActivateChrysalis;
                }
            }
        }
        private void OnDisable()
        {
            foreach (GameObject _chrysalis in _subChrysalii)
            {
                _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= ActivateUnit;
            }
            foreach (GameObject _unit in _subUnits)
            {
                _unit.GetComponent<Rechrysalize>()._startChrysalis -= ActivateChrysalis;
            }            
        }
        public void ActivateChrysalis(int _chrysalisIndex)
        {
            if (_currentSubUnit != _subChrysalii[_chrysalisIndex])
            {
            float _timeToKeep = 0;
            ChrysalisTimer _chrysalisTimer = _currentSubUnit.GetComponent<ChrysalisTimer>();
            if (_chrysalisTimer != null)
            {
                _timeToKeep = _chrysalisTimer.TimerCurrent;
            }
            // for (int _indexInSubChrysalis=0; _indexInSubChrysalis<_subChrysalii.Length; _indexInSubChrysalis++)
            {
                // if (_indexInSubChrysalis == _chrysalisIndex)
                {
                    _currentSubUnit = _subChrysalii[_chrysalisIndex];
                    Debug.Log($"activating chrysalis" + _chrysalisIndex);
                    _subChrysalii[_chrysalisIndex].SetActive(true);
                    _subChrysalii[_chrysalisIndex].GetComponent<ChrysalisTimer>()?.StartThisChrysalis(_timeToKeep);
                    if (!_theseUnits.ActiveUnits.Contains(_subChrysalii[_chrysalisIndex]))
                    {
                        _theseUnits.ActiveUnits.Add(_subChrysalii[_chrysalisIndex]);
                    }                    
                }
            }
            for (int _unitIndex = 0; _unitIndex < _subUnits.Length; _unitIndex ++)
            {
                DeactivateUnit(_unitIndex);
            }
            }
        }
        public void ActivateUnit(int _unitIndex)
        {
            for (int _indexInSubUnits=0; _indexInSubUnits<_subUnits.Length; _indexInSubUnits++)
            {
                if (_indexInSubUnits == _unitIndex)  
                {
                    _currentSubUnit = _subUnits[_unitIndex];
                    _subUnits[_unitIndex].SetActive(true);
                    _subUnits[_unitIndex].GetComponent<UnitManager>()?.RestartUnit();
                    if (!_theseUnits.ActiveUnits.Contains(_subUnits[_indexInSubUnits]))
                    {
                        _theseUnits.ActiveUnits.Add(_subUnits[_unitIndex]);
                    }
                }
                // else 
                // {
                //     _subUnits[_indexInSubUnits].SetActive(false);   
                //     if (_theseUnits.ActiveUnits.Contains(_subUnits[_indexInSubUnits]))
                //     {
                //         int _indexInActiveUnits = _theseUnits.ActiveUnits.IndexOf(_subUnits[_indexInSubUnits]);
                //         _theseUnits.ActiveUnits.Remove(_theseUnits.ActiveUnits[_indexInActiveUnits]);
                //     }                                     
                // }   
                DeactivateChrysalis(_indexInSubUnits);
                // if (_subChrysalii[_indexInSubUnits].active == true)
                // {
                //     _subChrysalii[_indexInSubUnits].SetActive(false);
                // }
                // if (_theseUnits.ActiveUnits.Contains(_subChrysalii[_indexInSubUnits]))
                // {
                //     _theseUnits.ActiveUnits.Remove(_subChrysalii[_indexInSubUnits]);
                // }        
            }
        }
        private void DeactivateChrysalis(int _chryslisIndex)
        {
            if (_subChrysalii[_chryslisIndex].activeInHierarchy == true)
            {
                _subChrysalii[_chryslisIndex].SetActive(false);
            }
            if (_theseUnits.ActiveUnits.Contains(_subChrysalii[_chryslisIndex]))
            {
                _theseUnits.ActiveUnits.Remove(_subChrysalii[_chryslisIndex]);
            }
        }
        private void DeactivateUnit(int _unitIndex)
        {
            if (_subUnits[_unitIndex].activeInHierarchy == true)
            {
                _subUnits[_unitIndex].SetActive(false);
            }
            if (_theseUnits.ActiveUnits.Contains(_subUnits[_unitIndex]))
            {
                _theseUnits.ActiveUnits.Remove(_subUnits[_unitIndex]);
            }
        }
    }
}
