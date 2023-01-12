using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewUnitStats", menuName ="Unit/UnitStatsSO")]
    public class UnitStatsSO : ScriptableObject
    {
        bool debugBool = false;
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
       [SerializeField] private Sprite _chrysalisSprite;
       public Sprite ChrysalisSprite => _chrysalisSprite;
       [SerializeField] private string _unitName;
       public string UnitName {get{return _unitName;}}
       [SerializeField] private float _currencyCost;
       public float CurrencyCost => _currencyCost;
        [SerializeField] private float _manaBase = 1;
        private float _mana;
        public float Mana { get { return _mana; } }
        [SerializeField] private float _origionalBaseHealthMaxBasic = 1;
        [SerializeField] private float _healthMaxBase;
        [SerializeField] private float _healthMaxBasic;
        public float HealthMaxBasic { get { return _healthMaxBasic; } }
        [SerializeField] private float _heathMaxAdvMult;
        public float HealthMaxAdvMult => _heathMaxAdvMult;
        [SerializeField] private float _buildTimeBasic = 1f;
        public float BuildTimeBasic => _buildTimeBasic;
        [SerializeField] private float _buildTimeBasicBase = 1;
        [SerializeField] private float _buildTimeAdvMult = 1;
        public float BuildTimeAdvMult => _buildTimeAdvMult;
       [SerializeField] private float _origionalBaseAttackChargeUp = 1;
       [SerializeField] private float _attackChargeUpBasic;
       public float AttackChargeUpBasic {get{return _attackChargeUpBasic;}}
        [SerializeField] private float _attackChargeUpAdvMult;
        public float AttackChargeUpMult => _attackChargeUpAdvMult;
        [SerializeField] private float _origionalBaseAttackWindDown = 1;
       [SerializeField] private float   _attackWindDownBasic;
       public float AttackWindDownBasic {get {return _attackWindDownBasic;}}
       [SerializeField] private float _attackWindDownAdvMult;
       public float AttackWindDownMult => _attackWindDownAdvMult;
        [SerializeField] private float _origionalBaseDPSBasic = 1;
        [SerializeField] private float _baseDPSBasic;
        public float BaseDPSBasic { get { return _baseDPSBasic; } }
        [SerializeField] private float _baseDPSAdvMult;
        public float BaseDPSAdvMult => _baseDPSAdvMult;
       [SerializeField] private float _baseDamageBasic;
       public float BaseDamageBasic {get{return _baseDamageBasic;}}
       [SerializeField] private float _baseDamageAdvMult;
       public float BaseDamageAdvMult => _baseDamageAdvMult;
       [SerializeField] private float _baseRangeBasic;       
       public float BaseRangeBasic {get{return _baseRangeBasic;}}
       [SerializeField] private float _baseRangeAdvMult;
       public float BaseRangeAdvMult => _baseRangeAdvMult;
       
    //   [SerializeField] private float _typeHealthMaxMultiplier;
    //    [SerializeField] private float _tierHealthMaxMultipleir;
       
       [SerializeField] private float _chrysalisTimerMax;
       public float ChrysalisTimerMax {get {return _chrysalisTimerMax;}}
       
       [SerializeField] private AdvUnitModifierSO _advUnitModifierSO;
       public AdvUnitModifierSO AdvUnitModifierSO => _advUnitModifierSO;
       
       [SerializeField] private GameObject _hatchEffectPrefab;
       public GameObject HatchEffectPrefab {get {return _hatchEffectPrefab;}}
       [SerializeField] private UnitStatsMultiplierSO _baseMultipler;
       [SerializeField] private UnitStatsMultiplierSO _typeMultipler;
       public UnitStatsMultiplierSO TypeMultiplier => _typeMultipler;
       [SerializeField] private UnitStatsMultiplierSO _tierMultiplier;
       public UnitStatsMultiplierSO TierMultiplier {get {return _tierMultiplier;}}

       
        private void OnValidate()
        {
           Initialize();
        }
        public void Initialize()
        {
            if (debugBool)
            Debug.Log($"Initialize");
            _healthMaxBasic = _origionalBaseHealthMaxBasic * _baseMultipler.HealthMultiplier * _typeMultipler.HealthMultiplier * _tierMultiplier.HealthMultiplier;
            _baseDPSBasic = _origionalBaseDPSBasic * _baseMultipler.DPSMultiplier * _typeMultipler.DPSMultiplier * _tierMultiplier.DPSMultiplier;
            _baseRangeBasic = _typeMultipler.Range;
            _attackChargeUpBasic = _origionalBaseAttackChargeUp * _baseMultipler.AttackChargeUp * _typeMultipler.AttackChargeUp * _tierMultiplier.AttackChargeUp;
            _attackWindDownBasic = _origionalBaseAttackWindDown * _baseMultipler.AttackWindDown * _typeMultipler.AttackWindDown * _tierMultiplier.AttackWindDown;
            _baseDamageBasic = _baseDPSBasic * (_attackChargeUpBasic + _attackWindDownBasic);
            _mana = _manaBase * _baseMultipler.ManaMultiplier * _typeMultipler.ManaMultiplier * _tierMultiplier.ManaMultiplier;
            _buildTimeBasic = _buildTimeBasicBase * _baseMultipler.BuildTimeMultiplier * _typeMultipler.BuildTimeMultiplier * _tierMultiplier.BuildTimeMultiplier;
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
        [Button("$Initialize")]
        private void InitializeButton()
        {
            Initialize();
        }
    }
}
