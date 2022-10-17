using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewUnitStats", menuName ="Unit/UnitStatsSO")]
    public class UnitStatsSO : ScriptableObject
    {
        [SerializeField] private int _amountToPool;
        public int AmountToPool{get{return _amountToPool;}}
        [SerializeField] private float _projectileSpeed;
        public float ProjectileSpeed {get{return _projectileSpeed;}}
       [SerializeField] private Sprite _projectileSprite;
       public Sprite ProjectileSprite {get{return _projectileSprite;}}
       [SerializeField] private string _unitName;
       public string UnitName {get{return _unitName;}}
       [SerializeField] private float _attackChargeUp;
       public float AttackChargeUp {get{return _attackChargeUp;}}
       [SerializeField] private float   _attackWindDown;
       public float AttackWindDown {get {return _attackWindDown;}}
       [SerializeField] private float _baseDamage;
       public float BaseDamage {get{return _baseDamage;}}
       [SerializeField] private float _baseRange;
       public float BaseRange {get{return _baseRange;}}
       [SerializeField] private float _healthMaxBase;
      [SerializeField] private float _typeHealthMaxMultiplier;
       [SerializeField] private float _tierHealthMaxMultipleir;
       [SerializeField] private float _healthMax;
       public float HealthMax {get{return _healthMax;}}
       [SerializeField] private float _chrysalisTimerMax;
       public float ChrysalisTimerMax {get {return _chrysalisTimerMax;}}
       [SerializeField] private GameObject _hatchEffectPrefab;
       public GameObject HatchEffectPrefab {get {return _hatchEffectPrefab;}}

        public void Initialize()
        {
            _healthMax = _healthMaxBase * _typeHealthMaxMultiplier * _tierHealthMaxMultipleir;
        }
    }
}
