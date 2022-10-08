using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis
{
    [System.Serializable]
    [CreateAssetMenu(menuName ="Unit/PlayerUnitsSO", fileName ="PlayerUnits")]
    public class PlayerUnitsSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> _activeUnits;
        public List<GameObject> ActiveUnits {get{return _activeUnits;} set{_activeUnits = value;}}
        [SerializeField] private ControllerManager _controllerManager;
        public ControllerManager ControllerManager {get{return _controllerManager;}}
        
        public void InitializePlayerUnitsSize(int _size)
        {
            _activeUnits = new List<GameObject>();
            _activeUnits.Clear();
        }
    }
}
