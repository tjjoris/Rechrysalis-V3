using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class RechrysalisControllerInitialize : MonoBehaviour
    {
        [SerializeField] private GameObject _parentUnitPrefab;
        [SerializeField] private GameObject _unitRing;
        [SerializeField] private float _ringDistFromCentre = 2f;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits {get{return _parentUnits;}}
        public void Initialize(CompSO _unitComp)
        {
            // foreach (GameObject _unit in _parentUnits)
            for (int _parentUnitIndex = 0; _parentUnitIndex < 1; _parentUnitIndex++)
            {       
                // for (int _childUnitIndex =0; _childUnitIndex < 3; _childUnitIndex++)
                // {      
                        // _unit.GetComponent<ParentUnitManager>()?.Initialize();
                        GameObject go = Instantiate(_parentUnitPrefab, _unitRing.transform);
                        go.GetComponent<ParentUnitManager>()?.Initialize(_parentUnitIndex, _unitComp);
                // }                
            }
        }
    }
}
