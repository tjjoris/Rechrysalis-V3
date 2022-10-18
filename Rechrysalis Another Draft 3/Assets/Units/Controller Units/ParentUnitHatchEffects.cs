using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ParentUnitHatchEffects : MonoBehaviour
    {
        private List<GameObject> _hatchEffects;
        private GameObject[] _subUnits;
        private GameObject[] _subchrysalii;
        public void Initialize (GameObject[] _subUnits, GameObject[] _subchrysalii)
        {
            this._subUnits = _subUnits;
            this._subchrysalii =_subchrysalii;
            _hatchEffects = new List<GameObject>();
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            _hatchEffects.Add(_hatchEffect);
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
