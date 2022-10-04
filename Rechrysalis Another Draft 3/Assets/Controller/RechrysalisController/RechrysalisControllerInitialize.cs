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
        public void Initialize(int _controllerIndex, CompSO _unitComp)
        {
            // foreach (GameObject _unit in _parentUnits)
            for (int _parentUnitIndex = 0; _parentUnitIndex < 1; _parentUnitIndex++)
            {       
                        float _radToOffset = Mathf.Deg2Rad * (((360f / 3f) * _parentUnitIndex) + 90);  
                        Vector3 _unitOffset = new Vector3 (Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, 0f);
                Debug.Log($"radtooffset" + _radToOffset + "vector 3 " + _unitOffset);
                GameObject go = Instantiate(_parentUnitPrefab, _unitRing.transform);
                        go.transform.localPosition = _unitOffset;
                        go.name = "Parent Unit " + _parentUnitIndex.ToString();
                        go.GetComponent<ParentUnitManager>()?.Initialize(_controllerIndex, _parentUnitIndex, _unitComp);
            }
        }
    }
}
