using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.Unit
{
    public class UpgradeUnit : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private ChrysalisActivation _chrysalisActivation;
        public Action<float> _subtractMana;

        public void Initialize(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
        }
        public void UpgradeUnitFunction(int _chrysalisIndex)
        {
            if ((_chrysalisIndex == 0) && (_parentUnitManager.CurrentSubUnit != _parentUnitManager.SubUnits[0])) return;
            if (_parentUnitManager.CurrentSubUnit == _parentUnitManager.SubChrysalii[_chrysalisIndex]) return;
            if (_chrysalisIndex == 0) return;
            if (!CheckIfEnoughMana(_chrysalisIndex)) return;
            SubtractMana(_chrysalisIndex);
            _chrysalisActivation.ActivateChrysalis(_chrysalisIndex);
        }
        private bool CheckIfEnoughMana(int _chrysalisIndex)
        {
            if ((_parentUnitManager.SubUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost <= _parentUnitManager.ManaAmount))
            {
                return true;
            }
            return false;
        }
        private void SubtractMana(int _chrysalisIndex)
        {
            _subtractMana?.Invoke(_parentUnitManager.SubUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost);
        }
    }
}
