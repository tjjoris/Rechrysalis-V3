using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class RechrysalisControllerInitialize : MonoBehaviour
    {
        [SerializeField] private GameObject _parentUnitPrefab;
        [SerializeField] private GameObject _childUnitPrefab;
        [SerializeField] private GameObject _unitRing;
        [SerializeField] private float _ringDistFromCentre = 2f;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits {get{return _parentUnits;}}
        private List<GameObject> _allUnits;        
        public void Initialize(int _controllerIndex, CompSO _unitComp, CompsAndUnitsSO _compsAndUnits)
        {
            _allUnits = new List<GameObject>();
            _allUnits.Clear();        
            _parentUnits = new GameObject[_unitComp.ParentUnitCount];
            // foreach (GameObject _unit in _parentUnits)
            for (int _parentUnitIndex = 0; _parentUnitIndex < _unitComp.ParentUnitCount; _parentUnitIndex++)
            {       
                float _radToOffset = Mathf.Deg2Rad * (((360f / 3f) * _parentUnitIndex) + 90);  
                Vector3 _unitOffset = new Vector3 (Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, 0f);
                Debug.Log($"radtooffset" + _radToOffset + "vector 3 " + _unitOffset);
                GameObject go = Instantiate(_parentUnitPrefab, _unitRing.transform);
                go.transform.localPosition = _unitOffset;
                _parentUnits[_parentUnitIndex] = go;
                go.name = "Parent Unit " + _parentUnitIndex.ToString();
                ParentUnitManager _pum = go.GetComponent<ParentUnitManager>();
                _pum?.Initialize(_controllerIndex, _parentUnitIndex, _unitComp);                        
                for (int _childUnitIndex = 0; _childUnitIndex < _unitComp.ChildUnitCount; _childUnitIndex++)
                {
                    GameObject childGo = Instantiate(_childUnitPrefab, go.transform);
                    childGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _unitComp.UnitSOArray[(_parentUnitIndex * _unitComp.ParentUnitCount) + (_childUnitIndex)], _compsAndUnits);
                    _pum.SubUnits[_childUnitIndex] = childGo;
                    childGo.name = $"Child Unit " + _childUnitIndex;
                    _allUnits.Add(childGo);
                }
                _pum.ActivateUnit(0);
            }            
        }
        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
        // private void SetBasicStartingUnits()
        // {
        //     foreach (GameObject _parentUnit in _parentUnits)
        //     {
        //         ParentUnitManager _parentUnitManager = _parentUnit.GetComponent<ParentUnitManager>();
        //         _parentUnitManager?.ActivateUnit(0);
        //     }
        // }
    }
}
