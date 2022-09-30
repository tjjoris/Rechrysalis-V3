using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class FreeEnemyInitialize : MonoBehaviour
    {
        [SerializeField] private FreeUnitLayoutSO _freeEnemyCompLayout;
        [SerializeField] private GameObject _FreeUnitPrefab;
        public void Initialize(ControllerManager _enemyController, CompSO _freeUnitCompSO, PlayerUnitsSO _playerUnitsSO)        
        {
            if (_freeUnitCompSO.UnitSOArray.Length > 0) {
            for (int i = 0; i < _freeUnitCompSO.UnitSOArray.Length; i++)
            {
                if (_freeUnitCompSO.UnitSOArray[i] != null)
                {
                    Vector3 _newUnitPos = _freeEnemyCompLayout.UnitPos[0, i];
                    GameObject newFreeEnemy = Instantiate(_FreeUnitPrefab, _newUnitPos, Quaternion.identity, gameObject.transform);
                    newFreeEnemy.GetComponent<PushBackFromPlayer>()?.Initialize(_enemyController);
                    _playerUnitsSO.ActiveUnits[i] = newFreeEnemy;
                }
            }
            }
        }
    }
}
