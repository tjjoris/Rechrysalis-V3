using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    [System.Serializable]
    public class HatchEffectClass
    {
        [SerializeField] private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab {get => _hatchEffectPrefab; set => _hatchEffectPrefab = value;}        
        // [SerializeField] private float _hatchEffectHealth;
        // public float HatchEffectHealth {get => _hatchEffectHealth; set => _hatchEffectHealth = value;}
    }
}
