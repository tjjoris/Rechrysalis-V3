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
        [SerializeField] private bool _timeStopped;
        public bool TimeStopped { get {return _timeStopped;} set {
            _timeStopped = value;
            PauseUnPause(value);
        }}
        [SerializeField] private bool _paused;
        public bool Paused { get {return _paused;} set{
            _paused = value;
            PauseUnPause(value);
        }}
        
        [SerializeField] CompsAndUnitsSO _compsAndUnitsSO;        
        [SerializeField] ControllerManager[] _controllerManager;
        [SerializeField] PlayerUnitsSO[] _playerUnitsSO;  
        [SerializeField] CompSO[] _compSO;

        [SerializeField] CompCustomizerSO _compCustomizer;
        [SerializeField] ProjectilesHolder _projectilesHolder;
        [SerializeField] BackgroundManager _backGroundManager;
        [SerializeField] private LevelDisplay _levelDisplay;
        [SerializeField] private PlayerPrefsInteract _playerPrefsInteract;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Transform _targetCameraScrollTransform;
        public Transform TargetCameraScrollTransform => _targetCameraScrollTransform;
        public EventSystem EventSystem => _eventSystem;
        public PlayerPrefsInteract PlayerPrefsInteract => _playerPrefsInteract;

        private void Awake() {
            // _compsAndUnitsSO.CompsSO = _compSO;
            _compSO = _compsAndUnitsSO.CompsSO;
            _compsAndUnitsSO.ControllerManagers = _controllerManager;
            GameMaster.GetSingleton().ReferenceManager.CompsAndUnitsSO = _compsAndUnitsSO;
            _projectilesHolder.Initialize();
            _levelDisplay?.SetLevelText(_compsAndUnitsSO.Level);
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
            if ((!_timeStopped) && (!_paused))
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
        private void PauseUnPause(bool pauseBool)
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
            _playerPrefsInteract._changePlayerPrefs += UpdatePreferances;
        }
        private void OnDisable()
        {
            _playerPrefsInteract._changePlayerPrefs -= UpdatePreferances;
        }
        private void UpdatePreferances()
        {
            foreach (ControllerManager controllerManager in _controllerManager)
            {
                if (controllerManager != null)
                {
                    if (controllerManager.CheckRayCast != null)
                    {
                        Debug.Log($"set target during target mode" + _playerPrefsInteract.GetTargetOnlyDuringTargetMode());
                    }
                }
            }
        }
    }
}
