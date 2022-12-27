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
            Debug.Log($"trigger hatch for unit " + _unitIndex);
            if (_hatchEffectPrefab != null)
            {
                if (_addHatchEffect != null)
                {
                GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
                // bool _effectAll = _hatchEffect.GetComponent<HETimer>().AllUnits;
                bool _effectAll = true;
                Debug.Log($"hatch effect " + _hatchEffect.name + " unit index " + _unitIndex + " effect all " + _effectAll);
                    _addHatchEffect?.Invoke(_hatchEffect, _unitIndex, _effectAll);
                }
            }
        }
    }
}
