using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;
using Rechrysalis.Attacking;
using Rechrysalis.Background;
using Rechrysalis.CompCustomizer;
using Rechrysalis.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Rechrysalis
{
    public class MainManager : MonoBehaviour
    {
        private bool _debugBool = false;
        private PauseScript _pauseScript;
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;  
        public CompsAndUnitsSO CompsAndUnitsSO => _compsAndUnitsSO;    
        [SerializeField] ControllerManager[] _controllerManager;
        [SerializeField] PlayerUnitsSO[] _playerUnitsSO;  
        [SerializeField] CompSO[] _compSO;
        [SerializeField] private LevelSceneManagement _levelSceneManagement;
        public LevelSceneManagement LevelSceneManagement { get => _levelSceneManagement; set => _levelSceneManagement = value; }        
        [SerializeField] CompCustomizerSO _compCustomizer;
        [SerializeField] ProjectilesHolder _projectilesHolder;
        [SerializeField] BackgroundManager _backGroundManager;
        [SerializeField] private LevelDisplay _levelDisplay;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Transform _targetCameraScrollTransform;
        public Transform TargetCameraScrollTransform => _targetCameraScrollTransform;
        public EventSystem EventSystem => _eventSystem;

        private void Awake()
        {
            _levelSceneManagement = GetComponent<LevelSceneManagement>();
            _pauseScript = GetComponent<PauseScript>();

        }
        private void Start() {
            // _compsAndUnitsSO.CompsSO = _compSO;
            _levelSceneManagement?.Initialize(_compsAndUnitsSO);
            _compSO = _compsAndUnitsSO.CompsSO;
            _compsAndUnitsSO.ControllerManagers = _controllerManager;
            GameMaster.GetSingleton().ReferenceManager.CompsAndUnitsSO = _compsAndUnitsSO;
            _projectilesHolder.Initialize();
            _levelDisplay?.SetLevelText(_compsAndUnitsSO.Level);
            if (_debugBool) Debug.Log($"main start initializing controllers");
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i=0; i<_controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null) 
                    {
                        _controllerManager[i].Initialize(i, _playerUnitsSO, _compSO[i], _controllerManager[GetOppositeController.ReturnOppositeController(i)], _compsAndUnitsSO, _compCustomizer, this, _graphicRaycaster, _targetCameraScrollTransform);
                    }
                }
            }
            _backGroundManager?.Initialize();
            UpdatePreferances();
        }
        private void FixedUpdate()
        {
            if (!_pauseScript.IsPaused())
            {
                _backGroundManager.Tick();
                ResetTick();
                Tick();
            }
        }
        private void ResetTick()
        {
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i = 0; i < _controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null)
                    {
                        _controllerManager[i].ResetTick();
                    }
                }
            }
        }
        private void Tick()
        {
            float _timeAmount = Time.fixedDeltaTime;
            if ((_controllerManager != null) && (_controllerManager.Length > 0))
            {
                for (int i = 0; i < _controllerManager.Length; i++)
                {
                    if (_controllerManager[i] != null)
                    {
                        _controllerManager[i].FixedTick(_timeAmount);
                    }
                }
            }
            _projectilesHolder?.Tick(_timeAmount);
        }
        public void PauseUnPause(bool pauseBool)
        {
            foreach (ControllerManager controllerManager in _controllerManager)
            {
                if (controllerManager != null)
                {
                    controllerManager.PauseUnPause(pauseBool);
                }
            }
        }
        private void OnEnable()
        {
            PlayerPrefsInteract._changePlayerPrefs += UpdatePreferances;
        }
        private void OnDisable()
        {
            PlayerPrefsInteract._changePlayerPrefs -= UpdatePreferances;
        }
        private void UpdatePreferances()
        {
            foreach (ControllerManager controllerManager in _controllerManager)
            {
                if (controllerManager != null)
                {
                    if (controllerManager.CheckRayCast != null)
                    {
                        if (_debugBool) Debug.Log($"set target during target mode" + PlayerPrefsInteract.GetTargetOnlyDuringTargetMode());
                    }
                }
            }
        }
    }
}
