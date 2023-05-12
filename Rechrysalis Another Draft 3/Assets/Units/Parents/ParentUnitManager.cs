using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using System;
using TMPro;
using Rechrysalis.Movement;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ParentUnitManager : MonoBehaviour
    {
        private bool _debugBool = false;
        private ControllerManager _controllerManager;
        public ControllerManager ControllerManager => _controllerManager;
        private ControllerManager _enemyControllerManager;
        public ControllerManager EnemyControllerManager => _enemyControllerManager;
        [SerializeField] private ParentUnitClass _parentUnitClass;
        public ParentUnitClass ParentUnitClass {get => _parentUnitClass; set => _parentUnitClass = value; }
        private int _parentIndex;
        public int ParentIndex => _parentIndex;
        [SerializeField] private int _controllerIndex;
        [SerializeField] private GameObject[] _subUnits;
        public GameObject[] SubUnits {get {return _subUnits;}set {_subUnits = value;}}
        [SerializeField] private GameObject[] _subChrysalii;
        [SerializeField] private List<UnitManager> _childUnitManagers;
        public List<UnitManager> ChildUnitManagers { get => _childUnitManagers; set => _childUnitManagers = value; }
        
        public GameObject[] SubChrysalii {get{return _subChrysalii;}set {_subChrysalii = value;}}
        // private List<HatchEffectManager> _hatchEffectManagersToDamage;
        [SerializeField] private List<UnitManager> _childChrysaliiUnitManagers;
        public List<UnitManager> ChildChrysaliiUnitManagers { get => _childChrysaliiUnitManagers; set => _childChrysaliiUnitManagers = value; }
        
        
        private List<HatchEffectSO> _subHatchEffects; 
        public List<HatchEffectSO> SubHatchEffects {get { return _subHatchEffects;}}       
        private PlayerUnitsSO _theseUnits;
        public PlayerUnitsSO TheseUnits {get {return _theseUnits;}}
        [SerializeField] private GameObject _currentSubUnit;
        public GameObject CurrentSubUnit {get { return _currentSubUnit;} set { _currentSubUnit = value;} }
        private RotateParentUnit _rotateParentUnit;
        private ParentHealth _parentHealth;
        public ParentHealth ParentHealth => _parentHealth;
        private ParentUnitHatchEffects _parentUnitHatchEffects;
        public ParentUnitHatchEffects ParentUnitHatchEffects => _parentUnitHatchEffects;
        private ParentHatchEffectAddRemove _parentHatchEffectAddRemove;
        
        private UnitActivation _unitActivation;
        public UnitActivation UnitActivation => _unitActivation;
        private ChrysalisActivation _chrysalisActivation;
        public ChrysalisActivation ChrysalilsActivation => _chrysalisActivation;
        private UpgradeUnit _upgradeUnit;
        private TargetScoreValue _targetScoreValue;
        public TargetScoreValue TargetScoreValue => _targetScoreValue;
        private ParentFreeEnemyManager _parentFreeEnemyManager;
        private AIFlawedUpdate _aiFlawedUpdate;
        private AIAlwaysPreferClosest _aiAlwaysPreferClosest;
        private FreeEnemyKiteMaxRange _freeEnemyKiteMaxRange;
        private FreeEnemyApproach _freeApproach;
        private PriorityScoreChrysalis _priorityScoreChrysalis;
        private bool _aiCanMove = true;
        public bool AICanMove {get {return _aiCanMove;} set {_aiCanMove = value;}}
        private Mover _parentUnitMover;
        public Mover ParentUnitMover => _parentUnitMover;
        // private float _manaAmount;
        // public float ManaAmount {set{_manaAmount = value;} get {return _manaAmount;} }
        [SerializeField] private ProgressBarManager _progressBarManager;
        public ProgressBarManager ProgressBarManager => _progressBarManager;
        [SerializeField] private HilightRingParentManager _hilightRingParentManager;
        public HilightRingParentManager HilightRingParentManager { get => _hilightRingParentManager; set => _hilightRingParentManager = value; }
        
        public Action<GameObject, int, int, bool> _addHatchEffect;
        public Action<GameObject, int, bool> _removeHatchEffect;
        public Action<float> _parentDealsDamage;
        public Action<float> _subtractMana;
        // [SerializeField] private TMP_Text _manaText;

        [SerializeField] private bool _isStopped;
        public bool IsStopped 
        {
            set{
                _isStopped = value;
            }
            get {return _isStopped;}
         }

        private void Awake()
        {

            _unitActivation = GetComponent<UnitActivation>();
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
            _upgradeUnit = GetComponent<UpgradeUnit>();
            _parentHealth = GetComponent<ParentHealth>();
            _rotateParentUnit = GetComponent<RotateParentUnit>();
            _parentUnitHatchEffects = GetComponent<ParentUnitHatchEffects>();
            _parentHatchEffectAddRemove = GetComponent<ParentHatchEffectAddRemove>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _aiFlawedUpdate = GetComponent<AIFlawedUpdate>();
            _freeEnemyKiteMaxRange = GetComponent<FreeEnemyKiteMaxRange>();
            _freeApproach = GetComponent<FreeEnemyApproach>();
            _parentFreeEnemyManager = GetComponent<ParentFreeEnemyManager>();
            _parentUnitMover = GetComponent<Mover>();
            _priorityScoreChrysalis = GetComponent<PriorityScoreChrysalis>();
            _progressBarManager = GetComponent<ProgressBarManager>();
            _unitActivation = GetComponent<UnitActivation>();
        }
        public void Initialize(int _controllerIndex, int _parentUnitIndex, PlayerUnitsSO _theseUnits, Transform controllertransform, List<HatchEffectSO> subHatchEffects, ParentUnitClass parentUnitClass, MainManager mainManager)
        {
            _controllerManager = controllertransform.GetComponent<ControllerManager>();
            _enemyControllerManager = _controllerManager.EnemyController;
            _childUnitManagers = new List<UnitManager>();
            _childChrysaliiUnitManagers = new List<UnitManager>();
            _unitActivation?.Initialize(this);
            _chrysalisActivation?.Initialize(this);
            _upgradeUnit?.Initialize(this, _controllerManager.GetComponent<ManaGenerator>());
            this._parentIndex = _parentUnitIndex;
            this._subHatchEffects = subHatchEffects;
            this._controllerIndex = _controllerIndex;
            this._theseUnits = _theseUnits;
            _parentUnitClass = parentUnitClass; 
            _parentHealth?.Initialize();          
            _rotateParentUnit?.Initialize(controllertransform);
            _parentHatchEffectAddRemove?.Initialzie(this);
            GetComponent<ParentClickManager>()?.Initialize(_controllerIndex);
            _freeApproach?.Initialize(_theseUnits, _enemyControllerManager.GetComponent<Mover>());
            _parentUnitMover?.Initialize(_controllerIndex, mainManager);
            _priorityScoreChrysalis?.Initialize(_enemyControllerManager);
        }
        public void Tick(float timeAmount)
        {
            TickChildUnits(timeAmount);
        }
        public void AIFlawedUpdateActivated()
        {
            if (_debugBool)
            {
                Debug.Log($"working???");
            }
            TargetPrioratizeByScore targetPrioratizeByScore = _currentSubUnit.GetComponent<TargetPrioratizeByScore>();
            targetPrioratizeByScore?.SetTargetByScore();
            bool _isRetreating = false;
            _aiAlwaysPreferClosest = _currentSubUnit.GetComponent<AIAlwaysPreferClosest>();
            _aiAlwaysPreferClosest?.CheckIfTargetInRange();
            if (_freeEnemyKiteMaxRange != null)
            {
                _freeEnemyKiteMaxRange?.Tick(_aiCanMove);
                _isRetreating = _freeEnemyKiteMaxRange.GetRetreating();
            }
            if (_freeApproach != null)
            {
                _freeApproach?.Tick(_isRetreating, _aiCanMove);
                if ((!_isRetreating) && (!_freeApproach.GetIsApproaching()))
                {
                    _parentUnitMover.SetDirection(Vector2.zero);
                }
            }   
        }
        private void TickChildUnits(float timeAmount)
        {
            foreach (UnitManager childUnit in _childUnitManagers)
            {
                if (childUnit.gameObject.activeInHierarchy)
                {
                    childUnit.Tick(timeAmount);
                }
            }
            foreach (UnitManager childChrysalii in _childChrysaliiUnitManagers)
            {
                if (childChrysalii.gameObject.activeInHierarchy)
                {
                    childChrysalii.Tick(timeAmount);
                }
            }
        }
        private void OnEnable()
        {
            AddChrysalisAndUnitActions();
        }
        public void AddChrysalisAndUnitActions()
        {
            foreach (Transform _child in transform)
            {                
                ChrysalisTimer _chryslisTimer = _child.GetComponent<ChrysalisTimer>();
                if (_chryslisTimer != null)
                {
                    _chryslisTimer._startUnit -= _unitActivation.ActivateUnit;
                    _chryslisTimer._startUnit += _unitActivation.ActivateUnit;
                }
            }
            _parentHealth = GetComponent<ParentHealth>();
            if (_chrysalisActivation != null)
            {
                _parentHealth._unitDies -= _chrysalisActivation.ActivateChrysalis;
                _parentHealth._unitDies += _chrysalisActivation.ActivateChrysalis;
            }
            if ((_subUnits != null) && (_subUnits.Length>0))
            {
                for (int _subIndex = 0; _subIndex < _subUnits.Length; _subIndex ++)
                {
                    if (_subUnits[_subIndex] != null)
                    {
                        _subUnits[_subIndex].GetComponent<UnitManager>()._unitDealsDamage -= ParentDealsDamage;
                        _subUnits[_subIndex].GetComponent<UnitManager>()._unitDealsDamage += ParentDealsDamage;
                    }
                }
            }
        }
        private void OnDisable()
        {
            foreach (GameObject _chrysalis in _subChrysalii)
            {
                if (_chrysalis != null)
                {
                    _chrysalis.GetComponent<ChrysalisTimer>()._startUnit -= _unitActivation.ActivateUnit;
                }
            }
            if (_chrysalisActivation != null)
            {
                _parentHealth._unitDies -= _chrysalisActivation.ActivateChrysalis;
            }
            if ((_subUnits != null) && (_subUnits.Length > 0))
            {
                for (int _subIndex = 0; _subIndex < _subUnits.Length; _subIndex++)
                {
                    if (_subUnits[_subIndex] != null)
                    {
                        _subUnits[_subIndex].GetComponent<UnitManager>()._unitDealsDamage -= ParentDealsDamage;
                    }
                }
            }      
        }
        private void ParentDealsDamage(float _damage)
        {
            _parentDealsDamage?.Invoke(_damage);
        }
    }
}
