using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;
using Rechrysalis.HatchEffect;
using Rechrysalis.CompCustomizer;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        [SerializeField] private CheckRayCastSO _checkRayCast;
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
        [SerializeField] private HilightRingManager _hilightRingManager;
        [SerializeField] private UpgradeRingManager _upgradeRingManager;
        [SerializeField] private float _unitRingOuterRadius;
        private Mover _mover;
        private bool _isStopped;
        private List<GameObject> _hatchEffects;
        private FreeEnemyInitialize _freeEnemyInitialize;
        private ControllerFreeUnitHatchEffectManager _controllerFreeHatchEffectManager;
        // private CompCustomizerSO _compCustomizer;
        private ManaGenerator _manaGenerator;
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

        public void Initialize(int _controllerIndex, PlayerUnitsSO[] _playerUnitsSO, CompSO _compSO, ControllerManager _enemyController, CompsAndUnitsSO _compsAndUnits, CompCustomizerSO _compCustomizer) {
            this._controllerIndex = _controllerIndex;
            this._playerUnitsSO = _playerUnitsSO;
            this._compSO = _compSO;
            this._enemyController = _enemyController;
            this._compsAndUnits = _compsAndUnits;
            // this._compCustomizer = _compCustomizer;
            _manaGenerator = GetComponent<ManaGenerator>();
            
            _allUnits = new List<GameObject>();
            _hatchEffects = new List<GameObject>();
            _allUnits.Clear();
            _mover = GetComponent<Mover>();
            if (_mover != null) {
            _mover?.Initialize(_controllerIndex);
            _mover?.SetSpeed(_compsAndUnits.Speed);
            }
            _click?.Initialize(gameObject, _compsAndUnits, _unitRingManager, _checkRayCast);
            _touch?.Initialize(gameObject, _compsAndUnits, _unitRingManager, _checkRayCast);
            _checkRayCast?.Initialize(_compsAndUnits, _unitRingManager, _hilightRingManager, _upgradeRingManager, _unitRingOuterRadius);
            _controllerFreeHatchEffectManager = GetComponent<ControllerFreeUnitHatchEffectManager>();
            _freeEnemyInitialize = GetComponent<FreeEnemyInitialize>();
            if (_freeEnemyInitialize != null)
            {
            _freeEnemyInitialize.Initialize(_controllerIndex, _enemyController, _compSO, _playerUnitsSO[_controllerIndex], _compsAndUnits, _compsAndUnits.FreeUnitCompSO[_controllerIndex], _compCustomizer);
            _allUnits = _freeEnemyInitialize.GetAllUnits();
            }            
            RechrysalisControllerInitialize _rechrysalisControllerInitialize = GetComponent<RechrysalisControllerInitialize>();
            if (GetComponent<RechrysalisControllerInitialize>() != null)
            {
            _rechrysalisControllerInitialize.Initialize(_controllerIndex, _compSO, _compsAndUnits, _unitRingManager, _hilightRingManager, _upgradeRingManager, _unitRingOuterRadius);
            _allUnits = _rechrysalisControllerInitialize.GetAllUnits();
            _parentUnits = GetComponent<RechrysalisControllerInitialize>().ParentUnits;
            _manaGenerator?.Initialize(_parentUnits);
            }
            // Debug.Log($"health " + _compsAndUnits.ControllerHealth[_controllerIndex]);
            GetComponent<ControllerHealth>()?.Initialize(_compsAndUnits.ControllerHealth[_controllerIndex], _allUnits, _compsAndUnits);
            // _unitRingManager?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ParentUnitCount);            
            SetIsStopped(true);

            SubScribeToParentUnits();
            GetComponent<ControllerHealth>()?.SubscribeToParentUnits(_parentUnits);
            _rechrysalisControllerInitialize?.ActivateInitialUnits();
        }        
        private void OnEnable()
        {
           SubScribeToParentUnits();
           UnSubscribeToHatchEffects();
        }
        public void SubScribeToParentUnits()
        {
            if ((_parentUnits != null) && (_parentUnits.Length > 0))
            {
                // foreach (GameObject _parentUnit in _parentUnits)
                for (int _parentIndex = 0; _parentIndex < _parentUnits.Length; _parentIndex++)                
                {
                    if (_parentUnits[_parentIndex] != null)
                    {
                        ParentUnitManager _parentManager = _parentUnits[_parentIndex].GetComponent<ParentUnitManager>();
                        _parentManager._addHatchEffect -= AddHatchEffect;
                        _parentManager._addHatchEffect += AddHatchEffect;
                        _parentManager._parentDealsDamage -= DealsDamage;
                        _parentManager._parentDealsDamage += DealsDamage;

                    }
                }
            }
        }
        public void SubscribeToHatchEffect(GameObject _hatchEffect)
        {
            _hatchEffect.GetComponent<HatchEffectManager>()._hatchEffectDies -= RemoveHatchEffect;
            _hatchEffect.GetComponent<HatchEffectManager>()._hatchEffectDies += RemoveHatchEffect;
        }
        public void SubscribeToHatchEffects()
        {
            if ((_hatchEffects != null) && (_hatchEffects.Count > 0))
            {
                foreach (GameObject _hatchEffect in _hatchEffects)
                {
                    if (_hatchEffect != null)
                    {
                        _hatchEffect.GetComponent<HatchEffectManager>()._hatchEffectDies -= RemoveHatchEffect;
                        _hatchEffect.GetComponent<HatchEffectManager>()._hatchEffectDies += RemoveHatchEffect;
                    }
                }
            }
        }
        public void UnSubscribeToHatchEffects()
        {
            if ((_hatchEffects != null) && (_hatchEffects.Count > 0))
            {
                foreach (GameObject _hatchEffect in _hatchEffects)
                {
                    if (_hatchEffect != null)
                    {
                        _hatchEffect.GetComponent<HatchEffectManager>()._hatchEffectDies -= RemoveHatchEffect;
                    }
                }
            }
        }
        
        private void OnDisable()
        {
            foreach (GameObject _parentUnit in _parentUnits)
            {
                if (_parentUnit != null)
                {
                    _parentUnit.GetComponent<ParentUnitManager>()._addHatchEffect -= AddHatchEffect;
                    _parentUnit.GetComponent<ParentUnitManager>()._parentDealsDamage -= DealsDamage;
                }
            }
            UnSubscribeToHatchEffects();
        }
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
        public void FixedTick(float _timeAmount)
        {
            // float _timeAmount = Time.fixedDeltaTime;         
            // _mover?.Tick(_timeAmount);
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
            _unitRingManager?.Tick(_timeAmount);
            TickParentUnits();
            TickChildUnits(_timeAmount);
            TickFreeEnemyParentUnits(_timeAmount);
            TickHatchEffects(_timeAmount);
            _manaGenerator?.Tick(_timeAmount);
        }

        private void TickHatchEffects(float _timeAmount)
        {
            if (_hatchEffects.Count > 0)
            {
            // foreach (GameObject _hatchEffect in _hatchEffects)
            for (int _hatchIndex = 0; _hatchIndex < _hatchEffects.Count; _hatchIndex ++)
            {
                // HETimer _hETimer = _hatchEffect.GetComponent<HETimer>();
                // if ((_hETimer != null))
                // {
                //     _hETimer.Tick(_timeAmount);
                //     if (_hETimer.CheckIsExpired())
                //     {
                //         foreach (GameObject _parentUnit in _parentUnits)
                //         {
                //             _parentUnit.GetComponent<ParentUnitManager>()?.RemoveHatchEffect(_hatchEffect);
                //         }
                //     }
                // }
                if (_hatchEffects[_hatchIndex] != null)
                {
                    HatchEffectManager _hatchEffectManager = _hatchEffects[_hatchIndex].GetComponent<HatchEffectManager>();
                    _hatchEffectManager?.Tick(_timeAmount);
                }
            }
            }
        }

        private void TickFreeEnemyParentUnits(float _timeAmount)
        {
            foreach (GameObject _freeParentUnit in _playerUnitsSO[_controllerIndex].ParentUnits)
            {
                if ((_freeParentUnit != null))
                {
                    _freeParentUnit.GetComponent<ParentFreeEnemyManager>()?.Tick(_timeAmount);
                }
            }
        }

        private void TickChildUnits(float _timeAmount)
        {
            foreach (GameObject _unit in _allUnits)
            {
                if (_unit.activeInHierarchy)
                {
                    _unit.GetComponent<UnitManager>().Tick(_timeAmount);
                }
            }
        }

        private void TickParentUnits()
        {
            foreach (GameObject _parentUnit in _parentUnits)
            {
                if (_parentUnit != null)
                {
                    _parentUnit.GetComponent<RotateParentUnit>()?.Tick();
                }
            }
        }
        private void DealsDamage(float _damage)
        {
            _manaGenerator.StartTimer();
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
        public void ActivateChrysalis(int _parentUnit, int _childUnit)
        {
            if ((_childUnit != -1) && (_parentUnits[_parentUnit] != null) && (_parentUnits[_parentUnit].GetComponent<ParentUnitManager>().SubUnits[_childUnit] != null))
            {
                _parentUnits[_parentUnit].GetComponent<ParentUnitManager>()?.UpgradeUnit(_childUnit);
            }
        }
        public void ReserveChrysalis(int _parentIndex, int _childIndex)
        {
            if (_childIndex != -1)
            {
                _parentUnits[_parentIndex].GetComponent<ParentUnitManager>()?.ReserveChrysalis(_parentIndex, _childIndex);
            }
        }
        public void AddHatchEffect(GameObject _hatchEffect, int _parentIndex, int _unitIndex, bool _effectAll)
        {
            {
                Debug.Log($" add hatch effect " + _hatchEffect.name + " for parents" + _parentIndex + "unit " + _unitIndex);
                for (int _parentLoopIndex = 0; _parentLoopIndex < _parentUnits.Length; _parentLoopIndex++)
                {
                    if (_parentUnits[_parentLoopIndex] != null)
                    {
                        // Debug.Log($"subscribing to hatch effect for parent " + _parentLoopIndex);
                        if (_parentLoopIndex == _parentIndex)
                        {
                            _parentUnits[_parentLoopIndex].GetComponent<ParentUnitManager>()?.AddHatchEffect(_hatchEffect);
                        }
                        else if (_effectAll)
                        {
                            _parentUnits[_parentLoopIndex].GetComponent<ParentUnitManager>()?.AddHatchEffect(_hatchEffect);
                        }
                    }
                }
                _hatchEffects.Add(_hatchEffect);
                SubscribeToHatchEffect(_hatchEffect);
            }
            if ((_parentUnits[_parentIndex] != null))
            {
                ParentUnitHatchEffects _parentUnitHatchEffect = _parentUnits[_parentIndex].GetComponent<ParentUnitHatchEffects>();
                {
                    if (_parentUnitHatchEffect != null)
                    {
                        _parentUnitHatchEffect.AddHatchEffect(_hatchEffect);
                    }
                }
            }
        }
        public void RemoveHatchEffect(GameObject _hatchEffect, int _parentIndex, int _unitIndex, bool _effectAll)
        {
            for (int _parentLoopIndex = 0; _parentLoopIndex < _parentUnits.Length; _parentLoopIndex++)
            {
                if (_parentUnits[_parentLoopIndex] != null)
                {
                    if (_parentLoopIndex == _parentIndex)
                    {
                        _parentUnits[_parentLoopIndex].GetComponent<ParentUnitManager>()?.RemoveHatchEffect(_hatchEffect);
                    }
                    else if (_effectAll)
                    {
                        _parentUnits[_parentLoopIndex].GetComponent<ParentUnitManager>()?.RemoveHatchEffect(_hatchEffect);
                    }
                }
            }
            if (_hatchEffects.Contains(_hatchEffect))
            {
                _hatchEffects.Remove(_hatchEffect);
            }
            if ((_parentUnits[_parentIndex] != null))
            {
                ParentUnitHatchEffects _parentUnitHatchEffect = _parentUnits[_parentIndex].GetComponent<ParentUnitHatchEffects>();
                {
                    if (_parentUnitHatchEffect != null)
                    {
                        _parentUnitHatchEffect.RemoveHatchEffect(_hatchEffect);
                    }
                }
            }
            Destroy(_hatchEffect.gameObject);
        }
        public void ShowUnitText()
        {
            for (int _unitIndex = 0; _unitIndex < _allUnits.Count; _unitIndex ++)
            {
                _allUnits[_unitIndex].GetComponent<UnitManager>().ShowUnitText();
            }
        }
        public void HideUnitText()
        {
            for (int _unitIndex = 0; _unitIndex < _allUnits.Count; _unitIndex++)
            {
                _allUnits[_unitIndex].GetComponent<UnitManager>().HideUnitText();
            }
        }
    }
}
