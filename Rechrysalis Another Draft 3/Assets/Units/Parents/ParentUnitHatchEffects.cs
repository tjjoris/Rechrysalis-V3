////////using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;
using System;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class ParentUnitHatchEffects : MonoBehaviour
    {
        private bool _debugBool = false;
        private ControllerManager _controllerManager;
        private ParentUnitManager _parentUnitManager;
        private DisplayUnitHEIcon _displayUnitHEIcon;
        [SerializeField] private List<GameObject> _hatchEffects;
        public List<GameObject> HatchEffects => _hatchEffects;
        private GameObject[] _subUnits;
        private int _parentIndex;
        private GameObject[] _subChrysalii;
        public Action<GameObject, int, int, bool> _addHatchEffect;
        public Action<GameObject, int, int, bool> _removeHatchEffect;

        private void Awake()
        {

            _parentUnitManager = GetComponent<ParentUnitManager>();
            _displayUnitHEIcon = GetComponent<DisplayUnitHEIcon>();
        }
        public void Initialize (GameObject[] _subUnits, GameObject[] _subchrysalii, ControllerManager controllerManager)
        {
            _controllerManager = controllerManager;
            _parentIndex = _parentUnitManager.ParentIndex;
            this._subUnits = _subUnits;
            this._subChrysalii =_subchrysalii;
            _hatchEffects = new List<GameObject>();
            // GetComponent<ParentClickManager>().Initialize(_controllerIndex);            
        }

        public void CreateHatchEffect(GameObject _hatchEffectPrefab, int _parentIndex, int _unitIndex, bool _affectAll)
        {
            UI.DebugTextStatic.DebugText.DisplayText("create hatch effet in PUHE");
            if (_debugBool) Debug.Log($"create hacth effect called in parent unit hatch effects");
            if (_hatchEffectPrefab == null) 
            {
                Debug.LogWarning("hatcheffect prefab == null");
                return;
            }
            GameObject _hatchEffect = Instantiate(_hatchEffectPrefab, transform);
            HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
            _hatchEffectManager?.Initialize(null, _parentIndex, _unitIndex, _affectAll, _parentUnitManager.ParentUnitClass.AdvUnitClass, _parentUnitManager.ChildUnitManagers[_unitIndex], _controllerManager, _parentUnitManager);
            _addHatchEffect?.Invoke(_hatchEffect, _parentIndex, _unitIndex, _hatchEffectManager.AffectAll);
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            if (_debugBool) Debug.Log($"add hatch effect called in parent unit hatch effects script");
            _hatchEffects.Add(_hatchEffect);
            for (int _index = 0; _index < _hatchEffects.Count; _index ++)
            {
                _hatchEffects[_index].GetComponent<HEDisplay>().PositionOffset(_index);
            }
        }
        public void RemoveAllHatchEffectsOwnedByUnit()
        {
            List<GameObject> tempHEList = new List<GameObject>();
            foreach (GameObject hatchEffect in _hatchEffects)
            {
                if (hatchEffect != null)
                {
                    tempHEList.Add(hatchEffect);
                }
            }
            foreach (GameObject hatchEffect in tempHEList)
            {
                if ((hatchEffect != null) && (hatchEffect.activeInHierarchy))
                {
                    SetHEIconToInactive(hatchEffect);
                    _removeHatchEffect?.Invoke(hatchEffect, _parentIndex, 0, true);
                }
            }
        }

        private static void SetHEIconToInactive(GameObject hatchEffect)
        {
            HatchEffectManager hem = hatchEffect.GetComponent<HatchEffectManager>();
            if (hem == null) return;
            hem.SetHEIconToInactive();
        }

        public void RemoveHatchEffect(GameObject _hatchEffect)
        {
            if (_hatchEffects.Contains(_hatchEffect))
            {
                _hatchEffects.Remove(_hatchEffect);
            }
            for (int _index = 0; _index < _hatchEffects.Count; _index++)
            {
                if (_hatchEffects[_index] == null) continue;
                HEDisplay heDisplay = _hatchEffects[_index].GetComponent<HEDisplay>();
                if (heDisplay == null) continue;            
                _hatchEffects[_index].GetComponent<HEDisplay>().PositionOffset(_index);
            }
        }
        public void TakeDamage(float _damage)
        {
            if ((_hatchEffects.Count > 0) && (_hatchEffects[0] != null))
            {
                if (_debugBool) Debug.Log($"hatch effect take damage " + _damage);
                _hatchEffects[0].GetComponent<HatchEffectManager>().TakeDamage(_damage);
            }
        }
    }
}
