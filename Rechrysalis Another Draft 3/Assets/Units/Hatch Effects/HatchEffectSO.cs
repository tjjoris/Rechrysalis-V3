using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    [System.Serializable]
    [CreateAssetMenu (menuName = "Unit/HatchEffectsSO", fileName = "new Hatch Efect")]
    public class HatchEffectSO : ScriptableObject
    {
        [SerializeField] private string _hatchEffectName;
        public string HatchEffectName {get {return _hatchEffectName;}}
    }
}
