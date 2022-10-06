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

        public void Initialize(int _controllerIndex, int _parentUnitIndex, CompSO _unitComp)
        {
            this._controllerIndex = _controllerIndex;
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
