////////using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;
using System;

namespace Rechrysalis.Unit
{
    public class ParentUnitHatchEffects : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private List<GameObject> _hatchEffects;
        public List<GameObject> HatchEffects => _hatchEffects;
        private GameObject[] _subUnits;
        private GameObject[] _subChrysalii;
        public Action<GameObject, int, int, bool> _addHatchEffect;
        public void Initialize (GameObject[] _subUnits, GameObject[] _subchrysalii)
        {
            _parentUnitManager = GetComponent<ParentUnitManager>();
            this._subUnits = _subUnits;
            this._subChrysalii =_subchrysalii;
            _hatchEffects = new List<GameObject>();
            // GetComponent<ParentClickManager>().Initialize(_controllerIndex);
        }

        public void CreateHatchEffect(GameObject _hatchEffectPrefab, int _parentIndex, int _unitIndex, bool _affectAll, float hpMax)
        {
            if ((_hatchEffectPrefab != null) && (_parentUnitManager.SubHatchEffects[_unitIndex] != null))
            {
                GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
                HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
                _hatchEffectManager?.Initialize(_parentUnitManager.SubHatchEffects[_unitIndex], _parentIndex, _unitIndex, _affectAll, _parentUnitManager.ParentUnitClass.AdvUnitClass, hpMax);
                _addHatchEffect?.Invoke(_hatchEffect, _parentIndex, _unitIndex, _hatchEffectManager.AffectAll);
            }
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            _hatchEffects.Add(_hatchEffect);
            for (int _index = 0; _index < _hatchEffects.Count; _index ++)
            {
                _hatchEffects[_index].GetComponent<HEDisplay>().PositionOffset(_index);
            }
            // foreach (GameObject _unit in _subUnits)
            // {
            //     _unit.GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
            // }
            // foreach (GameObject _chrysalis in _subChrysalii)
            // {
            //     _chrysalis.GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
            // }
        }
        public void RemoveHatchEffect(GameObject _hatchEffect)
        {
            if (_hatchEffects.Contains(_hatchEffect))
            {
                _hatchEffects.Remove(_hatchEffect);
            }
            for (int _index = 0; _index < _hatchEffects.Count; _index++)
            {
                _hatchEffects[_index].GetComponent<HEDisplay>().PositionOffset(_index);
            }
        }
        public void TakeDamage(float _damage)
        {
            if ((_hatchEffects.Count > 0) && (_hatchEffects[0] != null))
            {
                // Debug.Log($"take damage " + _damage);
                _hatchEffects[0].GetComponent<HatchEffectManager>().TakeDamage(_damage);
            }
        }
    }
}
