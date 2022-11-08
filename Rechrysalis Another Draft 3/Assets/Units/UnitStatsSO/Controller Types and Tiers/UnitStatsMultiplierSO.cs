using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Unit/UnitStatsMultiplier", fileName ="UnitStatsMultiplier")]
    public class UnitStatsMultiplierSO : ScriptableObject
    {
        [SerializeField] private float _healthMultiplier = 1;
        public float HealthMultiplier {get{return _healthMultiplier;}}
        [SerializeField] private float _dpsMultiplier = 1;
        public float DPSMultiplier {get{return _dpsMultiplier;}}
        [SerializeField] private float _range = 5;
        public  float Range {get{return _range;}}
        [SerializeField] private float  _attackChargeUp = 1;
        public float AttackChargeUp {get{return _attackChargeUp;}}
        [SerializeField] private float _attackWindDown = 1;
        public float AttackWindDown {get{return _attackWindDown;}}
        [SerializeField] private float _manaMultiplier = 1;
        public float ManaMultiplier {get {return _manaMultiplier;}}
        [SerializeField] private int _tier;        
        public int Tier {get {return _tier;}}
    }
}
