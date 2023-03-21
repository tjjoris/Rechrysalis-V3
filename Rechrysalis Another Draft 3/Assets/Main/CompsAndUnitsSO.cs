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
        public enum NewGameStatus {NewGame, Lost, Won};
        [SerializeField] private NewGameStatus _newGameStatusEnum;
        public NewGameStatus NewGameStatusEnum { get => _newGameStatusEnum; set => _newGameStatusEnum = value; }
        
        [SerializeField] private FreeUnitCompSO[] _levels;
        public FreeUnitCompSO[] Levels {get{return _levels;}}
        [SerializeField] private float _speed;
        public float Speed {get {return _speed;}}
        [SerializeField] private int _level;
        public int Level {get {return _level;} set {_level = value;}}
        [SerializeField] private CompSO[] _compsSO;
        public CompSO[] CompsSO {set{_compsSO = value;}get{return _compsSO;}}
        [SerializeField] private FreeUnitCompSO[] _freeUnitCompSO;
        public FreeUnitCompSO[] FreeUnitCompSO {get { return _freeUnitCompSO;}}
        [SerializeField] private ControllerManager[] _controllerManagers;
        public ControllerManager[] ControllerManagers {set{_controllerManagers = value;} get{return _controllerManagers;}}
        [SerializeField] private TargetsListSO[] _targetsLists;
        public TargetsListSO[] TargetsLists {get{return _targetsLists;}}
        [SerializeField] private PlayerUnitsSO[] _playerUnits;
        public PlayerUnitsSO[] PlayerUnits {get{return _playerUnits;}}
        [SerializeField] private UnitStatsSO _chrysalis;
        public UnitStatsSO Chrysalis {get{return _chrysalis;}}
        [SerializeField] private float[] _controllerHealth;
        public float[] ControllerHealth {get{return _controllerHealth;}}
        [SerializeField] private int _controllerHPTokensCurrent = 3;
        [SerializeField] private float _freeUnitToControllerLifeLostMult = 2;          
        public float FreeUnitToControllerLifeLostMult => _freeUnitToControllerLifeLostMult;
        [SerializeField] private float _freeUnitControllerLifeGainedPerLevel = 5;
        public float FreeUnitControllerLifeGainedPerLevel => _freeUnitControllerLifeGainedPerLevel;
        public int ControllerHPTokensCurrent {get {return _controllerHPTokensCurrent;} set {_controllerHPTokensCurrent = value;}}
        [SerializeField] private int _controllerHPTokensMax = 3;
        public int ControllerHPTokensMax => _controllerHPTokensMax;
        // [SerializeField] private List<GameObject> _projectilesList;
        // public List<GameObject> ProjectilesList {set {_projectilesList = value;} get{return _projectilesList;}}
        
        public void Initialize(CompSO[] _compSO, ControllerManager _controllerMangerOne, ControllerManager _ControllerManagerTwo)
        {
            _controllerHPTokensCurrent = _controllerHPTokensMax;
            
                        this._controllerManagers = new ControllerManager[_compSO.Length];

        }        
        public void AddControllerHPTokens(int tokensToAdd)
        {
            _controllerHPTokensCurrent += tokensToAdd;
            if (_controllerHPTokensCurrent > _controllerHPTokensMax)
            {
                _controllerHPTokensCurrent = _controllerHPTokensMax;
            }
        }
    }
}
