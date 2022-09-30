using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class FreeEnemyControllerManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _unitsList;
        public List<GameObject> UnitsList {get{return _unitsList;} set{_unitsList = value;}}
        [SerializeField] private PlayerUnitsSO[] _unitListSOArray;
        public PlayerUnitsSO[] UnitListSoArray {get{return _unitListSOArray;}set{_unitListSOArray = value;}}
        [SerializeField] private FreeUnitCompSO _freeEnemyComp;
        public FreeUnitCompSO FreeUnitComp{get{return _freeEnemyComp;}set{_freeEnemyComp = value;}}
        [SerializeField] private GameObject _freeEnemyPrefab;
        [SerializeField] private FreeUnitLayoutSO _freeEnemeyCompLayout;

        private void Awake() {
            Initialize();
        }
        public void Initialize()
        {
            for (int i=0; i<_freeEnemyComp.UnitSOArray.Length;i++)
            {
                if (_freeEnemyComp.UnitSOArray[i] != null)
                {
                    Vector3 _newUnitPos = _freeEnemeyCompLayout.UnitPos[0,i];
                    GameObject newFreeEnemy = Instantiate(_freeEnemyPrefab, _newUnitPos, Quaternion.identity, gameObject.transform);
                }
            }
        }
        
    }
}
