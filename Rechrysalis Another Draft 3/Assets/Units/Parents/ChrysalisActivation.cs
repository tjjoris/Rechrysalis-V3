using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ChrysalisActivation : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
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
    }
}
