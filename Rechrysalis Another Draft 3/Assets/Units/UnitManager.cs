using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _subUnits;
        public GameObject[] SubUnits {get {return _subUnits;}set {_subUnits = value;}}

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
