using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Unit/UnitStatsMultiplier", fileName ="UnitStatsMultiplier")]
    public class UnitStatsMultiplierSO : ScriptableObject
    {
        [SerializeField] private float _multiplier = 1;
        public float Multiplier {get{return _multiplier;}}
    }
}
