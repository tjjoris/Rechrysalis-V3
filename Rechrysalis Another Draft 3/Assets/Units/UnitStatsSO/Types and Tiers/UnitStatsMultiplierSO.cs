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
    }
}
