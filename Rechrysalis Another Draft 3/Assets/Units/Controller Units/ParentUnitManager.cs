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

        public void Initialize(int _controllerIndex, int _parentUnitIndex, CompSO _unitComp)
        {
            this._controllerIndex = _controllerIndex;
            for (int i=0; i<_subUnits.Length; i++)
            {
                UnitManager _unitManager = _subUnits[i].GetComponent<UnitManager>();
                int _unitIndex = (_parentUnitIndex * 3) + i;
                if (_unitComp.UnitSOArray[_unitIndex] != null)
                _unitManager?.Initialize(_controllerIndex, _unitComp.UnitSOArray[_unitIndex]);
                // _unitManager
            }
        }
        public void ActivateUnit(int _unitIndex)
        {
            for (int i=0; i<_subUnits.Length; i++)
            {
                if (i == _unitIndex)  
                {
                    _subUnits[_unitIndex].SetActive(true);
                }
                else 
                {
                    _subUnits[i].SetActive(false);
                }
            }
        }
    }
}
