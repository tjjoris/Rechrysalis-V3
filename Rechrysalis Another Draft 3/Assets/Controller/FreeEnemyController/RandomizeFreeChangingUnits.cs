using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class RandomizeFreeChangingUnits : MonoBehaviour
    {
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        [SerializeField] private ControllerUnitsSO _changingUnits;
        public ControllerUnitsSO ChangingUnits => _changingUnits;
        [SerializeField] private ControllerUnitsSO _focusFireChangings;
        public ControllerUnitsSO FocusFireChangings { get => _focusFireChangings; set => _focusFireChangings = value; }        
        [SerializeField] private ControllerUnitsSO _unitTypes;
        public ControllerUnitsSO UnitTypes { get => _unitTypes; set => _unitTypes = value; }
        [SerializeField] private List<UnitStatsMultiplierSO> _tierMultipliersToChooseFrom = new List<UnitStatsMultiplierSO>();
        public List<UnitStatsMultiplierSO> TierMultipliersToChooseFrom => _tierMultipliersToChooseFrom;
        [SerializeField] private List<ParentUnitClass> _listOfRandomParentUnitClasses = new List<ParentUnitClass>();
        public List<ParentUnitClass> ListOfRandomParentUnitClasses { get => _listOfRandomParentUnitClasses; set => _listOfRandomParentUnitClasses = value; }
        [SerializeField] private List<ParentUnitClass> _listOfFFParentUnitClasses;
        public List<ParentUnitClass> ListOfFFParentUnitClasses { get => _listOfFFParentUnitClasses; set => _listOfFFParentUnitClasses = value; }
        
        
        private List<UnitStatsSO> _unitsNotEnoughManaForNotTried = new List<UnitStatsSO>();
        private List<ParentUnitClass> _ifNotEnoughManaParentUnitClassesNotTried;
        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
            _unitsNotEnoughManaForNotTried = _changingUnits.ControllerUnits;
            _listOfRandomParentUnitClasses = new List<ParentUnitClass>();
            _listOfFFParentUnitClasses = new List<ParentUnitClass>();
            // _ifNotEnoughManaParentUnitClassesNotTried = new List<ParentUnitClass>();
        }
        public void RandomizeChangingUnitsFunc(int level)
        {
            ChangeUnits(level, _changingUnits, false, _listOfRandomParentUnitClasses);
            ChangeUnits(level, _focusFireChangings, true, _listOfFFParentUnitClasses);
        }

        private void ChangeUnits(int level, ControllerUnitsSO changingUnits, bool focusFireBool, List<ParentUnitClass> listOfParentUnitClasses)
        {
            int i = 0;
            foreach (UnitStatsSO unitToChange in changingUnits.ControllerUnits)
            {
                if (unitToChange != null)
                {
                    ChangeThisUnitType(level, unitToChange);
                    ChangeThisUnitTIer(level, unitToChange);
                    // if (i < 1)
                    // {
                    //     ChangeFocusFire(true, unitToChange);
                    // }
                    // else 
                    // {
                    ChangeFocusFire(focusFireBool, unitToChange);
                    // }
                }
                else
                {
                    Debug.LogError($"error ! unit changing null");
                }
                ParentUnitClass parentUnitClass = new ParentUnitClass();
                parentUnitClass.ClearAllUpgrades();
                parentUnitClass.SetUTCBasicUnit(unitToChange.UpgradeTypeClass);
                parentUnitClass.SetAllStats();
                listOfParentUnitClasses.Add(parentUnitClass);
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
            unitToChange.UnitName += " " + randomNumber.ToString();
        }
        public ParentUnitClass GetARandomParentUnitClassFromChangings()
        {
            int randomNumber = Random.Range(0, _listOfRandomParentUnitClasses.Count -1);
            return _listOfRandomParentUnitClasses[randomNumber];
        }
        public ParentUnitClass GetARandomParentUnitClassFromChangingsBasedOnLifeAmount(float life)
        {
            ParentUnitClass parentUnitClass = GetARandomParentUnitClassFromChangings();
            if ((parentUnitClass.BasicUnitClass.ControllerLifeCostMult <= life))
            {
                return parentUnitClass;
            }
            List<ParentUnitClass> ifNotEnoughManaParentUnitClassesNotTried = _listOfRandomParentUnitClasses;
            if (ifNotEnoughManaParentUnitClassesNotTried.Contains(parentUnitClass))
            {
                ifNotEnoughManaParentUnitClassesNotTried.Remove(parentUnitClass);
            }
            foreach(ParentUnitClass parentUnitClassLeft in ifNotEnoughManaParentUnitClassesNotTried)            
            {
                if (parentUnitClassLeft != null)
                {
                    if ((parentUnitClass.BasicUnitClass.ControllerLifeCostMult <= life))
                    {
                        return parentUnitClassLeft;
                    }
                }
            }
            return null;
        }
        public UnitStatsSO GetARandomUnitFromChangings()
        {
            int randomNumber = Random.Range(0, _changingUnits.ControllerUnits.Count -1);
            {
                return _changingUnits.ControllerUnits[randomNumber];
            }
        }
        // public UnitStatsSO GetARandomUnitFromChangingsBasedOnLifeAmount(float life)
        // {
        //     UnitStatsSO randomUnit = GetARandomUnitFromChangings();

        //     return randomUnit;
        // }
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
