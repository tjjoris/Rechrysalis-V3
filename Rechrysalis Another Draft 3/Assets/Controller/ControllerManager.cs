using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        [SerializeField] private Click _click;
        [SerializeField] private TouchSO _touch;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits{get{return _parentUnits;} set{_parentUnits = value;}}
        [SerializeField] private PlayerUnitsSO[] _playerUnitsSO;
        public PlayerUnitsSO[] PlayerUnitsSO {get{return _playerUnitsSO;} set{_playerUnitsSO = value;}}    
        [SerializeField] private CompSO _compSO;     
        private ControllerManager _enemyController;

        private Mover _mover;
        public void Initialize(int _controllerIndex, PlayerUnitsSO[] _playerUnitsSO, CompSO _compSO, ControllerManager _enemyController) {
            this._controllerIndex = _controllerIndex;
            this._playerUnitsSO = _playerUnitsSO;
            this._compSO = _compSO;
            this._enemyController = _enemyController;
            _mover = GetComponent<Mover>();
            if (_mover != null) {
            _mover?.Initialize(_controllerIndex);
            }
            _click?.Initialize(gameObject);
            _touch?.Initialize(gameObject);
            
            GetComponent<FreeEnemyInitialize>()?.Initialize(_enemyController, _compSO, _playerUnitsSO[_controllerIndex]);
            GetComponent<RechrysalisControllerInitialize>()?.Initialize();
        }
        private void SetUpParentUnits()
        {
            if (_parentUnits.Length > 0)
            {
                for (int i = 0; i < _parentUnits.Length; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        ParentUnitManager _parentUnitManager = _parentUnits[i].GetComponent<ParentUnitManager>();
                        _parentUnitManager.SubUnits[j].GetComponent<UnitManager>()?.Initialize(_compSO.UnitSOArray[(i * 3) + j]);
                    }
                    _parentUnits[i].GetComponent<ParentUnitManager>()?.ActivateUnit(0);
                }
            }
        }
        private void Update() 
        {
            _click?.Tick();
            _touch?.Tick();
        }
        public void ResetTick()
        {
            _mover?.ResetMovement();
            if (_playerUnitsSO[_controllerIndex].ActiveUnits.Length > 0)
            {
                foreach (GameObject _unitToReset in _playerUnitsSO[_controllerIndex].ActiveUnits)
                {
                    if (_unitToReset != null)
                    {
                    _unitToReset.GetComponent<Mover>()?.ResetMovement();
                    }
                }
            }
        }
        public void Tick() {  
            float _deltaTime = Time.deltaTime;         
            _mover?.Tick(_deltaTime);
            if (_playerUnitsSO[_controllerIndex].ActiveUnits.Length > 0)
            {
                foreach (GameObject _unitToTick in _playerUnitsSO[_controllerIndex].ActiveUnits)
                {
                    if (_unitToTick != null)
                    {
                    _unitToTick.GetComponent<Mover>()?.Tick(_deltaTime);
                    }
                }
            }
        }
    }
}
