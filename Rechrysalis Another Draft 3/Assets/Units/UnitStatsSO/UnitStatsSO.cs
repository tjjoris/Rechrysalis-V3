using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewUnitStats", menuName ="Unit/UnitStatsSO")]
    public class UnitStatsSO : ScriptableObject
    {
        // [SerializeField] private bool _empty;
        // public bool Empty {get {return _empty;}}
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        public UpgradeTypeClass UpgradeTypeClass {get {return _upgradeTypeClass;}}
        [SerializeField] private int _amountToPool;
        public int AmountToPool{get{return _amountToPool;}}
        [SerializeField] private float _projectileSpeed;
        public float ProjectileSpeed {get{return _projectileSpeed;}}
       [SerializeField] private Sprite _projectileSprite;
       public Sprite ProjectileSprite {get{return _projectileSprite;}}
       [SerializeField] private Sprite _unitSprite;
       public Sprite UnitSprite {get{return _unitSprite;}}
       [SerializeField] private string _unitName;
       public string UnitName {get{return _unitName;}}
        [SerializeField] private float _manaBase = 1;
        private float _mana;
        public float Mana { get { return _mana; } }
        [SerializeField] private float _healthMaxBasic;
        public float HealthMaxBasic { get { return _healthMaxBasic; } }
        [SerializeField] private float _heathMaxAdvMult;
        public float HealthMaxAdvMult => _heathMaxAdvMult;
        [SerializeField] private float _buildTimeBasic = 1f;
        public float BuildTimeBasic => _buildTimeBasic;
        [SerializeField] private float _buildTimeAdvMult = 1;
        public float BuildTimeAdvMult => _buildTimeAdvMult;
       [SerializeField] private float _origionalBaseAttackChargeUp = 1;
       [SerializeField] private float _attackChargeUpBasic;
       public float AttackChargeUpBasic {get{return _attackChargeUpBasic;}}
        [SerializeField] private float _attackChargeUpAdvMult;
        public float AttackChargeUpMult => _attackChargeUpAdvMult;
       [SerializeField] private float   _attackWindDown;
       [SerializeField] private float _origionalBaseAttackWindDown = 1;
       public float AttackWindDown {get {return _attackWindDown;}}
       [SerializeField] private float _baseDamage;
       public float BaseDamage {get{return _baseDamage;}}
       [SerializeField] private float _origionalBaseDPS = 1;
       [SerializeField] private float _baseDPS;
       public float BaseDPS {get{return _baseDPS;}}
       [SerializeField] private float _baseRange;       
       public float BaseRange {get{return _baseRange;}}
       [SerializeField] private float _origionalBaseHealthMax = 1;
       [SerializeField] private float _healthMaxBase;
    //   [SerializeField] private float _typeHealthMaxMultiplier;
    //    [SerializeField] private float _tierHealthMaxMultipleir;
       
       [SerializeField] private float _chrysalisTimerMax;
       public float ChrysalisTimerMax {get {return _chrysalisTimerMax;}}
       
       [SerializeField] private GameObject _hatchEffectPrefab;
       public GameObject HatchEffectPrefab {get {return _hatchEffectPrefab;}}
       [SerializeField] private UnitStatsMultiplierSO _baseMultipler;
       [SerializeField] private UnitStatsMultiplierSO _typeMultipler;
       [SerializeField] private UnitStatsMultiplierSO _tierMultiplier;
       public UnitStatsMultiplierSO TierMultiplier {get {return _tierMultiplier;}}

       
        private void OnValidate()
        {
           Initialize();
        }
        public void Initialize()
        {
            _healthMaxBasic = _origionalBaseHealthMax * _baseMultipler.HealthMultiplier * _typeMultipler.HealthMultiplier * _tierMultiplier.HealthMultiplier;
            _baseDPS = _origionalBaseDPS * _baseMultipler.DPSMultiplier * _typeMultipler.DPSMultiplier * _tierMultiplier.DPSMultiplier;
            _baseRange = _typeMultipler.Range;
            _attackChargeUpBasic = _origionalBaseAttackChargeUp * _baseMultipler.AttackChargeUp * _typeMultipler.AttackChargeUp * _tierMultiplier.AttackChargeUp;
            _attackWindDown = _origionalBaseAttackWindDown * _baseMultipler.AttackWindDown * _typeMultipler.AttackWindDown * _tierMultiplier.AttackWindDown;
            _baseDamage = _baseDPS * (_attackChargeUpBasic + _attackWindDown);
            _mana = _manaBase * _baseMultipler.ManaMultiplier * _typeMultipler.ManaMultiplier * _tierMultiplier.ManaMultiplier;
            if (_upgradeTypeClass == null)
            {
                _upgradeTypeClass = new UpgradeTypeClass();
            }
            _upgradeTypeClass.SetUnitStatsSO(this);
            if (_tierMultiplier.Tier == 0)
            {
                _upgradeTypeClass.SetUpgradeType(UpgradeTypeClass.UpgradeType.Basic);
            }
            else {
                _upgradeTypeClass.SetUpgradeType(UpgradeTypeClass.UpgradeType.Advanced);
            }
        }
    }
}
