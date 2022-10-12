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
        private int _controllerIndex;
        private ControllerManager _enemyController;
        private CompSO _compSO;
        private PlayerUnitsSO _playerUnitsSO;
        private CompsAndUnitsSO _compsAndUnits;
        private FreeUnitCompSO _freeUnitCompSO;
        // private int _controllerIndex;
        private List<GameObject> _allUnits;
        int _waveIndex;
        public void Initialize(int _controllerIndex, ControllerManager _enemyController, CompSO _compSO, PlayerUnitsSO _playerUnitsSO, CompsAndUnitsSO _compsAndUnits, FreeUnitCompSO _freeUnitCompSO)        
        {
            this._controllerIndex = _controllerIndex;
            this._enemyController = _enemyController;
            this._compSO = _compSO;
            this._playerUnitsSO = _playerUnitsSO;
            this._compsAndUnits = _compsAndUnits;
            this._freeUnitCompSO = _freeUnitCompSO;
            _allUnits = new List<GameObject>();
            // this._controllerIndex = _controllerIndex;
            Debug.Log("size " + _compSO.UnitSOArray.Length.ToString());
            _playerUnitsSO.InitializePlayerUnitsSize(_compSO.UnitSOArray.Length);
            if (_freeUnitCompSO.Waves.Length > 0)
            {
                // for (int _waveIndex = 0; _waveIndex < _freeUnitCompSO.Waves.Length; _waveIndex++)
                _waveIndex = 0;
                // {
                    CreateWave(_controllerIndex, _enemyController, _compSO, _playerUnitsSO, _compsAndUnits, _freeUnitCompSO, _waveIndex);
                // }
            }
        }

        private void CreateWave(int _controllerIndex, ControllerManager _enemyController, CompSO _compSO, PlayerUnitsSO _playerUnitsSO, CompsAndUnitsSO _compsAndUnits, FreeUnitCompSO _freeUnitCompSO, int _waveIndex)
        {
            // if (_compSO.UnitSOArray.Length > 0) {
            if (_freeUnitCompSO.Waves[_waveIndex].UnitInWave.Length > 0)
            {
                // for (int i = 0; i < _compSO.UnitSOArray.Length; i++)
                for (int _unitInWaveIndex = 0; _unitInWaveIndex < _freeUnitCompSO.Waves[_waveIndex].UnitInWave.Length; _unitInWaveIndex++)
                {
                    if (_compSO.UnitSOArray[_unitInWaveIndex] != null)
                    {
                        Vector3 _newUnitPos = _freeEnemyCompLayout.UnitPos[0, _unitInWaveIndex];
                        GameObject newFreeEnemy = Instantiate(_FreeUnitPrefab, _newUnitPos, Quaternion.identity, gameObject.transform);
                        newFreeEnemy.name = _compSO.UnitSOArray[_unitInWaveIndex].name + " " + _unitInWaveIndex.ToString();
                        newFreeEnemy.GetComponent<PushBackFromPlayer>()?.Initialize(_enemyController);
                        newFreeEnemy.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _compSO.UnitSOArray[_unitInWaveIndex], _compsAndUnits);
                        newFreeEnemy.GetComponent<Mover>()?.Initialize(_controllerIndex);
                        _playerUnitsSO.ActiveUnits.Add(newFreeEnemy);
                        _allUnits.Add(newFreeEnemy);
                    }
                }
            }
        }
        public void NextWave()
        {
            _waveIndex ++;
            if (_freeUnitCompSO.Waves.Length >= _waveIndex)
            {                
                CreateWave(_controllerIndex, _enemyController, _compSO, _playerUnitsSO, _compsAndUnits, _freeUnitCompSO, _waveIndex);
            }
        }

        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
    }
}
