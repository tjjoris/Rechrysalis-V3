using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;

namespace Rechrysalis.Controller
{
    public class FreeEnemyInitialize : MonoBehaviour
    {
        [SerializeField] private FreeUnitLayoutSO _freeEnemyCompLayout;
        [SerializeField] private GameObject _FreeUnitPrefab;
        // private int _controllerIndex;
        private List<GameObject> _allUnits;
        public void Initialize(int _controllerIndex, ControllerManager _enemyController, CompSO _freeUnitCompSO, PlayerUnitsSO _playerUnitsSO, CompsAndUnitsSO _compsAndUnits)        
        {
            _allUnits = new List<GameObject>();
            // this._controllerIndex = _controllerIndex;
            Debug.Log("size " + _freeUnitCompSO.UnitSOArray.Length.ToString());
            _playerUnitsSO.InitializePlayerUnitsSize(_freeUnitCompSO.UnitSOArray.Length);
            if (_freeUnitCompSO.UnitSOArray.Length > 0) {
            for (int i = 0; i < _freeUnitCompSO.UnitSOArray.Length; i++)
            {
                if (_freeUnitCompSO.UnitSOArray[i] != null)
                {
                    Vector3 _newUnitPos = _freeEnemyCompLayout.UnitPos[0, i];
                    GameObject newFreeEnemy = Instantiate(_FreeUnitPrefab, _newUnitPos, Quaternion.identity, gameObject.transform);
                    newFreeEnemy.name = _freeUnitCompSO.UnitSOArray[i].name + " " + i.ToString();
                    newFreeEnemy.GetComponent<PushBackFromPlayer>()?.Initialize(_enemyController);
                    newFreeEnemy.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _freeUnitCompSO.UnitSOArray[i], _compsAndUnits);
                    newFreeEnemy.GetComponent<Mover>()?.Initialize(_controllerIndex);
                    _playerUnitsSO.ActiveUnits.Add(newFreeEnemy);
                    _allUnits.Add(newFreeEnemy);
                }
            }
            }
        }
        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
    }
}
