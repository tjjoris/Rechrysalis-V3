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
        [SerializeField] private List<UnitStatsMultiplierSO> _tierMultipliersToChooseFrom = new List<UnitStatsMultiplierSO>();
        public List<UnitStatsMultiplierSO> TierMultipliersToChooseFrom => _tierMultipliersToChooseFrom;
        private List<UnitStatsSO> _unitsNotEnoughManaForNotTried = new List<UnitStatsSO>();
        public void Initialize()
        {
            _unitsNotEnoughManaForNotTried = _changingUnits.ControllerUnits;
        }
        public void RandomizeChangingUnitsFunc(int level)
        {
            int i=0;
            foreach (UnitStatsSO unitToChange in _changingUnits.ControllerUnits)
            {
                if (unitToChange != null)
                {
                    ChangeThisUnitType(level, unitToChange);
                    if (i < 1)
                    {
                        ChangeFocusFire(true, unitToChange);
                    }
                    else 
                    {
                        ChangeFocusFire(false, unitToChange);
                    }
                }
                else
                {
                    Debug.LogError($"error ! unit changing null");
                }
                i++;
            }
        }
        private void ChangeFocusFire(bool ffBool, UnitStatsSO unitToChange)
        {
            unitToChange.AIFocusFire = ffBool;
        }
        private void ChangeThisUnitType(int level, UnitStatsSO unitToChange)
        {
            int randomNumber = Random.Range(0, _unitTypes.ControllerUnits.Count -1);            
            unitToChange.TypeMultiplier = _unitTypes.ControllerUnits[randomNumber].TypeMultiplier;
            unitToChange.UnitSprite = _unitTypes.ControllerUnits[randomNumber].UnitSprite;
            unitToChange.UnitName = _unitTypes.ControllerUnits[randomNumber].UnitName;
        }
        private void ChangeThisUnitTIer(int level, UnitStatsSO unitToChange)
        {
            int randomNumber = Random.Range(0, level);
            if (randomNumber > _tierMultipliersToChooseFrom.Count -1)
            {
                randomNumber = _tierMultipliersToChooseFrom.Count -1;
            }
            unitToChange.TierMultiplier = _tierMultipliersToChooseFrom[randomNumber];
        }
        public UnitStatsSO GetARandomUnitFromChangings()
        {
            int randomNumber = Random.Range(0, _changingUnits.ControllerUnits.Count -1);
            {
                return _changingUnits.ControllerUnits[randomNumber];
            }
        }
        public void NotEnoughManaForThisUnit(UnitStatsSO unitNotEnoughManaFor)
        {
            if (_unitsNotEnoughManaForNotTried.Contains(unitNotEnoughManaFor))
            {
                _unitsNotEnoughManaForNotTried.Remove(unitNotEnoughManaFor);
            }
        }
        public UnitStatsSO GetAUnitNotTriedWhenNotEnoughMana()
        {
            return _unitsNotEnoughManaForNotTried[0];
        }
    }
}
