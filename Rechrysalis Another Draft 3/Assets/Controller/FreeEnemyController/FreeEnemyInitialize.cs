using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using UnityEngine.SceneManagement;
using Rechrysalis.CompCustomizer;
using Rechrysalis.UI;

namespace Rechrysalis.Controller
{
    public class FreeEnemyInitialize : MonoBehaviour
    {
        private bool _debugBool  = false;
        private MainManager _mainManager;
        private LevelSceneManagement _levelSceneManagement;
        [SerializeField] private GameObject _FreeUnitPrefab;
        private int _controllerIndex;
        private ControllerManager _enemyController;
        private ControllerFreeUnitHatchEffectManager _controllerFreeHatch;
        private CompSO _compSO;
        private PlayerUnitsSO _playerUnitsSO;
        public PlayerUnitsSO PlayerUnitsSO {get => _playerUnitsSO; set => _playerUnitsSO = value;}
        private CompsAndUnitsSO _compsAndUnits;
        private FreeUnitCompSO _freeUnitCompSO;
        private CompCustomizerSO _compCustomizer;
        private ControllerManager _controllerManager;
        private ControllerHealth _controllerHealth;
        [SerializeField] private FreeControllerControllerProgressBar _freeControllerControllerProgressBar;
        public FreeControllerControllerProgressBar FreeControllerControllerProgressBar => _freeControllerControllerProgressBar;
        // private int _controllerIndex;
        private List<GameObject> _allUnits;       
        private Transform _targetCameraScrollTransform;
        private int _waveIndex;
        private int _unitInWaveIndex = 0;
        private float _lifeToSpendOnThisWave;
        private RandomizeFreeChangingUnits _randomizeFreeChangingUnits;
        private FreeEnemyWaveGenerator _freeEnemyWaveGenerator;
        private FreeEnemyWaveInstantiator _freeEnemyWaveInstantiator;
        private FreeEnemyUnitInstantiator _freeEnemyUnitInstantiator;
        private LifePerFreeWave _lifePerFreeWave;
        [SerializeField] private WaveLayoutsByRange _waveLayoutsByRange;
        public WaveLayoutsByRange WaveLayoutsByRange => _waveLayoutsByRange;
        [SerializeField] private List<WaveClass> _waveClassList = new List<WaveClass>();
        public List<WaveClass> WaveClassList => _waveClassList;
        [SerializeField] private ControllerUnitsSO _controllerUnitsToChooseFrom;
        public ControllerUnitsSO ControllerUnitsToChooseFrom => _controllerUnitsToChooseFrom;
        [SerializeField] private ControllerUnitsSO _currentChangingControllerUnits;
        public ControllerUnitsSO CurrentChangingControllerUnits => _currentChangingControllerUnits;

        private void Awake()
        {
            _controllerManager = GetComponent<ControllerManager>();
            _controllerHealth = GetComponent<ControllerHealth>();
            _lifePerFreeWave = GetComponent<LifePerFreeWave>();
            _freeEnemyWaveGenerator = GetComponent<FreeEnemyWaveGenerator>();
            _freeEnemyWaveInstantiator = GetComponent<FreeEnemyWaveInstantiator>();
            _freeEnemyUnitInstantiator = GetComponent<FreeEnemyUnitInstantiator>();
            _controllerFreeHatch = GetComponent<ControllerFreeUnitHatchEffectManager>();
            _randomizeFreeChangingUnits = GetComponent<RandomizeFreeChangingUnits>();

        }
        public void Initialize(int controllerIndex, ControllerManager enemyController, CompSO compSO, PlayerUnitsSO playerUnitsSO, CompsAndUnitsSO compsAndUnits, FreeUnitCompSO freeUnitCompSO, CompCustomizerSO compCustomizer, MainManager mainManager)        
        {
            _mainManager = mainManager;
            _levelSceneManagement = mainManager.GetComponent<LevelSceneManagement>();
            _targetCameraScrollTransform = _mainManager.TargetCameraScrollTransform;
            // _lifePerFreeWave?.Initialize();         
            this._controllerIndex = controllerIndex;
            this._enemyController = enemyController;
            this._compSO = compSO;
            this._playerUnitsSO = playerUnitsSO;
            this._compsAndUnits = compsAndUnits;
            this._freeUnitCompSO = freeUnitCompSO;
            this._compCustomizer = compCustomizer;
            _freeEnemyWaveGenerator?.Initialize(_compsAndUnits);
            _freeEnemyWaveInstantiator?.Initialize(_compsAndUnits, _levelSceneManagement);
            _freeEnemyUnitInstantiator?.Initialize(_mainManager, _controllerManager, _enemyController);
            _controllerHealth?.IncreaseMaxHealth(_compsAndUnits.Level * _compsAndUnits.FreeUnitControllerLifeGainedPerLevel);
            // _freeControllerControllerProgressBar.Initialize(_controllerHealth.HealthMax);
            _freeControllerControllerProgressBar.LevelSceneManagement = _mainManager.LevelSceneManagement;
            
            _allUnits = new List<GameObject>();
            // this._controllerIndex = _controllerIndex;
            if (_debugBool)
            {
                Debug.Log("unit SO array size " + compSO.UnitSOArray.Length.ToString());
            }
            playerUnitsSO.InitializePlayerUnitsSize(compSO.UnitSOArray.Length);
            // _waveIndex = _compsAndUnits.Level;

            compsAndUnits.FreeUnitCompSO[controllerIndex] = compsAndUnits.Levels[compsAndUnits.Level];
            _freeUnitCompSO = compsAndUnits.FreeUnitCompSO[controllerIndex];

            _randomizeFreeChangingUnits?.Initialize(_compsAndUnits);
            _randomizeFreeChangingUnits?.RandomizeChangingUnitsFunc(_compsAndUnits.Level);
            _freeEnemyWaveGenerator?.GenerateWaves();
            _freeControllerControllerProgressBar.Initialize(_freeEnemyWaveGenerator.ProgressMaxForThisLevel);
            _freeEnemyWaveInstantiator?.CreateWave();
            if (freeUnitCompSO.Waves.Length > 0)
            {
                // for (int _waveIndex = 0; _waveIndex < _freeUnitCompSO.Waves.Length; _waveIndex++)
                _waveIndex = 0;
                // {
                    // CreateWave(controllerIndex, enemyController, compSO, playerUnitsSO, compsAndUnits, _freeUnitCompSO, _waveIndex);
                // }
            }
            AddNextWaveAction();
            FreeUnitHatchEffect[] _freeUnitHatchEffects = new FreeUnitHatchEffect[_allUnits.Count];
            for (int _unitIndex = 0; _unitIndex < _allUnits.Count; _unitIndex++)
            {                
                _freeUnitHatchEffects[_unitIndex] = _allUnits[_unitIndex].GetComponent<FreeUnitHatchEffect>();
            }
            // _controllerFreeHatch?.SetFreeHatches(_freeUnitHatchEffects);
            // _controllerFreeHatch?.SubscribeToUnits();
            RestartUnits();
        }

        // private void CreateWave(int controllerIndex, ControllerManager enemyController, CompSO compSO, PlayerUnitsSO playerUnitsSO, CompsAndUnitsSO compsAndUnits, FreeUnitCompSO freeUnitCompSO, int waveIndex)
        // {
        //     // if (_compSO.UnitSOArray.Length > 0) {
        //         // WaveSO wave = freeUnitCompSO.Waves[waveIndex];
        //     // if (wave.UnitInWave.Length > 0)
        //     {
        //         _targetCameraScrollTransform.localPosition = Vector2.zero;
        //         // _controllerFreeHatch?.InitializeUnitsArray(wave.UnitInWave.Length);
        //         // for (int i = 0; i < _compSO.UnitSOArray.Length; i++)
        //         // wave.ParentUnitClasses = new List<ParentUnitClass>();
        //         // wave.ParentUnitClasses.Clear();
        //         _unitInWaveIndex = 0;
        //         _lifeToSpendOnThisWave = _lifePerFreeWave.GetLifeToSpendOnThisWave(_compsAndUnits.Level, waveIndex);
        //         if (_lifeToSpendOnThisWave > _controllerHealth.HealthCurrent)
        //         {
        //             _lifeToSpendOnThisWave = _controllerHealth.HealthCurrent;
        //         }
        //         if (_debugBool)
        //         {
        //             Debug.Log($"life to spend on this wave " + _lifeToSpendOnThisWave);
        //         }
        //         // for (int _unitInWaveIndex = 0; _unitInWaveIndex < freeUnitCompSO.Waves[waveIndex].UnitInWave.Length; _unitInWaveIndex++)
        //         {
        //             CreateUnit();
        //         }
        //     }
        // }

        // private void CreateUnit()
        // {
        //     UnitStatsSO unitStats = _randomizeFreeChangingUnits.GetARandomUnitFromChangings();
        //     if (unitStats != null)
        //     {

        //         ParentUnitClass parentUnitClass = new ParentUnitClass();
        //         parentUnitClass.Initialize(_compsAndUnits);
        //         parentUnitClass.ClearAllUpgrades();
        //         parentUnitClass.SetUTCBasicUnit(unitStats.UpgradeTypeClass);
        //         parentUnitClass.SetAllStats();
        //         float controllerLifeCostOfUnit = parentUnitClass.BasicUnitClass.ControllerLifeCostMult * _compsAndUnits.FreeUnitToControllerLifeLostMult;
        //         if (_debugBool)
        //         {
        //             Debug.Log($"controller cost of unit " + controllerLifeCostOfUnit);
        //         }
        //         if ((_lifeToSpendOnThisWave >= controllerLifeCostOfUnit) && (_unitInWaveIndex <= _freeUnitCompSO.WaveLayout.UnitInWave.Length -1))
        //         {
        //             // _wave.ParentUnitClasses.Add(parentUnitClass);                        
        //             // ParentUnitClass parentUnitClass = _wave.ParentUnitClasses[_compSO.ParentUnitClassList.Count - 1];

        //             // Vector3 _newUnitPos = _freeEnemyCompLayout.UnitPos[0, _unitInWaveIndex];
        //             Vector3 _newUnitPos = (_freeUnitCompSO.WaveLayout.GetUnitPosInWave(_unitInWaveIndex) + _enemyController.gameObject.transform.position);
        //             _newUnitPos = _waveLayoutsByRange.GetWaveLayoutByRange(parentUnitClass.BasicUnitClass.Range).GetUnitPosInWave(_unitInWaveIndex);
        //             _newUnitPos = new Vector2(_newUnitPos.x, _newUnitPos.y + _enemyController.transform.position.y);
        //             // _newUnitPos.y = _newUnitPos.y + enemyController.gameObject.transform.position.y;                        
        //             GameObject newFreeEnemy = Instantiate(_FreeUnitPrefab, _newUnitPos, Quaternion.identity, gameObject.transform);
        //             newFreeEnemy.transform.Rotate(new Vector3(0, 0, 180f));
        //             _playerUnitsSO.ParentUnits.Add(newFreeEnemy);
        //             newFreeEnemy.name = unitStats.UnitName + " " + _unitInWaveIndex.ToString();
        //             newFreeEnemy.GetComponent<PushBackFromPlayer>()?.Initialize(_enemyController);
        //             // UnitManager _unitManager = newFreeEnemy.GetComponent<UnitManager>();
        //             // newFreeEnemy.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex);   
        //             ParentUnitManager parentUnitManager = newFreeEnemy.GetComponent<ParentUnitManager>();
        //             parentUnitManager?.Initialize(_controllerIndex, _unitInWaveIndex, _playerUnitsSO, transform, null, parentUnitClass, _mainManager);
        //             parentUnitManager.ParentUnitClass = parentUnitClass;
        //             _controllerManager.ParentUnitManagers.Add(parentUnitManager);
        //             if (parentUnitManager.GetComponent<ParentHealth>() != null)
        //             {
        //                 _controllerManager.ParentHealths.Add(parentUnitManager.GetComponent<ParentHealth>());
        //             }
        //             Mover parentUnitMover = parentUnitManager.GetComponent<Mover>();
        //             if (parentUnitMover != null)
        //             {
        //                 _controllerManager.ParentUnitMovers.Add(parentUnitMover);
        //             }
        //             ParentFreeEnemyManager _freeParentManager = newFreeEnemy.GetComponent<ParentFreeEnemyManager>();
        //             // _freeParentManager?.InitializeOld(controllerIndex, _unitStats, compsAndUnits, _unitInWaveIndex, playerUnitsSO);
        //             _freeParentManager?.Initialize(_controllerManager, parentUnitClass.BasicUnitClass, _unitInWaveIndex, _compsAndUnits, _controllerIndex);
        //             parentUnitManager.ChildUnitManagers.Add(_freeParentManager.BasicUnitManager);
        //             newFreeEnemy.GetComponent<ParentHealth>()?.SetMaxHealth(parentUnitClass.BasicUnitClass.HPMax);
        //             newFreeEnemy.GetComponent<Mover>()?.Initialize(_controllerIndex, _mainManager);
        //             _playerUnitsSO.ActiveUnits.Add(newFreeEnemy);
        //             _allUnits.Add(_freeParentManager.UnitManager.gameObject);
        //             // _controllerFreeHatch?.SetUnitsArray(newFreeEnemy, _unitInWaveIndex);
        //             AIFocusFireOnInitialzie(unitStats, parentUnitManager);
        //             // _unitManager?.RestartUnit();
        //             Die die = parentUnitManager.GetComponent<Die>();
        //             if (die != null)
        //             {
        //                 die.ControllerProgressValue = controllerLifeCostOfUnit;
        //                 die.FreeControllerProgressBar = _freeControllerControllerProgressBar;
        //             }
        //             _controllerHealth?.TakeDamage(controllerLifeCostOfUnit);
        //             _lifeToSpendOnThisWave -= controllerLifeCostOfUnit;
        //             _unitInWaveIndex ++;
        //             // CreateUnit                    
        //         }
        //         else
        //         {
        //             if (_controllerHealth.HealthCurrent <= _lifeToSpendOnThisWave)
        //             {
        //                 _controllerHealth?.SetHealthToZero();
        //             }
        //             _lifeToSpendOnThisWave = 0;
        //             if ((_controllerManager.ParentUnitManagers.Count <= 0) && (_controllerHealth.CheckIfHealthZero()))                    
        //             {
        //                 // LevelDone();
        //             }
        //         }
        //         if (_lifeToSpendOnThisWave > 0)
        //         {
        //             CreateUnit();
        //         }
        //     }
        // }

        private static void AIFocusFireOnInitialzie(UnitStatsSO _unitStats, ParentUnitManager parentUnitManager)
        {
           
            foreach (UnitManager childUnit in parentUnitManager.ChildUnitManagers)
            {
                if (childUnit != null)
                {
                    if ((!_unitStats.AIFocusFire))
                    {
                        if ((childUnit.GetComponent<TargetPrioratizeByScore>() != null))
                        {
                            Destroy(childUnit.GetComponent<TargetPrioratizeByScore>());
                        }
                    }
                    else 
                    {
                        childUnit.ControllerUnitSpriteHandler.TintSpriteRed();
                        //this unit has focus fire, and already starts with the focus fire component.
                    }
                }
            }       
        }

        private bool CheckIfLevelDone()
        {
            // if (_waveIndex >= _freeUnitCompSO.Waves.Length)
            if (_controllerHealth.CheckIfHealthZero())
            {
                return true;
            }
            return false;
        }
        private void RestartUnits()
        {
            if (_allUnits.Count > 0) 
            {
                foreach (GameObject _unit in _allUnits)
                {
                    _unit.GetComponent<UnitManager>()?.RestartUnit();
                }
            }
        }
        public void NextWave()
        {
            if (_controllerManager.ParentUnitManagers.Count <= 0)
            {
                _waveIndex ++;
                // if (_freeUnitCompSO.Waves.Length >= _waveIndex)
                {                
                    if (_debugBool)
                    {
                        Debug.Log($"wave index" + _waveIndex + "waves lenght "+ _freeUnitCompSO.Waves.Length);
                    }
                    if (CheckIfLevelDone())
                    {
                        // LevelDone();
                        return;
                    }
                    // CreateWave(_controllerIndex, _enemyController, _compSO, _playerUnitsSO, _compsAndUnits, _freeUnitCompSO, _waveIndex);
                    _freeEnemyWaveInstantiator?.CreateWave();
                    AddNextWaveAction();
                }
            }
        }

        private void LevelDone()
        {
            _compsAndUnits.Level++;
            if (_compsAndUnits.Level < _compsAndUnits.Levels.Length)
            {
                GoToCompCustomizer();
            }
            else
            {
                _compsAndUnits.NewGameStatusEnum = CompsAndUnitsSO.NewGameStatus.Won;
                SceneManager.LoadScene("Start");
            }
            return;
        }

        private void GoToCompCustomizer()
        {
            _compCustomizer.NumberOfUpgrades = 1;
            SceneManager.LoadScene("CompCustomizer");
        }        
        private void OnEnable()
        {
            AddNextWaveAction();
        }
        private void AddNextWaveAction()
        {
            foreach (Transform _childUnit in transform)
            {
                Die _die = _childUnit.GetComponent<Die>();
                if (_die != null) {
                    _die._spawnWaveAction -= NextWave;
                    _die._spawnWaveAction += NextWave;
                }
            }
        }
        private void OnDisable()
        {
            foreach (Transform _childUnit in transform)
            {
                Die _die = _childUnit.GetComponent<Die>();
                if (_die != null)
                {
                    _die._spawnWaveAction -= NextWave;
                }
            }
        }

        public List<GameObject> GetAllUnits()
        {
            return _allUnits;
        }
        // public void AddHatchEffects(List<GameObject> _allUnitsList, GameObject _hatchEffect, int _unitIndex, bool _allUnits)
        // {
        //     for (int _unitInList = 0; _unitInList < _allUnitsList.Count; _unitInList ++)
        //     {
        //         if (_allUnitsList[_unitIndex] != null)
        //         {
        //             if (_unitIndex == _unitInList)
        //             {
        //                 _allUnitsList[_unitInList].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //             }
        //             else if (_allUnits)
        //             {
        //                 _allUnitsList[_unitInList].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //             }
        //         }
        //     }
        // }
    }
}
