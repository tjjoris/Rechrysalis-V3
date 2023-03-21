using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    public class UnitClass
    {

        private bool debugBool = true;
        
        [SerializeField] private float _manaCost;
        public float ManaCost {get => _manaCost; set => _manaCost = value;}
        [SerializeField] private string _unitName;
        public string UnitName { get => _unitName; set => _unitName = value; }
        [SerializeField] private float _hpMax;       
        public float HPMax {get => _hpMax; set => _hpMax = value;}
        [SerializeField] private float _buildTime;
        public float BuildTime {get => _buildTime; set => _buildTime = value;}
        [SerializeField] private float _range;
        public float Range {get => _range; set => _range = value;}
        [SerializeField] private float _dps;
        public float DPS {get => _dps; set => _dps = value; }
        [SerializeField] private float _attackChargeUp;
        public float AttackChargeUp {get => _attackChargeUp; set  =>_attackChargeUp = value; }
        [SerializeField] private float _attackWindDown;
        public float AttackWindDown {get => _attackWindDown; set => _attackWindDown = value; }
        [SerializeField] private float _damage;
        public float Damamge {get => _damage; set => _damage = value; }
        [SerializeField] private List<GameObject> _hatchEffectPrefab = new List<GameObject>();
        public List<GameObject> HatchEffectPrefab {get => _hatchEffectPrefab; set => _hatchEffectPrefab = value; }
        [SerializeField] private List<HatchEffectClass> _hatchEffectClasses = new List<HatchEffectClass>();
        public List<HatchEffectClass> HatchEffectClasses { get => _hatchEffectClasses; set => _hatchEffectClasses = value; }
        
        [SerializeField] private float _hatchEffectMult;
        public float HatchEffectMult {get => _hatchEffectMult; set => _hatchEffectMult = value; }
        [SerializeField] private float _hatchEffectDurationAdd;
        public float HatchEffectDurationAdd { get => _hatchEffectDurationAdd; set => _hatchEffectDurationAdd = value; }        
        [SerializeField] private Sprite _unitSprite;
        public Sprite UnitSprite { get => _unitSprite; set => _unitSprite = value; }
        [SerializeField] private Sprite _chrysalisSprite;
        public Sprite ChrysalisSprite { get => _chrysalisSprite; set => _chrysalisSprite = value; }
        [SerializeField] private int _amountToPool;
        public int AmountToPool { get => _amountToPool; set => _amountToPool = value; }
        [SerializeField] private float _projectileSpeed;
        public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }
        [SerializeField] private float _sacrificeControllerAmount;
        public float SacrificeControllerAmount { get => _sacrificeControllerAmount; set => _sacrificeControllerAmount = value; }
        [SerializeField] private float _moveSpeedAdd;
        public float MoveSpeedAdd { get => _moveSpeedAdd; set => _moveSpeedAdd = value; }
        [SerializeField] private float _siegeDuration;
        public float SiegeDuration { get => _siegeDuration; set => _siegeDuration = value; }
        [SerializeField] private float _burstHeal;
        public float BurstHeal { get => _burstHeal; set => _burstHeal = value; }        
        [SerializeField] private Sprite _projectileSprite;
        [SerializeField] private float _controllerLifeCostMult = 1;
        public float ControllerLifeCostMult { get => _controllerLifeCostMult; set => _controllerLifeCostMult = value; }
        
        public Sprite ProjectileSprite { get => _projectileSprite; set => _projectileSprite = value; }
        
        
        
        public void CalculateDamge()
        {
            _damage = _dps * (_attackChargeUp + _attackWindDown);
        }
    }
}
