using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    public class FreeUnitHatchEffect : MonoBehaviour
    {
        private GameObject _hatchEffectPrefab;
        private int _unitIndex;
        // private bool _effectAll;
        public Action<GameObject, int, bool> _addHatchEffect;
        public void Initialize(GameObject _hatchEffectPrefab, int _unitIndex)
        {
            this._hatchEffectPrefab = _hatchEffectPrefab;
            this._unitIndex = _unitIndex;
            // this._effectAll = _effectAll;
        }
        public void TriggerHatchEffect()
        {
            GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
            bool _effectAll = _hatchEffect.GetComponent<HETimer>().AllUnits;
            _addHatchEffect?.Invoke(_hatchEffect, _unitIndex, _effectAll);
        }
    }
}
