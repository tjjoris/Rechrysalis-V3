using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class UpgradeUnit : MonoBehaviour
    {
        private bool _debugLog = false;
        private ParentUnitManager _parentUnitManager;
        private ChrysalisActivation _chrysalisActivation;
        public Action<float> _subtractMana;
        private ManaGenerator _manaGenerator;

        public void Initialize(ParentUnitManager parentUnitManager, ManaGenerator manaGenerator)
        {
            _parentUnitManager = parentUnitManager;
            _manaGenerator = manaGenerator;
            _chrysalisActivation = GetComponent<ChrysalisActivation>();
        }
        public void UpgradeUnitFunction(int _chrysalisIndex)
        {
            if (_debugLog)
            {
                Debug.Log("upgrade unit func start" + _chrysalisIndex);
            }
            if ((_chrysalisIndex == 0) && (_parentUnitManager.CurrentSubUnit != _parentUnitManager.SubUnits[0])) return;
            if (_parentUnitManager.CurrentSubUnit == _parentUnitManager.SubChrysalii[_chrysalisIndex]) return;
            if (_chrysalisIndex == 0) return;
            if ((_manaGenerator == null) || (!CheckIfEnoughMana(_chrysalisIndex))) return;
            if (_debugLog)
            {
                Debug.Log($"upgrade unit func " + _chrysalisIndex);
            }
            SubtractMana(_chrysalisIndex);
            _chrysalisActivation.ActivateChrysalis(_chrysalisIndex);
        }
        private bool CheckIfEnoughMana(int _chrysalisIndex)
        {
            if ((_parentUnitManager.SubUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost <= _manaGenerator.ManaCurrent))
            {
                return true;
            }
            return false;
        }
        private void SubtractMana(int _chrysalisIndex)
        {
            // Debug.Log($"should subtract " + _parentUnitManager.SubUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost);
            _subtractMana?.Invoke(_parentUnitManager.SubUnits[_chrysalisIndex].GetComponent<UnitManager>().ManaCost);
        }
    }
}
