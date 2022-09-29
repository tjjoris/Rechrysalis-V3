using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    [System.Serializable]
    [CreateAssetMenu(menuName ="Unit/PlayerUnitsSO", fileName ="PlayerUnits")]
    public class PlayerUnitsSO : ScriptableObject
    {
        [SerializeField] private GameObject[] _activeUnits;
        public GameObject[] ActiveUnits {get{return _activeUnits;} set{_activeUnits = value;}}
    }
}
