using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Rechrysalis.Unit;
using Rechrysalis.AdvancedUpgrade;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectManager : MonoBehaviour
    {
        private bool _debugBool = false;
        private int _parentIndex;
        private int _unitIndex;
        [SerializeField] private UnitManager _unitManager;
        public UnitManager UnitManager => _unitManager;
        [SerializeField] private ParentUnitManager _parentUnitManager;
        public ParentUnitManager ParentUnitManager => _parentUnitManager;
        [SerializeField] private ControllerManager _controllerManager;
        public ControllerManager ControllerManager => _controllerManager;
        [SerializeField] private HatchEffectSO _hatchEffectSO;
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        public UpgradeTypeClass UpgradeTypeClass => _upgradeTypeClass;
        private HETimer _hETimer;        
        private HatchEffectHealth _hEHealth;
        public HatchEffectHealth HEHealth => _hEHealth;
        private bool _affectAll = true;
        public bool AffectAll {get{return _affectAll;}}
        [SerializeField] private float _HEHealthMax;
        public float HEHealthMax => _HEHealthMax;
        [SerializeField] private float _hatchMult;
        public float HatchMult => _hatchMult;
        [SerializeField] private float _hatchDurationMult;
        [SerializeField] private float _manaCostIncrease;
        public float ManaCostIncrease => _manaCostIncrease;
        [SerializeField] private float _buildTimeIncrease;
        public float BuildTimeIncrease => _buildTimeIncrease;
        private HEDisplay _hEDisplay;
        [SerializeField] private TMP_Text _name;
        // private float _maxHP;
        // private float _currentHP;
        [SerializeField] private float _hpDrainPerTick = 1f;
        private int _tier;
        [SerializeField] private HEIncreaseDamage _hEIncreaseDamage;
        public HEIncreaseDamage HEIncreaseDamage => _hEIncreaseDamage;
        [SerializeField] private HEIncreaseDefence _hEIncreaseDefence;
        public HEIncreaseDefence HEIncreaseDefence => _hEIncreaseDefence;
        [SerializeField] private OnHatchAOEManager _onHatchAOEManager;
        public OnHatchAOEManager OnHatchAOEManager => _onHatchAOEManager;
        private  List<HEIconChangeColour> _heIconChangeColour;
        public List<HEIconChangeColour> HEIconChangeColour => _heIconChangeColour;
        [SerializeField] private UnitClass _unitClass;
        public Action<GameObject, int, int, bool> _hatchEffectDies;
        [SerializeField] private float _burstHealAmount;
        public float BurstHealAmount => _burstHealAmount;


        private void OnValidate()
        {
            SetUpAwake();
        }
        private void Awake()
        {
            // _upgradeTypeClass.HatchEffectManager = this;
            SetUpAwake();
            _heIconChangeColour = new List<HEIconChangeColour>();
        }
        private void SetUpAwake()
        {

            _upgradeTypeClass.HatchEffectManager = this;
            _hETimer = GetComponent<HETimer>();
            _hEDisplay = GetComponent<HEDisplay>();
            _hEHealth = GetComponent<HatchEffectHealth>();
            _hEIncreaseDamage = GetComponent<HEIncreaseDamage>();
            _hEIncreaseDefence = GetComponent<HEIncreaseDefence>();
            _onHatchAOEManager = GetComponent<OnHatchAOEManager>();
        }
        public void Initialize(HatchEffectSO hatchEffectSO, int _parentIndex, int _unitIndex, bool _affectAll, UnitClass advUnitClass, UnitManager unitManager, ControllerManager controllerManager, ParentUnitManager pum)
        {
            _parentUnitManager = pum;
            SetUpAwake();
            _controllerManager = controllerManager;
            _unitClass = advUnitClass;
            if (_debugBool)
            {
                Debug.Log($"HE Initialize " + hatchEffectSO.HatchEffectName +  " tier " + _tier);
            }
            this._parentIndex = _parentIndex;
            this._unitIndex = _unitIndex;
            this._affectAll = _affectAll;
            // this._tier = _tier;
            if (hatchEffectSO != null) this._hatchEffectSO = hatchEffectSO;
            // _hETimer = GetComponent<HETimer>();
            // _hEDisplay = GetComponent<HEDisplay>();
            // _hEHealth = GetComponent<HatchEffectHealth>();
            // if (_hatchEffectSO.HealthMax.Length > this._tier)
            {
            // _maxHP = _hatchEffectSO.HealthMax[_tier];
            _unitManager = unitManager;
            _hatchMult = advUnitClass.HatchEffectMult;
            _hatchDurationMult = advUnitClass.HatchEffectDurationAdd;
            _hEHealth.Initialize(_hatchMult, advUnitClass, _HEHealthMax);
            }
            // _hEIncreaseDamage = GetComponent<HEIncreaseDamage>();
            _hEIncreaseDamage?.Initialize(_hatchMult);
            // _hEIncreaseDefence = GetComponent<HEIncreaseDefence>();
            _hEIncreaseDefence?.Initialize(_hatchMult);
            _onHatchAOEManager?.Initialize(_controllerManager);
            // DisplayUnitHEIcon displayUnitHEIcon = _parentUnitManager.GetComponent<DisplayUnitHEIcon>();
            // _heIconChangeColour = displayUnitHEIcon.GetHEIconChangeColours(this);
            InitializeIconAndSetColour(null);
            
        }
        // public void SetUpUnit(UnitManager unit)
        // {
        //     AddBurstHeal(unit);
        // }
        // private void AddBurstHeal(UnitManager unit)
        // {
        //     if (_burstHealAmount == 0) return;
        //     unit.gameObject.AddComponent<BurstHealManager>();
        // }
        public void InitializeIconAndSetColour(List<HEIconChangeColour> heIconChangeColours)
        {
            DisplayUnitHEIcon displayUnitHEIcon = _parentUnitManager.GetComponent<DisplayUnitHEIcon>();
            _heIconChangeColour = displayUnitHEIcon.GetHEIconChangeColours(this);
            // _heIconChangeColour = heIconChangeColours;
            foreach (HEIconChangeColour heIconChangeColour in _heIconChangeColour)
            {
                heIconChangeColour.SetColourToActive();
            }
        }
        
        public void SetOffset(int _multiplier)
        {
            _hEDisplay?.PositionOffset(_multiplier);
        }       
        public void Tick(float _timeAmount)
        {
            TakeDamage(_timeAmount * _hpDrainPerTick);
            _onHatchAOEManager?.Tick(_timeAmount);
        } 
        public void TakeDamage(float _damageAmount)
        {
            _hEHealth?.TakeDamage(_damageAmount);
            if (!_hEHealth.CheckIfAlive())
            {
                // foreach (HEIconChangeColour heIconChangeColour in _heIconChangeColour)
                // {
                //     heIconChangeColour.SetColorToInactive();
                // }   
                SetHEIconToInactive();             
                _hatchEffectDies?.Invoke(gameObject, _parentIndex, _unitIndex, _affectAll);
            }            
        }
        public void SetHEIconToInactive()
        {
            foreach (HEIconChangeColour heIconChangeColour in _heIconChangeColour)
            {
                heIconChangeColour.SetColorToInactive();
            }
        }
    }
}
