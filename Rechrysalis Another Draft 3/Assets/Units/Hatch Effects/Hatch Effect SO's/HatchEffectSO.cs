using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    [System.Serializable]
    [CreateAssetMenu (menuName = "Unit/HatchEffectsSO", fileName = "new Hatch Efect")]
    public class HatchEffectSO : ScriptableObject
    {
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        public UpgradeTypeClass UpgradeTypeClass { get{ return _upgradeTypeClass; } set{ _upgradeTypeClass = value; } }
        [SerializeField] private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab => _hatchEffectPrefab;
        [SerializeField] private float _currencyCost;
        public float CurrencyCost => _currencyCost;
        [SerializeField] private float  _addedManaCost;
        public float  AddedManaCost => _addedManaCost;
        [SerializeField] private float[] _dpsIncrease;
        public float[] DPSIncrease {get {return _dpsIncrease;}}
        [SerializeField] private float[] _incomingDamageMultiplier;
        public float[] IncomingDamageMultiplier {get {return _incomingDamageMultiplier;}}
        [SerializeField] private float[] _rangeIncrease;
        public float[] RangeIncrase {get { return _rangeIncrease;}}
        [SerializeField] private float[] _speedDecrease;
        public float[] SpeedDecrease {get {return _speedDecrease;}}
        [SerializeField] private float[] _aoeDamage;
        public float[] AoEDamge {get{return _aoeDamage;}}
        [SerializeField] private float[] _manaMultiplier;
        public float[] ManaMultiplier {get {return _manaMultiplier;}}        
        [SerializeField] private float[] _hpHealed;
        public float[] HPHealed {get {return _hpHealed;}}
        [SerializeField] private float[] _hpRegenPerTick;
        public float[] HPRegenPerTIck {get { return _hpRegenPerTick;}}
        [SerializeField] private float[] _damageLossPerTick;
        public float[] DamageLossPerTick {get {return _damageLossPerTick;}}
        [SerializeField] private float[] _healthMax;
        public float[] HealthMax { get { return _healthMax; } }
        [SerializeField] private bool[] _affectAll;
        public bool[] AffectAll {get{return _affectAll;}}


        [SerializeField] private string _hatchEffectName;
        public string HatchEffectName {get {return _hatchEffectName;}}

        private void OnValidate()
        {
            if (_upgradeTypeClass == null)
            {
                _upgradeTypeClass = new UpgradeTypeClass();
            }
            _upgradeTypeClass.SetUpgradeType(UpgradeTypeClass.UpgradeType.HatchEffect);
            _upgradeTypeClass.SetHatchEffectSO(this);
        }
    }
}
