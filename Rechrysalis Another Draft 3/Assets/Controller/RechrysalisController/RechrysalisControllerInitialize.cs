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
        [SerializeField] private GameObject _chrysalisPrefab;
        [SerializeField] private GameObject _unitRing;
        [SerializeField] private float _ringDistFromCentre = 2f;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits {get{return _parentUnits;}}
        private List<GameObject> _allUnits;    
        private PlayerUnitsSO _theseUnits;    
        private float _unitRingOutRadius;
        private float _unitRingAngle = 90f;
        public void Initialize(int _controllerIndex, CompSO _unitComp, CompsAndUnitsSO _compsAndUnits, UnitRingManager _unitRingManager, HilightRingManager _hilightRingManager, UpgradeRingManager _upgradeRingManager, float _unitRingOuterRadius)
        {
            _allUnits = new List<GameObject>();
            _allUnits.Clear();                    
            _parentUnits = new GameObject[_unitComp.ParentUnitCount];
            _theseUnits = _compsAndUnits.PlayerUnits[_controllerIndex];
            _theseUnits.ActiveUnits = new List<GameObject>();
            _theseUnits.ActiveUnits.Clear();
            // foreach (GameObject _unit in _parentUnits)
            for (int _parentUnitIndex = 0; _parentUnitIndex < _unitComp.ParentUnitCount; _parentUnitIndex++)
            {       
                float _radToOffset = Mathf.Deg2Rad * (((360f / _unitComp.ParentUnitCount) * _parentUnitIndex) + _unitRingAngle);  
                Vector3 _unitOffset = new Vector3 (Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, 0f);
                // Debug.Log($"radtooffset" + _radToOffset + "vector 3 " + _unitOffset);
                GameObject parentUnitGO = Instantiate(_parentUnitPrefab, _unitRing.transform);
                parentUnitGO.transform.localPosition = _unitOffset;
                _parentUnits[_parentUnitIndex] = parentUnitGO;
                parentUnitGO.name = "Parent Unit " + _parentUnitIndex.ToString();
                ParentUnitManager _pum = parentUnitGO.GetComponent<ParentUnitManager>();
                _pum?.Initialize(_controllerIndex, _parentUnitIndex, _unitComp, _compsAndUnits.PlayerUnits[_controllerIndex], transform);                        
                _pum.SubUnits = new GameObject[_unitComp.UpgradeCountArray[_parentUnitIndex]];
                _pum.SubChrysalii = new GameObject[_unitComp.UpgradeCountArray[_parentUnitIndex]];
                for (int _childUnitIndex = 0; _childUnitIndex < _unitComp.UpgradeCountArray[_parentUnitIndex]; _childUnitIndex++)
                {
                    GameObject childUnitGo = Instantiate(_childUnitPrefab, parentUnitGO.transform);
                    UnitStatsSO _unitStats = _unitComp.UnitSOArray[(_parentUnitIndex * 3) + (_childUnitIndex)];
                    Debug.Log($"name " + (_unitStats.UnitName));
                    _unitStats.Initialize();
                    childUnitGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _unitStats, _compsAndUnits);
                    _pum.SubUnits[_childUnitIndex] = childUnitGo;
                    childUnitGo.name = $"Child Unit " + _childUnitIndex;
                    _allUnits.Add(childUnitGo);
                    // _theseUnits.ActiveUnits.Add(childUnitGo);
                    childUnitGo.SetActive(false);
                    GameObject chrysalisGo = Instantiate(_chrysalisPrefab, parentUnitGO.transform);
                    chrysalisGo.name = $"Chrysalis " + _childUnitIndex;
                    // chrysalisGo.GetComponent<ChrysalisManager>()?.Initialize(_unitStats.ChrysalisTimerMax, childUnitGo);
                    chrysalisGo.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _compsAndUnits.Chrysalis, _compsAndUnits);
                    chrysalisGo.GetComponent<ChrysalisTimer>()?.Initialize(_unitStats.ChrysalisTimerMax, _childUnitIndex);
                    _pum.SubChrysalii[_childUnitIndex] = chrysalisGo;                    
                    _allUnits.Add(chrysalisGo);                  
                    chrysalisGo.SetActive(false);  
                }
                _pum.AddChrysalisAndUnitActions();
                _pum.ActivateUnit(0);
            }
            _hilightRingManager?.Initialize(_unitRingManager);
            _unitRingManager?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ParentUnitCount, _parentUnits, _unitRingAngle, _hilightRingManager.transform);  
            _upgradeRingManager?.Initialize(_unitRingAngle);        
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
