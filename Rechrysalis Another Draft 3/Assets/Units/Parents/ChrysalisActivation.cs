using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ChrysalisActivation : MonoBehaviour
    {
        private bool _debugBool = false;
        private ParentUnitManager _parentUnitManager;
        private UnitActivation _unitActivation;
        private ParentHealth _parentHealth;
        private TargetScoreValue _targetScoreValue;
        private ProgressBarManager _progressBarManager;
        [SerializeField] private HilightRingParentManager _hilightRingParentManager;
        public HilightRingParentManager HilightRingParentManager { get => _hilightRingParentManager; set => _hilightRingParentManager = value; }
        
        
        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _unitActivation = GetComponent<UnitActivation>();
            _parentHealth = GetComponent<ParentHealth>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
            _progressBarManager = GetComponent<ProgressBarManager>();
        }
        public void DeactivateChrysalis(int _chryslisIndex)
        {
            if (_parentUnitManager.SubChrysalii[_chryslisIndex] != null)
            {
                if (_parentUnitManager.SubChrysalii[_chryslisIndex].activeInHierarchy == true)
                {
                    _parentUnitManager.SubChrysalii[_chryslisIndex].SetActive(false);
                }
            }
            if (_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.SubChrysalii[_chryslisIndex]))
            {
                _parentUnitManager.TheseUnits.ActiveUnits.Remove(_parentUnitManager.SubChrysalii[_chryslisIndex]);
            }
        }
        public void ActivateChrysalis(int _chrysalisIndex)
        {
            if (_parentUnitManager.SubChrysalii[_chrysalisIndex] == null) return;
            // if ((_chrysalisIndex == 0) && (_parentUnitManager.CurrentSubUnit != _parentUnitManager._subUnits[0])) return;
            // if (_parentUnitManager.CurrentSubUnit != _parentUnitManager.SubChrysalii[_chrysalisIndex])
            {
                _parentHealth.SetChrysalis(true);

                _parentHealth.SetMaxHealth(_parentUnitManager.SubUnits[_chrysalisIndex].GetComponent<UnitManager>().UnitClass.HPMax);
                float _timeToKeep = 0;
                ChrysalisTimer _chrysalisTimer = _parentUnitManager.CurrentSubUnit.GetComponent<ChrysalisTimer>();
                if (_chrysalisTimer != null)
                {
                    _timeToKeep = _chrysalisTimer.TimerCurrent;
                }
                for (int _indexInSubChrysalis = 0; _indexInSubChrysalis < _parentUnitManager.SubChrysalii.Length; _indexInSubChrysalis++)
                {
                    if (_indexInSubChrysalis == _chrysalisIndex)
                    {
                        _parentUnitManager.CurrentSubUnit = _parentUnitManager.SubChrysalii[_chrysalisIndex];
                        if (_debugBool)
                        {
                            Debug.Log($"activating chrysalis" + _chrysalisIndex);
                        }
                        _parentUnitManager.SubChrysalii[_chrysalisIndex].SetActive(true);
                        _parentHealth.CurrentUnit = _parentUnitManager.SubChrysalii[_chrysalisIndex].GetComponent<UnitManager>();
                        _parentUnitManager.SubChrysalii[_chrysalisIndex].GetComponent<ChrysalisTimer>()?.StartThisChrysalis(_timeToKeep);
                        if (!_parentUnitManager.TheseUnits.ActiveUnits.Contains(_parentUnitManager.SubChrysalii[_chrysalisIndex]))
                        {
                            _parentUnitManager.TheseUnits.ActiveUnits.Add(_parentUnitManager.SubChrysalii[_chrysalisIndex]);
                        }
                    }
                    else DeactivateChrysalis(_indexInSubChrysalis);
                }
                for (int _unitIndex = 0; _unitIndex < _parentUnitManager.SubUnits.Length; _unitIndex++)
                {
                    _unitActivation.DeactivateUnit(_unitIndex);
                }
                _progressBarManager?.TintChrysalis();
            }
            _targetScoreValue.SetEgg(true);
        }
    }
}
