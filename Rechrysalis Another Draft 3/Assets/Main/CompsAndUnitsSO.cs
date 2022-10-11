using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;

namespace Rechrysalis
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CompsAndUnitsSO", menuName ="Main/CompsAndUnitsSO")]
    public class CompsAndUnitsSO : ScriptableObject
    {
        [SerializeField] private CompSO[] _compsSO;
        public CompSO[] CompsSO {set{_compsSO = value;}get{return _compsSO;}}
        [SerializeField] private ControllerManager[] _controllerManagers;
        public ControllerManager[] ControllerManagers {set{_controllerManagers = value;} get{return _controllerManagers;}}
        [SerializeField] private TargetsListSO[] _targetsLists;
        public TargetsListSO[] TargetsLists {get{return _targetsLists;}}
        [SerializeField] private PlayerUnitsSO[] _playerUnits;
        public PlayerUnitsSO[] PlayerUnits {get{return _playerUnits;}}
        [SerializeField] private UnitStatsSO _chrysalis;
        public UnitStatsSO Chrysalis {get{return _chrysalis;}}
        
        public void Initialize(CompSO[] _compSO, ControllerManager _controllerMangerOne, ControllerManager _ControllerManagerTwo)
        {
            
                        this._controllerManagers = new ControllerManager[_compSO.Length];

        }
    }
}
