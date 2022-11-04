using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.CompCustomizer
{
    public class UpgradeButtonHorizontalLayoutManager : MonoBehaviour
    {
        [SerializeField] private UpgradeButtonManager[] _upgradeButtonManagerArray;
        public UpgradeButtonManager[] UpgradeButtonManagerArray {get {return _upgradeButtonManagerArray;}}
        private UnitStatsSO _basicUnitSO;
        public UnitStatsSO BasicUnitSO {get {return _basicUnitSO;}}
        private UnitStatsSO _advUnitSO;
        public UnitStatsSO AdvUnitSO {get {return _advUnitSO;}}
        private HatchEffectSO _hatchEffectSO;
        public HatchEffectSO HatchEffectSO {get {return _hatchEffectSO;}}

        [SerializeField] private CompCustomizerSO _compCustomizerSO;

        public void Initialize (CompCustomizerSO _compCustomizerSO, UnitStatsSO _basicUnitNotToPick, UnitStatsSO _advUnitNotToPick, HatchEffectSO _hatchEfectNotToPick, Color _basicColour, Color _advColour, Color _hatchColour)
        {
            this._compCustomizerSO = _compCustomizerSO;
            CheckTOPickABasicUnit(_basicUnitNotToPick);
            CheckToPickAAdvUnit(_advUnitNotToPick);
            CheckToPickHatchEffect(_hatchEfectNotToPick);
            _upgradeButtonManagerArray[0].Initialize(_basicUnitSO, null);
            _upgradeButtonManagerArray[0].SetBackGColour(_basicColour);
            _upgradeButtonManagerArray[1].Initialize(_advUnitSO, null);
            _upgradeButtonManagerArray[1].SetBackGColour(_advColour);
            _upgradeButtonManagerArray[2].Initialize(null, _hatchEffectSO);
            _upgradeButtonManagerArray[2].SetBackGColour(_hatchColour);

        }
        private HatchEffectSO CheckToPickHatchEffect (HatchEffectSO _hatchEffectNotToPick)
        {
            _hatchEffectSO = PickAHatchEffect();
            return _hatchEffectSO;            
        }
        private HatchEffectSO PickAHatchEffect ()
        {
            int _availableHatchEffectIndex = UnityEngine.Random.Range(0, _compCustomizerSO.ArrayOfAvailableHatchEffects.Length);
            return _compCustomizerSO.ArrayOfAvailableHatchEffects[_availableHatchEffectIndex];
        }
        private UnitStatsSO CheckToPickAAdvUnit (UnitStatsSO _advUnitNotToPick)
        {
            _advUnitSO = PickAAdvUnit();
            while (_advUnitSO == _advUnitNotToPick)
            {
                _advUnitSO = PickABasicUnit();
            }
            return _advUnitSO;
        }
        private UnitStatsSO PickAAdvUnit()
        {
            int _availableAdvUnitIndex = UnityEngine.Random.Range(0, _compCustomizerSO.ArrayOfAvailableAdvUnits.Length);
            return _compCustomizerSO.ArrayOfAvailableAdvUnits[_availableAdvUnitIndex];
        }
        private UnitStatsSO CheckTOPickABasicUnit(UnitStatsSO _basicUnitNotToPick)
        {
            _basicUnitSO = PickABasicUnit();
            while (_basicUnitSO == _basicUnitNotToPick)
            {
                _basicUnitSO = PickABasicUnit();
            }
            return _basicUnitSO;
        }
        private UnitStatsSO PickABasicUnit()
        {
            int _availableBasicUnitIndex = UnityEngine.Random.Range(0, _compCustomizerSO.ArrayOfAvailableBasicUnits.Length);
            return _compCustomizerSO.ArrayOfAvailableBasicUnits[_availableBasicUnitIndex];
        }
    }
}