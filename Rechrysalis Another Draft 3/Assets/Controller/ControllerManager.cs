using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        [SerializeField] private Click _click;
        [SerializeField] private TouchSO _touch;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits{get{return _parentUnits;} set{_parentUnits = value;}}  
        private List<GameObject> _allUnits;      
        [SerializeField] private PlayerUnitsSO[] _playerUnitsSO;
        public PlayerUnitsSO[] PlayerUnitsSO {get{return _playerUnitsSO;} set{_playerUnitsSO = value;}}    
        [SerializeField] private CompSO _compSO;     
        private ControllerManager _enemyController;
        [SerializeField] private CompsAndUnitsSO _compsAndUnits;
        [SerializeField] private UnitRingManager _unitRingManager;
        private Mover _mover;
        private bool _isStopped;
        // public bool IsStopped
        // {
        //     set
        //     {
        //         _isStopped = value;
        //         _mover.IsStopped = value;
        //         foreach (GameObject _parentUnit in _parentUnits)
        //         {
        //             _parentUnit.GetComponent<ParentUnitManager>().IsStopped = value;
        //         }
        //     }
        // }

        public void Initialize(int _controllerIndex, PlayerUnitsSO[] _playerUnitsSO, CompSO _compSO, ControllerManager _enemyController, CompsAndUnitsSO _compsAndUnits) {
            this._controllerIndex = _controllerIndex;
            this._playerUnitsSO = _playerUnitsSO;
            this._compSO = _compSO;
            this._enemyController = _enemyController;
            this._compsAndUnits = _compsAndUnits;
            _allUnits = new List<GameObject>();
            _allUnits.Clear();
            _mover = GetComponent<Mover>();
            if (_mover != null) {
            _mover?.Initialize(_controllerIndex);
            }
            _click?.Initialize(gameObject, _compsAndUnits, _unitRingManager);
            _touch?.Initialize(gameObject, _compsAndUnits, _unitRingManager);
            FreeEnemyInitialize _freeEnemyInitialize = GetComponent<FreeEnemyInitialize>();
            if (_freeEnemyInitialize != null)
            {
            _freeEnemyInitialize.Initialize(_controllerIndex, _enemyController, _compSO, _playerUnitsSO[_controllerIndex], _compsAndUnits, _compsAndUnits.FreeUnitCompSO[_controllerIndex]);
            _allUnits = _freeEnemyInitialize.GetAllUnits();
            }
            RechrysalisControllerInitialize _rechrysalisControllerInitialize = GetComponent<RechrysalisControllerInitialize>();
            if (GetComponent<RechrysalisControllerInitialize>() != null)
            {
            _rechrysalisControllerInitialize.Initialize(_controllerIndex, _compSO, _compsAndUnits, _unitRingManager);
            _allUnits = _rechrysalisControllerInitialize.GetAllUnits();
            _parentUnits = GetComponent<RechrysalisControllerInitialize>().ParentUnits;
            }
            // Debug.Log($"health " + _compsAndUnits.ControllerHealth[_controllerIndex]);
            GetComponent<ControllerHealth>()?.Initialize(_compsAndUnits.ControllerHealth[_controllerIndex], _allUnits);
            // _unitRingManager?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ParentUnitCount);
            SetIsStopped(true);


        }        
        // private void SetUpParentUnits()
        // {
        //     if (_parentUnits.Length > 0)
        //     {
        //         for (int i = 0; i < _parentUnits.Length; i++)
        //         {
        //             for (int j = 0; j < 3; j++)
        //             {
        //                 ParentUnitManager _parentUnitManager = _parentUnits[i].GetComponent<ParentUnitManager>();
        //                 _parentUnitManager.SubUnits[j].GetComponent<UnitManager>()?.Initialize(_controllerIndex, _compSO.UnitSOArray[(i * 3) + j], _compsAndUnits);
        //             }
        //             _parentUnits[i].GetComponent<ParentUnitManager>()?.ActivateUnit(0);
        //         }
        //     }
        // }
        private void Update() 
        {
            _click?.Tick();
            _touch?.Tick();
        }
        public void ResetTick()
        {
            _mover?.ResetMovement();
            if (_playerUnitsSO[_controllerIndex].ActiveUnits.Count > 0)
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
        public void Tick(float _timeAmount) {  
            // float _timeAmount = Time.fixedDeltaTime;         
            _mover?.Tick(_timeAmount);
            // if (_playerUnitsSO[_controllerIndex].ActiveUnits.Length > 0)
            // {
            //     foreach (GameObject _unitToTick in _playerUnitsSO[_controllerIndex].ActiveUnits)
            //     {
            //         if (_unitToTick != null)
            //         {
            //         // _unitToTick.GetComponent<Mover>()?.Tick(_deltaTime);
            //         _unitToTick.GetComponent<UnitManager>()?.Tick(_timeAmount);
            //         }
            //     }
            // }
            foreach (GameObject _unit in _allUnits)
            {
                if (_unit.active)
                {
                    _unit.GetComponent<UnitManager>().Tick(_timeAmount);
                }
            }
        }
        public void SetIsStopped(bool _isStopped)
        {
            this._isStopped = _isStopped;
            if (_mover != null) {
            _mover.IsStopped = _isStopped;
            }
            // foreach (GameObject _parentUnit in _parentUnits)
            // {
            //     _parentUnit.GetComponent<ParentUnitManager>().IsStopped = _isStopped;
            // }
            foreach (GameObject _unit in _allUnits)
            {
                _unit.GetComponent<UnitManager>().IsStopped = _isStopped;
            }
        }
    }
}
