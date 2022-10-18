////////using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ParentUnitHatchEffects : MonoBehaviour
    {
        private List<GameObject> _hatchEffects;
        private GameObject[] _subUnits;
        private GameObject[] _subChrysalii;
        public void Initialize (GameObject[] _subUnits, GameObject[] _subchrysalii)
        {
            this._subUnits = _subUnits;
            this._subChrysalii =_subchrysalii;
            _hatchEffects = new List<GameObject>();
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            _hatchEffects.Add(_hatchEffect);
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
        }
    }
}
