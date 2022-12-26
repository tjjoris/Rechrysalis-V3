using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    [System.Serializable]
    public class UnitClass
    {

        private bool debugBool = true;
        
        [SerializeField] private float _manaCost;
        public float ManaCost {get => _manaCost; set => _manaCost = value;}
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
        [SerializeField] private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab {get => _hatchEffectPrefab; set => _hatchEffectPrefab = value; }
        [SerializeField] private float _hatchEffectMult;
        public float HatchEffectMult {get => _hatchEffectMult; set => _hatchEffectMult = value; }
        [SerializeField] private Sprite _unitSprite;
        public Sprite UnitSprite { get => _unitSprite; set => _unitSprite = value; }
        

        public void CalculateDamge()
        {
            _damage = _dps / (_attackChargeUp + _attackWindDown);
        }
    }
}
