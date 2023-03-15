using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "AdvUnitModiferSO" , menuName = "Unit/AdvUnitModifierSO")]
    public class AdvUnitModifierSO : ScriptableObject
    {
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        public UpgradeTypeClass UpgradeTypeClass => _upgradeTypeClass;
        [SerializeField] private string _upgradeName;
        public string UpgradeName => _upgradeName;
        [SerializeField] private float _currencyCost;
        public float CurrencyCost => _currencyCost;   
        [SerializeField] private float _manaAdd;
        public float ManaAdd => _manaAdd;
        [SerializeField] private float _manaMult = 1f;
        public float ManaMult => _manaMult;
        [SerializeField] private float _buildTimeAdd;
        public float BuildTimeAdd => _buildTimeAdd;
        [SerializeField] private float _buildTimeMult = 1f;
        public float BuildTimeMult => _buildTimeMult;
        [SerializeField] private float _hpMaxAdd;
        public float HPMaxAdd => _hpMaxAdd;
        [SerializeField] private float _hpMaxMult = 1f;
        public float HPMaxMult => _hpMaxMult;
        [SerializeField] private float _rangeAdd;
        public float RangeAdd => _rangeAdd;
        [SerializeField] private float _dpsAdd;
        public float DPSAdd => _dpsAdd;
        [SerializeField] private float _dpsMult = 1f;
        public float DPSMult => _dpsMult;
        [SerializeField] private float _attackChargeUpAdd;
        public float AttackChargeUpAdd => _attackChargeUpAdd;
        [SerializeField] private float _attackChargeUpMult = 1f;
        public float AttackChargeUpMult => _attackChargeUpMult;
        [SerializeField] private float _attackWindDownAdd;
        public float AttackWindDownAdd => _attackWindDownAdd;
        [SerializeField] private float _attackWindDownMult = 1f;
        public float AttackWindDownMult => _attackWindDownMult;
        [SerializeField] private float _hatchEffectMultiplierAdd;
        public float HatchEffectMultiplierAdd => _hatchEffectMultiplierAdd;
        // [SerializeField] private float _hatchEffectMultiplierMult;
        // public float HatchEffectMultiplierMult => _hatchEffectMultiplierMult;
        [SerializeField] private float _hatchEffectDurationAdd;
        public float HatchEffectDurationAdd => _hatchEffectDurationAdd;
        [SerializeField] private float _sacrificeControllerAmount = 0;
        public float SacrificeControllerAmount => _sacrificeControllerAmount;
        [SerializeField] private float _moveSpeedAdd = 0;
        public float MoveSpeedAdd => _moveSpeedAdd;


        private void OnValidate()
        {
            Initialize();
        }
        public void Initialize()
        {
            _upgradeTypeClass = new UpgradeTypeClass();
            _upgradeTypeClass.SetAdvUnitModifierSO(this);
            _upgradeTypeClass.SetUpgradeType(UpgradeTypeClass.UpgradeType.Advanced);
        }
    }
}
