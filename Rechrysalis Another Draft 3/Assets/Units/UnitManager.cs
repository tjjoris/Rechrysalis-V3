using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using Rechrysalis.Controller;
using Rechrysalis.AdvancedUpgrade;
// using System;

namespace Rechrysalis.Unit
{
    public class UnitManager : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        private ControllerManager _enemyControllerManager;
        private bool _debugBool = false;
        [SerializeField] private UnitClass _unitClass;
        public UnitClass UnitClass => _unitClass;
        [SerializeField] private ControllerUnitSpriteHandler _unitSpriteHandler;
        public ControllerUnitSpriteHandler ControllerUnitSpriteHandler => _unitSpriteHandler;
        [SerializeField] private int _controllerIndex;        
        public int ControllerIndex {get{return _controllerIndex;}}
        private int _freeUnitIndex;
        public int FreeUnitIndex => _freeUnitIndex;
        [SerializeField] private int _childUnitIndex;
        public int ChildUnitIndex => _childUnitIndex;
        [SerializeField] private UnitStatsSO _unitStats;
        private HatchEffectSO _hatchEffectSO;
        [SerializeField] private TMP_Text _nameText;
        public UnitStatsSO UnitStats {get{return _unitStats;}}
        [SerializeField] private Hatch _hatch;
        public Hatch Hatch { get => _hatch; set => _hatch = value; }
        private Health _health;
        private Mover _mover;
        private Attack _attack;
        private Range _range;
        private UnitManagerRemoveHatchEffect _unitManagerRemoveHatchEffect;
        private ControllerUnitAttackClosest _controllerUnitAttackClosest;
        private ChrysalisTimer _chrysalisTimer;
        public ChrysalisTimer ChryslaisTimer => _chrysalisTimer;
        private Rechrysalize _rechrysalize;
        private CompsAndUnitsSO _compsAndUnits;
        private ProjectilesPool _projectilesPool;
        private List<GameObject> _currentHatchEffects;
        public List<GameObject> CurrentHatchEffects => _currentHatchEffects;
        [SerializeField] private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab {get {return _hatchEffectPrefab;}}
        private FreeUnitHatchEffect _freeHatchScript;
        private ParentUnitManager _parentUnitManager;
        public ParentUnitManager ParentUnitManager => _parentUnitManager;
        private TargetPrioratizeByScore _targetPrioratizeByScore;
        private MoveSpeedAddManager _moveSpeedAddManager;
        public MoveSpeedAddManager MoveSpeedAddManager => _moveSpeedAddManager;
        private SiegeManager _siegeManager;
        private HatchAdjustBuildTimerMaxBase _hatchAdjustBuildTimerMaxBase;
        public HatchAdjustBuildTimerMaxBase HatchAdjustBuildTimerMaxBase => _hatchAdjustBuildTimerMaxBase;
        private BurstHealManager _burstHealManager;
        public BurstHealManager BurstHealManager => _burstHealManager;
        [SerializeField] private GameObject _particleEffectPrefab;
        public GameObject ParticleEffectPrefab => _particleEffectPrefab;
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
        public System.Action<float> _unitDealsDamage;

        private void Awake()
        {
            _hatch = GetComponent<Hatch>();
            _mover = GetComponent<Mover>();
            _attack = GetComponent<Attack>();
            _chrysalisTimer = GetComponent<ChrysalisTimer>();
            _rechrysalize = GetComponent<Rechrysalize>();
            _projectilesPool = GetComponent<ProjectilesPool>();
            _targetPrioratizeByScore = GetComponent<TargetPrioratizeByScore>();
            _range = GetComponent<Range>();
            _unitManagerRemoveHatchEffect = GetComponent<UnitManagerRemoveHatchEffect>();
        }
        public void Initialize(ControllerManager controllerManager, int controllerIndex, UnitClass unitClass, int freeUnitIndex, int subUnitIndex, CompsAndUnitsSO compsAndUnits, bool isChrysalis)
        {
            _controllerManager = controllerManager;
            _childUnitIndex = subUnitIndex;
            _parentUnitManager = transform.parent.GetComponent<ParentUnitManager>();
            Mover mover = _controllerManager.GetComponent<Mover>();
            if (_parentUnitManager.GetComponent<Mover>() != null)
            {
                mover = _parentUnitManager.GetComponent<Mover>();
            }
            _controllerIndex = controllerIndex;
            _compsAndUnits = compsAndUnits;
            _enemyControllerManager = _compsAndUnits.ControllerManagers[GetOppositeController.ReturnOppositeController(controllerIndex)];
            if (_debugBool)
            {
                Debug.Log($"controller index " + _controllerIndex);
            }
            _unitClass = unitClass;
            // GetComponent<ProjectilesPool>()?.CreatePool(_unitClass)
            _controllerUnitAttackClosest = GetComponent<ControllerUnitAttackClosest>();
            // _controllerUnitAttackClosest?.Initialzie();
            if (_parentUnitManager != null) _parentUnitManager.IsStopped = true;
            _attack?.Initialize(_unitClass, _parentUnitManager);
            _health?.Initialize(_unitClass.HPMax);
            _nameText.text = unitClass.UnitName;
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)], _controllerManager, _parentUnitManager);
            GetComponent<Rechrysalize>()?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ChildUnitCount);
            GetComponent<InRangeByPriority>()?.Initialize(_compsAndUnits.TargetsLists[_controllerIndex]);
            GetComponent<ClosestTarget>()?.Initialize(_compsAndUnits.PlayerUnits[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Range>()?.Initialize(unitClass);
            _projectilesPool?.CreatePool(unitClass.AmountToPool, unitClass.ProjectileSpeed, unitClass.ProjectileSprite);
            _currentHatchEffects = new List<GameObject>();
            _freeHatchScript = GetComponent<FreeUnitHatchEffect>();
            this._freeUnitIndex = freeUnitIndex;
            // _freeHatchScript?.Initialize(unitClass.HatchEffectPrefab, _freeUnitIndex);
            float _hatchManaMult = 1;
            // if (_hatchEffectSO != null)
            // {
            //     if (_hatchEffectSO.ManaMultiplier.Length >= _unitStats.TierMultiplier.Tier - 1)
            //     {
            //         _hatchManaMult = _hatchEffectSO.ManaMultiplier[_unitStats.TierMultiplier.Tier - 1];
            //     }
            // }
            _manaCost = unitClass.ManaCost;
            _targetPrioratizeByScore?.Initialize(_enemyControllerManager.GetComponent<TargetScoreRanking>(), compsAndUnits.TargetsLists[_controllerIndex]);
            if (!isChrysalis)
            {
                if (_unitClass.SacrificeControllerAmount > 0)
                {
                    gameObject.AddComponent<SacrificeManager>();
                    SacrificeManager sacrificeManager = GetComponent<SacrificeManager>();
                    sacrificeManager?.Initialize(_controllerManager.GetComponent<ControllerHealth>(), _unitClass.SacrificeControllerAmount);
                }
                if (_unitClass.MoveSpeedAdd > 0)
                {
                    gameObject.AddComponent<MoveSpeedAddManager>();
                    _moveSpeedAddManager = GetComponent<MoveSpeedAddManager>();
                    
                    _moveSpeedAddManager?.Initialize(mover, _unitClass.MoveSpeedAdd);
                }
                if (_unitClass.SiegeDuration > 0)
                {
                    gameObject.AddComponent<SiegeManager>();
                    _siegeManager = GetComponent<SiegeManager>();
                    _siegeManager?.Initialize(mover);
                    _siegeManager?.AddToSiegeDuration(_unitClass.SiegeDuration);
                }
                if (_unitClass.BurstHeal > 0)
                {
                    gameObject.AddComponent<BurstHealManager>();
                    _burstHealManager = GetComponent<BurstHealManager>();
                    _burstHealManager?.Initialize(_controllerManager, _unitClass.BurstHeal);
                }
                if (_unitClass.HatchBuildTimeMaxBaseAdd != 0)
                {
                    _hatchAdjustBuildTimerMaxBase = gameObject.AddComponent<HatchAdjustBuildTimerMaxBase>();                    
                    _hatchAdjustBuildTimerMaxBase?.Initialize(_controllerManager, _unitClass.HatchBuildTimeMaxBaseAdd);
                }
                foreach (UpgradeTypeClass upgradeTypeClass in _parentUnitManager.ParentUnitClass.UTCHatchEffects)
                {
                    if (upgradeTypeClass == null) continue;
                    HatchEffectManager hatchEffectManager = upgradeTypeClass.HatchEffectManager;
                    if (hatchEffectManager == null) continue;
                    AddBurstHeal(hatchEffectManager);
                }
            }
            ReCalculateDamageChanges();
        }
        private void AddBurstHeal(HatchEffectManager hatchEffectManager)
        {
            if (hatchEffectManager.BurstHealAmount == 0) return;
            gameObject.AddComponent<BurstHealManager>();
            _burstHealManager = GetComponent<BurstHealManager>();
            _burstHealManager?.Initialize(_controllerManager, hatchEffectManager.BurstHealAmount);
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
            // _controllerUnitAttackClosest?.Initialzie();
            if (_parentUnitManager != null)  _parentUnitManager.IsStopped = true;
            _health = GetComponent<Health>();
            _health?.Initialize(_unitStats.HealthMaxBasic);
            // _attack?.Initialize(_unitClass);
            _attack?.InitializeOld(_unitStats);
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)], _controllerManager, _parentUnitManager);
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
        }
        public void Tick(float timeAmount)
        {
            if (_debugBool)   Debug.Log($"tick");
                if (_mover != null)
                {
                    // _mover?.Tick(_timeAmount);
                    // if ((!_mover.IsStopped) && ())
                    // _isStopped = _mover.IsStopped;
                }
                if (_attack != null)
                {
                    // _attack.IsStopped = _isStopped;
                    _attack?.Tick(timeAmount);
                }
                // _projectilesPool?.TickProjectiles(_timeAmount);
                _chrysalisTimer?.Tick(timeAmount);
                _moveSpeedAddManager?.Tick(timeAmount);
                _siegeManager?.Tick(timeAmount);
                _hatchAdjustBuildTimerMaxBase?.Tick(timeAmount);
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
            _unitManagerRemoveHatchEffect?.RemoveHatchEffect(_hatchEffect);
            // if (_currentHatchEffects.Contains(_hatchEffect))
            // {
            //     _currentHatchEffects.Remove(_hatchEffect);
            // }
            // // ReCalculateStatChanges();
            // if (_hatchEffect.GetComponent<HEIncreaseDamage>() != null)
            // {
            //     ReCalculateDamageChanges();
            // }
            // HEIncreaseRange heIncreaseRange = _hatchEffect.GetComponent<HEIncreaseRange>();
            // if (heIncreaseRange != null)
            // {
            //     _range?.RemoveRangeHE(heIncreaseRange);
            // }
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
            HEIncreaseRange heIncreaseRange = _hatchEffect.GetComponent<HEIncreaseRange>();
            if (heIncreaseRange != null)
            {
                _range?.AddRangeHE(heIncreaseRange);
            }
        }
        public void ReCalculateDamageChanges()
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
