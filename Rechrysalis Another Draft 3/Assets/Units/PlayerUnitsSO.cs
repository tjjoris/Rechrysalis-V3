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
        private List<GameObject> _parentUnits;
        public List<GameObject> ParentUnits {get {return _parentUnits;} set {_parentUnits = value;}}
        
        public void InitializePlayerUnitsSize(int _size)
        {
            _activeUnits = new List<GameObject>();
            _activeUnits.Clear();
            _parentUnits = new List<GameObject>();
            _parentUnits.Clear();
        }
    }
}
