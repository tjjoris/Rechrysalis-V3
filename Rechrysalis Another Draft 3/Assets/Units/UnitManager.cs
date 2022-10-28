using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private ControllerUnitSpriteHandler _unitSpriteHandler;
        [SerializeField] private int _controllerIndex;        
        public int ControllerIndex {get{return _controllerIndex;}}
        private int _freeUnitIndex;
        [SerializeField] private UnitStatsSO _unitStats;
        [SerializeField] private TMP_Text _nameText;
        public UnitStatsSO UnitStats {get{return _unitStats;}}
        private Health _health;
        private Mover _mover;
        private Attack _attack;
        private ChrysalisTimer _chrysalisTimer;
        private Rechrysalize _rechrysalize;
        [SerializeField] private bool _isStopped;
        private CompsAndUnitsSO _compsAndUnits;
        private ProjectilesPool _projectilesPool;
        private List<GameObject> _hatchEffects;
        private GameObject _hatchEffectPrefab;
        public GameObject HatchEffectPrefab {get {return _hatchEffectPrefab;}}
        private FreeUnitHatchEffect _freeHatchScript;
        public bool IsStopped 
        {
            set{
                    _isStopped = value;
                    if (_mover != null)
                    {
                    _mover.IsStopped = _isStopped;
                    }
                    if (_attack != null)
                    {
                    _attack.IsStopped = _isStopped;
                    }
                }
            }
        public void Initialize(int _controllerIndex, UnitStatsSO _unitStats, CompsAndUnitsSO _compsAndUnits, int _freeUnitIndex)
        {
            this._controllerIndex = _controllerIndex;
            this._unitStats = _unitStats;
            _unitStats.Initialize();
            this._compsAndUnits = _compsAndUnits;
            GetComponent<ProjectilesPool>()?.CreatePool(_unitStats.AmountToPool, _unitStats.ProjectileSpeed, _unitStats.ProjectileSprite);
            _nameText.text = _unitStats.UnitName;
            _mover = GetComponent<Mover>();
            _attack = GetComponent<Attack>();
            if (_attack != null)  _attack.IsStopped = true;
            _attack?.Initialize(_unitStats);
            _health = GetComponent<Health>();
            _health?.Initialize(_unitStats.HealthMax);
            GetComponent<Die>()?.Initialize(_compsAndUnits, _controllerIndex);
            GetComponent<RemoveUnit>()?.Initialize(_compsAndUnits.PlayerUnits[_controllerIndex], _compsAndUnits.TargetsLists[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Rechrysalize>()?.Initialize(_compsAndUnits.CompsSO[_controllerIndex].ChildUnitCount);            
            GetComponent<InRangeByPriority>()?.Initialize(_compsAndUnits.TargetsLists[_controllerIndex]);
            GetComponent<ClosestTarget>()?.Initialize(_compsAndUnits.PlayerUnits[GetOppositeController.ReturnOppositeController(_controllerIndex)]);
            GetComponent<Range>()?.Initialize(_unitStats);
            _chrysalisTimer = GetComponent<ChrysalisTimer>();
            _rechrysalize = GetComponent<Rechrysalize>();
            _projectilesPool = GetComponent<ProjectilesPool>();
            _hatchEffects = new List<GameObject>();
            _hatchEffectPrefab = _unitStats.HatchEffectPrefab;
            _freeHatchScript = GetComponent<FreeUnitHatchEffect>();
            this._freeUnitIndex = _freeUnitIndex;
            _freeHatchScript?.Initialize(_unitStats.HatchEffectPrefab, _freeUnitIndex);
            _unitSpriteHandler.SetSpriteFunction(_unitStats.UnitSprite);
        }
        public void RestartUnit()
        {
            _health?.RestartUnit();
            _freeHatchScript?.TriggerHatchEffect();
        }
        public void Tick(float _timeAmount)
        {
            // if (gameObject.active == true) 
            // {
                _mover?.Tick(_timeAmount);
                _attack?.Tick(_timeAmount);
                // _projectilesPool?.TickProjectiles(_timeAmount);
                _chrysalisTimer?.Tick(_timeAmount);
            // }
        }
        public bool IsEnemy(int _controllerIndex)
        {
            if (this._controllerIndex != _controllerIndex)
            {
                return true;
            }
            return  false;
        }
        public void SetReserveChrysalis(int _childIndex)
        {
            _rechrysalize?.SetNextEvolved(_childIndex);
        }
        public void RemoveHatchEffect(GameObject _hatchEffect)
        {
            if (_hatchEffects.Contains(_hatchEffect))
            {
                _hatchEffects.Remove(_hatchEffect);
            }
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            if (!_hatchEffects.Contains(_hatchEffect))
            {
                _hatchEffects.Add(_hatchEffect);
            }
        }
        public void ShowUnitText()
        {
            _nameText.gameObject.SetActive(true);
        }
        public void HideUnitText()
        {
            _nameText.gameObject.SetActive(false);
        }
    }
}
