using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "AdvUnitModiferSO" , menuName = "Unit/AdvUnitModifierSO")]
    public class AdvUnitModifierSO : ScriptableObject
    {
        [SerializeField] private string _upgradeName;
        [SerializeField] private float _currencyCost;
        public float CurrencyCost => _currencyCost;        
        public string UpgradeName => _upgradeName;
        [SerializeField] private float _manaAdd;
        public float ManaAdd => _manaAdd;
        [SerializeField] private float _buildTimeAdd;
        public float BuildTimeAdd => _buildTimeAdd;
        [SerializeField] private float _hpMaxAdd;
        public float HPMaxAdd => _hpMaxAdd;
        [SerializeField] private float _rangeAdd;
        public float RangeAdd => _rangeAdd;
        [SerializeField] private float _dpsAdd;
        public float DPSAdd => _dpsAdd;
        [SerializeField] private float _attackChargeUpAdd;
        public float AttackChargeUpAdd => _attackChargeUpAdd;
        [SerializeField] private float _attackWindDownAdd;
        public float AttackWindDownAdd => _attackWindDownAdd;
        [SerializeField] private float _hatchEffectMultiplierAdd;
        public float HatchEffectMultiplierAdd => _hatchEffectMultiplierAdd;

    }
}
