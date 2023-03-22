using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class RandomizeFreeChangingUnits : MonoBehaviour
    {
        [SerializeField] private ControllerUnitsSO _changingUnits;
        public ControllerUnitsSO ChangingUnits => _changingUnits;
        [SerializeField] private ControllerUnitsSO _unitTypes;
        public ControllerUnitsSO UnitTypes { get => _unitTypes; set => _unitTypes = value; }
        
        public void RandomizeChangingUnitsFunc(int level)
        {
            foreach (UnitStatsSO unitToChange in _changingUnits.ControllerUnits)
            {
                if (unitToChange != null)
                {
                    ChangeThisUnit(level, unitToChange);
                }
                else
                {
                    Debug.LogError($"error ! unit changing null");
                }
            }
        }
        private void ChangeThisUnit(int level, UnitStatsSO unitToChange)
        {
            int randumNumber = Random.Range(0, _unitTypes.ControllerUnits.Count -1);
            unitToChange.TypeMultiplier = _unitTypes.ControllerUnits[randomNumber].TypeMultiplier;
        }
    }
}
