using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;
using UnityEngine.SceneManagement;
using Rechrysalis.CompCustomizer;

namespace Rechrysalis.Controller
{
    public class FreeEnemyInitialize : MonoBehaviour
    {
        private bool _debugBool  = false;
        private MainManager _mainManager;
        // [SerializeField] private FreeUnitLayoutSO _freeEnemyCompLayout;
        [SerializeField] private GameObject _FreeUnitPrefab;
        private int _controllerIndex;
        private ControllerManager _enemyController;
        private ControllerFreeUnitHatchEffectManager _controllerFreeHatch;
        private CompSO _compSO;
        private PlayerUnitsSO _playerUnitsSO;
        private CompsAndUnitsSO _compsAndUnits;
        private FreeUnitCompSO _freeUnitCompSO;
        private CompCustomizerSO _compCustomizer;
        private ControllerManager _controllerManager;
        private ControllerHealth _controllerHealth;
        // private int _controllerIndex;
        private List<GameObject> _allUnits;       
        private Transform _targetCameraScrollTransform;
        private int _waveIndex;

        [SerializeField] private WaveLayoutsByRange _waveLayoutsByRange;
        public WaveLayoutsByRange WaveLayoutsByRange => _waveLayoutsByRange;
        [SerializeField] private ControllerUnitsSO _controllerUnitsToChooseFrom;
        public ControllerUnitsSO ControllerUnitsToChooseFrom => _controllerUnitsToChooseFrom;
        [SerializeField] private ControllerUnitsSO _currentChangingControllerUnits;
        public ControllerUnitsSO CurrentChangingControllerUnits => _currentChangingControllerUnits;
        public void Initialize(int controllerIndex, ControllerManager enemyController, CompSO compSO, PlayerUnitsSO playerUnitsSO, CompsAndUnitsSO compsAndUnits, FreeUnitCompSO freeUnitCompSO, CompCustomizerSO compCustomizer, MainManager mainManager)        
        {
            _mainManager = mainManager;
            _targetCameraScrollTransform = _mainManager.TargetCameraScrollTransform;
            _controllerManager = GetComponent<ControllerManager>();
            _controllerHealth = GetComponent<ControllerHealth>();            
            this._controllerIndex = controllerIndex;
            this._enemyController = enemyController;
            this._compSO = compSO;
            this._playerUnitsSO = playerUnitsSO;
            this._compsAndUnits = compsAndUnits;
            this._freeUnitCompSO = freeUnitCompSO;
            _controllerHealth?.IncreaseMaxHealth(_compsAndUnits.Level * _compsAndUnits.FreeUnitControllerLifeGainedPerLevel);
            this._compCustomizer = compCustomizer;
            _controllerFreeHatch = GetComponent<ControllerFreeUnitHatchEffectManager>();
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
            if (freeUnitCompSO.Waves.Length > 0)
            {
                // for (int _waveIndex = 0; _waveIndex < _freeUnitCompSO.Waves.Length; _waveIndex++)
                _waveIndex = 0;
                // {
                    CreateWave(controllerIndex, enemyController, compSO, playerUnitsSO, compsAndUnits, _freeUnitCompSO, _waveIndex);
                // }
            }
            AddNextWaveAction();
            FreeUnitHatchEffect[] _freeUnitHatchEffects = new FreeUnitHatchEffect[_allUnits.Count];
            for (int _unitIndex = 0; _unitIndex < _allUnits.Count; _unitIndex++)
            {                
                _freeUnitHatchEffects[_unitIndex] = _allUnits[_unitIndex].GetComponent<FreeUnitHatchEffect>();
            }
            _controllerFreeHatch?.SetFreeHatches(_freeUnitHatchEffects);
            _controllerFreeHatch?.SubscribeToUnits();
            RestartUnits();
        }

        private void CreateWave(int controllerIndex, ControllerManager enemyController, CompSO compSO, PlayerUnitsSO playerUnitsSO, CompsAndUnitsSO compsAndUnits, FreeUnitCompSO freeUnitCompSO, int waveIndex)
        {
            // if (_compSO.UnitSOArray.Length > 0) {
                WaveSO wave = freeUnitCompSO.Waves[waveIndex];
            if (wave.UnitInWave.Length > 0)
            {
                _targetCameraScrollTransform.localPosition = Vector2.zero;
                _controllerFreeHatch?.InitializeUnitsArray(wave.UnitInWave.Length);
                // for (int i = 0; i < _compSO.UnitSOArray.Length; i++)
                wave.ParentUnitClasses = new List<ParentUnitClass>();
                wave.ParentUnitClasses.Clear();
                for (int _unitInWaveIndex = 0; _unitInWaveIndex < freeUnitCompSO.Waves[waveIndex].UnitInWave.Length; _unitInWaveIndex++)
                {
                    UnitStatsSO _unitStats = wave.UnitInWave[_unitInWaveIndex];
                    if (_unitStats != null)
                    {
                        
                        ParentUnitClass parentUnitClass = new ParentUnitClass();
                        parentUnitClass.ClearAllUpgrades();
                        parentUnitClass.SetUTCBasicUnit(_unitStats.UpgradeTypeClass);
                        parentUnitClass.SetAllStats();
                        float controllerLifeCostOfUnit = parentUnitClass.BasicUnitClass.ControllerLifeCostMult * _compsAndUnits.FreeUnitToControllerLifeLostMult;
                        if (_controllerHealth.CheckIfHasEnoughHealth(controllerLifeCostOfUnit))
                        {                            
                            // _wave.ParentUnitClasses.Add(parentUnitClass);                        
                            // ParentUnitClass parentUnitClass = _wave.ParentUnitClasses[_compSO.ParentUnitClassList.Count - 1];

                            // Vector3 _newUnitPos = _freeEnemyCompLayout.UnitPos[0, _unitInWaveIndex];
                            Vector3 _newUnitPos = (freeUnitCompSO.WaveLayout.GetUnitPosInWave(_unitInWaveIndex) + enemyController.gameObject.transform.position);
                            // _newUnitPos.y = _newUnitPos.y + enemyController.gameObject.transform.position.y;                        
                            GameObject newFreeEnemy = Instantiate(_FreeUnitPrefab, _newUnitPos, Quaternion.identity, gameObject.transform);
                            newFreeEnemy.transform.Rotate(new Vector3(0, 0, 180f));
                            playerUnitsSO.ParentUnits.Add(newFreeEnemy);
                            newFreeEnemy.name = _unitStats.name + " " + _unitInWaveIndex.ToString();
                            newFreeEnemy.GetComponent<PushBackFromPlayer>()?.Initialize(enemyController);
                            // UnitManager _unitManager = newFreeEnemy.GetComponent<UnitManager>();
                            // newFreeEnemy.GetComponent<UnitManager>()?.Initialize(_controllerIndex, _unitStats, _compsAndUnits, _unitInWaveIndex);   
                            ParentUnitManager parentUnitManager = newFreeEnemy.GetComponent<ParentUnitManager>();
                            parentUnitManager?.Initialize(_controllerIndex, _unitInWaveIndex, compSO, playerUnitsSO, transform, null, parentUnitClass, _mainManager);
                            parentUnitManager.ParentUnitClass = parentUnitClass;
                            _controllerManager.ParentUnitManagers.Add(parentUnitManager);
                            if (parentUnitManager.GetComponent<ParentHealth>() != null)
                            {
                                _controllerManager.ParentHealths.Add(parentUnitManager.GetComponent<ParentHealth>());
                            }
                            Mover parentUnitMover = parentUnitManager.GetComponent<Mover>();
                            if (parentUnitMover != null)
                            {
                                _controllerManager.ParentUnitMovers.Add(parentUnitMover);
                            }
                            ParentFreeEnemyManager _freeParentManager = newFreeEnemy.GetComponent<ParentFreeEnemyManager>();
                            // _freeParentManager?.InitializeOld(controllerIndex, _unitStats, compsAndUnits, _unitInWaveIndex, playerUnitsSO);
                            _freeParentManager?.Initialize(_controllerManager, parentUnitClass.BasicUnitClass, _unitInWaveIndex, compsAndUnits, _controllerIndex);
                            parentUnitManager.ChildUnitManagers.Add(_freeParentManager.BasicUnitManager);
                            newFreeEnemy.GetComponent<ParentHealth>()?.SetMaxHealth(parentUnitClass.BasicUnitClass.HPMax);
                            newFreeEnemy.GetComponent<Mover>()?.Initialize(controllerIndex, _mainManager);
                            playerUnitsSO.ActiveUnits.Add(newFreeEnemy);
                            _allUnits.Add(_freeParentManager.UnitManager.gameObject);
                            _controllerFreeHatch?.SetUnitsArray(newFreeEnemy, _unitInWaveIndex);
                            AIFocusFireOnInitialzie(_unitStats, parentUnitManager);
                            // _unitManager?.RestartUnit();
                            _controllerHealth?.TakeDamage(controllerLifeCostOfUnit);
                        }
                        else
                        {
                            _controllerHealth?.SetHealthToZero();
                        }
                    }
                }
            }
        }

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

        private bool CheckIfLevelDone(int _waveIndex)
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
            _waveIndex ++;
            // if (_freeUnitCompSO.Waves.Length >= _waveIndex)
            {                
                if (_debugBool)
                {
                    Debug.Log($"wave index" + _waveIndex + "waves lenght "+ _freeUnitCompSO.Waves.Length);
                }
                if (CheckIfLevelDone(_waveIndex))
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
                CreateWave(_controllerIndex, _enemyController, _compSO, _playerUnitsSO, _compsAndUnits, _freeUnitCompSO, _waveIndex);
                AddNextWaveAction();
            }
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
