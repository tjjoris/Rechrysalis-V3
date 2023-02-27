using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
// using System;

namespace Rechrysalis.Unit
{
    public class UnitManager : MonoBehaviour
    {
        private bool debugBool = false;
        [SerializeField] private UnitClass _unitClass;
        public UnitClass UnitClass => _unitClass;
        [SerializeField] private ControllerUnitSpriteHandler _unitSpriteHandler;
        [SerializeField] private int _controllerIndex;        
        public int ControllerIndex {get{return _controllerIndex;}}
        private int _freeUnitIndex;
        [SerializeField] private UnitStatsSO _unitStats;
        private HatchEffectSO _hatchEffectSO;
        [SerializeField] private TMP_Text _nameText;
        public UnitStatsSO UnitStats {get{return _unitStats;}}
        private Health _health;
        private Mover _mover;
        private Attack _attack;
        private ControllerUnitAttackClosest _controllerUnitAttackClosest;
        private ChrysalisTimer _chrysalisTimer;
        private Rechrysalize _rechrysalize;
        private CompsAndUnitsSO _compsAndUnits;
        private ProjectilesPool _projectilesPool;
        private List<GameObject> _currentHatchEffects;
        public List<GameObject> CurrentHatchEffects => _currentHatchEffects;
        [SerializeField] private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab {get {return _hatchEffectPrefab;}}
        private FreeUnitHatchEffect _freeHatchScript;
        private ParentUnitManager _parentUnitManager;
        private float _baseDPS;
        private float _newDPS;
        private float _baseChargeUp;
        private float _newChargeUp;
        private float _baseWindDown;
        private float _newWindDown;
        private float _baseIncomindDamageMult = 1;
        private float _newIncomingDamageMult = 1;
        private float _manaCost;
        public float ManaCost {get{return _manaCost;}}
        // [SerializeField] private bool _isStopped;
        // public bool IsStopped 
        // {
        //     set{
        //             _isStopped = value;
        //             if (_mover != null)
        //             {
        //             _mover.IsStopped = _isStopped;
        //             }
        //             if (_attack != null)
        //             {
        //             _attack.IsStopped = _isStopped;
        //             }
        //         }
        //     }
        public System.Action<float> _unitDealsDamage;
        public void Initialize(int controllerIndex, UnitClass unitClass, int freeUnitIndex, CompsAndUnitsSO compsAndUnits)
        {
            _parentUnitManager = transform.parent.GetComponent<ParentUnitManager>();
            _controllerIndex = controllerIndex;
            _compsAndUnits = compsAndUnits;
            Debug.Log($"controller index " + _controllerIndex);
            _unitClass = unitClass;
            // GetComponent<ProjectilesPool>()?.CreatePool(_unitClass)
            _mover = GetComponent<Mover>();
            _attack = GetComponent<Attack>();
            _controllerUnitAttackClosest = GetComponent<ControllerUnitAttackClosest>();
            _controllerUnitAttackClosest?.Initialzie();
            if (_parentUnitManager != null) _parentUnitManager.IsStopped = true;
            _attack?.Initialize(_unitClass);
            _health?.Initialize(_unitClass.HPMax);
            _nameText.text = unitClass.UnitName;
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Rechrysalize>()?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ChildUnitCount);
            GetComponent<InRangeByPriority>()?.Initialize(_compsAndUnits.TargetsLists[_controllerIndex]);
            GetComponent<ClosestTarget>()?.Initialize(_compsAndUnits.PlayerUnits[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Range>()?.Initialize(unitClass);
            _chrysalisTimer = GetComponent<ChrysalisTimer>();
            _rechrysalize = GetComponent<Rechrysalize>();
            _projectilesPool = GetComponent<ProjectilesPool>();
            _projectilesPool?.CreatePool(unitClass.AmountToPool, unitClass.ProjectileSpeed, unitClass.ProjectileSprite);
            _currentHatchEffects = new List<GameObject>();
            _freeHatchScript = GetComponent<FreeUnitHatchEffect>();
            this._freeUnitIndex = freeUnitIndex;
            _freeHatchScript?.Initialize(unitClass.HatchEffectPrefab, _freeUnitIndex);
            // _unitSpriteHandler.SetSpriteFunction(unitClass.UnitSprite);
            float _hatchManaMult = 1;
            if (_hatchEffectSO != null)
            {
                if (_hatchEffectSO.ManaMultiplier.Length >= _unitStats.TierMultiplier.Tier - 1)
                {
                    _hatchManaMult = _hatchEffectSO.ManaMultiplier[_unitStats.TierMultiplier.Tier - 1];
                }
            }
            _manaCost = unitClass.ManaCost;
            ReCalculateDamageChanges();
        }
        public void SetUnitSPrite(Sprite unitSprite)
        {
            _unitSpriteHandler.SetSpriteFunction(unitSprite);
        }
        public void InitializeOld(int _controllerIndex, UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _freeUnitIndex, HatchEffectSO _hatchEffectSO)
        {
            this._controllerIndex = _controllerIndex;
            this._unitStats = _unitStats;
            this._hatchEffectSO = _hatchEffectSO;
            _unitStats.Initialize();
            _baseDPS = _unitStats.BaseDPSBasic;
            _baseChargeUp = _unitStats.AttackChargeUpBasic;
            _baseWindDown = _unitStats.AttackWindDownBasic;
            this._compsAndUnits = _compsAndUnits;
            GetComponent<ProjectilesPool>()?.CreatePool(_unitStats.AmountToPool, _unitStats.ProjectileSpeed, _unitStats.ProjectileSprite);
            // _nameText.text = _unitStats.UnitName;
            _mover = GetComponent<Mover>();
            _attack = GetComponent<Attack>();
            _controllerUnitAttackClosest = GetComponent<ControllerUnitAttackClosest>();
            _controllerUnitAttackClosest?.Initialzie();
            if (_parentUnitManager != null)  _parentUnitManager.IsStopped = true;
            _health = GetComponent<Health>();
            _health?.Initialize(_unitStats.HealthMaxBasic);
            _attack?.Initialize(_unitClass);
            _attack?.InitializeOld(_unitStats);
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Rechrysalize>()?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ChildUnitCount);            
            GetComponent<InRangeByPriority>()?.Initialize(_compsAndUnits.TargetsLists[_controllerIndex]);
            GetComponent<ClosestTarget>()?.Initialize(_compsAndUnits.PlayerUnits[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Range>()?.InitializeOld(_unitStats);
            _chrysalisTimer = GetComponent<ChrysalisTimer>();
            _rechrysalize = GetComponent<Rechrysalize>();
            _projectilesPool = GetComponent<ProjectilesPool>();
            _currentHatchEffects = new List<GameObject>();
            // _hatchEffectPrefab = _unitStats.HatchEffectPrefab;
            _freeHatchScript = GetComponent<FreeUnitHatchEffect>();
            this._freeUnitIndex = _freeUnitIndex;
            _freeHatchScript?.Initialize(_unitStats.HatchEffectPrefab, _freeUnitIndex);
            _unitSpriteHandler.SetSpriteFunction(_unitStats.UnitSprite);
            float _hatchManaMult = 1;
            if (_hatchEffectSO != null)
            {
                if (_hatchEffectSO.ManaMultiplier.Length >= _unitStats.TierMultiplier.Tier - 1)
                {
                    _hatchManaMult = _hatchEffectSO.ManaMultiplier[_unitStats.TierMultiplier.Tier - 1];
                }
            }
            _manaCost = _unitStats.Mana * _hatchManaMult;
            // ReCalculateStatChanges();
            // _attack?.SetDamage(_unitStats.BaseDamageBasic);
            _attack?.SetDPS(_unitStats.BaseDPSBasic);
            ReCalculateDamageChanges();
        }
        private void OnEnable()
        {
            if (GetComponent<ProjectilesPool>() != null)
            {
                GetComponent<ProjectilesPool>()._projectileDealsDamage += UnitDealsDamage;
            }
        }
        private void OnDisable()
        {
            if (GetComponent<ProjectilesPool>() != null)
            {
                GetComponent<ProjectilesPool>()._projectileDealsDamage -= UnitDealsDamage;
            }
        }
        private void UnitDealsDamage(float _damage)
        {
            _unitDealsDamage?.Invoke(_damage);
        }
        public void SetUnitName (string _unitName)
        {
            _nameText.text = _unitName;
        }
        public void RestartUnit()
        {
            _health?.RestartUnit();
            // _freeHatchScript?.TriggerHatchEffect();
        }
        public void Tick(float _timeAmount)
        {
            if (debugBool)   Debug.Log($"tick");
            // if (gameObject.active == true) 
            // {
                // bool _isStopped = false;
                _controllerUnitAttackClosest?.CheckToGetTarget();
                if (_mover != null)
                {
                    // _mover?.Tick(_timeAmount);
                    // if ((!_mover.IsStopped) && ())
                    // _isStopped = _mover.IsStopped;
                }
                if (_attack != null)
                {
                    // _attack.IsStopped = _isStopped;
                    _attack?.Tick(_timeAmount);
                }
                // _projectilesPool?.TickProjectiles(_timeAmount);
                _chrysalisTimer?.Tick(_timeAmount);
            // }
        }
        public bool IsEnemy(int _controllerIndex)
        {
            if (this._controllerIndex != _controllerIndex)
            {
                return true;
            }
            return  false;
        }
        public void SetReserveChrysalis(int _childIndex)
        {
            _rechrysalize?.SetNextEvolved(_childIndex);
        }
        public void RemoveHatchEffect(GameObject _hatchEffect)
        {
            if (_currentHatchEffects.Contains(_hatchEffect))
            {
                _currentHatchEffects.Remove(_hatchEffect);
            }
            // ReCalculateStatChanges();
            if (_hatchEffect.GetComponent<HEIncreaseDamage>() != null)
            {
                ReCalculateDamageChanges();
            }
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            if (!_currentHatchEffects.Contains(_hatchEffect))
            {
                _currentHatchEffects.Add(_hatchEffect);
            }
            // ReCalculateStatChanges();
            if (_hatchEffect.GetComponent<HEIncreaseDamage>() != null)
            {
                ReCalculateDamageChanges();                
            }
        }
        private void ReCalculateDamageChanges()
        {
            List<HEIncreaseDamage> hEIncreaseDamageList = new List<HEIncreaseDamage>();
            foreach (GameObject hatchEffect in _currentHatchEffects)
            {
                if (hatchEffect.GetComponent<HEIncreaseDamage>() != null)
                {
                    hEIncreaseDamageList.Add(hatchEffect.GetComponent<HEIncreaseDamage>());
                }
            }
            _attack?.ReCalculateDamageWithHE(hEIncreaseDamageList);
        }
        // private void ReCalculateDefenceChanges()
        // {
        //     List<HEIncreaseDefence> hEIncreaseDefenceList = new List<HEIncreaseDefence>();
        //     foreach (GameObject hatchEffect  in _hatchEffects)
        //     {
        //         if (hatchEffect.GetComponent<HEIncreaseDefence>() != null)
        //         {
        //             hEIncreaseDefenceList.Add(hatchEffect.GetComponent<HEIncreaseDefence>());
        //         }
        //     }
        // }
        private void ReCalculateStatChanges()
        {
            // _newDPS = _baseDPS;
            // _newChargeUp = _baseChargeUp;
            // _newWindDown = _baseWindDown;
            // _newIncomingDamageMult = _baseIncomindDamageMult;
            // if ((_hatchEffects!= null) && (_hatchEffects.Count > 0))
            // {
            //     for (int _hatchIndex = 0; _hatchIndex < _hatchEffects.Count; _hatchIndex++)
            //     {
            //         HatchEffectManager _hatchEffectManager = _hatchEffects[_hatchIndex].GetComponent<HatchEffectManager>();
            //         _newDPS += _hatchEffectManager.DPSIncrease;
            //         _newIncomingDamageMult *= _hatchEffectManager.IncomingDamageMult;
            //     }
            // }
            // if (_newDPS == 0) 
            // {
            //     _attack?.SetDamage(0);
            //     return;
            // }
            // _attack?.SetDamage(_newDPS / (_newChargeUp + _newWindDown));
        }

        public float GetIncomingDamageMultiplier()
        {
            return _newIncomingDamageMult;
        }
        public void ShowUnitText()
        {
            _nameText.gameObject.SetActive(true);
        }
        public void HideUnitText()
        {
            _nameText.gameObject.SetActive(false);
        }
        public float GetDamage()
        {
            return _attack.GetDamage();
        }
    }
}
