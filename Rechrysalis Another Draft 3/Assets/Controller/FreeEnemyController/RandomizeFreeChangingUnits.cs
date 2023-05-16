using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class RandomizeFreeChangingUnits : MonoBehaviour
    {
        private bool _debugBool = false;
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
            int tier = level;
            foreach (UnitStatsSO unitToChange in changingUnits.ControllerUnits)
            {
                if (unitToChange != null)
                {
                    IfFFUnitChangeTypeToFFType(unitToChange, focusFireBool);
                    ChangeThisUnitTypeNotFF(tier, unitToChange, focusFireBool);
                    ChangeThisUnitTIer(tier, unitToChange);
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
                parentUnitClass.Initialize(_compsAndUnitsSO);
                parentUnitClass.ClearAllUpgrades();
                parentUnitClass.SetUTCBasicUnit(unitToChange.UpgradeTypeClass);
                // if (focusFireBool)
                // {
                //     parentUnitClass.SetUTCBasicUnit(changingUnits.ControllerUnits[0].UpgradeTypeClass);
                // }
                parentUnitClass.SetAllStats();
                listOfParentUnitClasses.Add(parentUnitClass);
                i++;
                tier --;
                if (tier < 0)
                {
                    tier = 0;
                }
            }
        }

        private void ChangeFocusFire(bool ffBool, UnitStatsSO unitToChange)
        {
            unitToChange.AIFocusFire = ffBool;
        }
        private void ChangeThisUnitTypeNotFF(int level, UnitStatsSO unitToChange, bool ffBool)
        {
            if (ffBool) return;
            int randomNumber = Random.Range(0, _unitTypes.ControllerUnits.Count -1);         
            ChangeThisUnitTypeToType(unitToChange, _unitTypes.ControllerUnits[randomNumber]);   
            // unitToChange.TypeMultiplier = _unitTypes.ControllerUnits[randomNumber].TypeMultiplier;
            // unitToChange.UnitSprite = _unitTypes.ControllerUnits[randomNumber].UnitSprite;
            // unitToChange.UnitName = _unitTypes.ControllerUnits[randomNumber].UnitName;
        }
        private void IfFFUnitChangeTypeToFFType(UnitStatsSO unitToChange, bool ffBool)
        {
            if (!ffBool) return;
            ChangeThisUnitTypeToType(unitToChange, _unitTypes.FFType);

        }
        private void ChangeThisUnitTypeToType(UnitStatsSO unitToChange, UnitStatsSO unitStatsSO)
        {
            unitToChange.TypeMultiplier = unitStatsSO.TypeMultiplier;
            unitToChange.UnitSprite = unitStatsSO.UnitSprite;
            unitToChange.UnitName = unitStatsSO.UnitName;
        }
        private void ChangeThisUnitTIer(int tier, UnitStatsSO unitToChange)
        {
            // int randomNumber = Random.Range(0, tier);
            // if (randomNumber > _tierMultipliersToChooseFrom.Count -1)
            // {
            //     randomNumber = _tierMultipliersToChooseFrom.Count -1;
            // }
            if (_tierMultipliersToChooseFrom.Count <= tier)
            {
                tier = _tierMultipliersToChooseFrom.Count -1;
            }
            unitToChange.TierMultiplier = _tierMultipliersToChooseFrom[tier];
            unitToChange.UnitName += " " + tier.ToString();
        }
        public ParentUnitClass GetARandomParentUnitClassFromChangings(List<ParentUnitClass> ffOrNot)
        {
            int randomNumber = Random.Range(0, ffOrNot.Count -1);
            return ffOrNot[randomNumber];
        }
        public ParentUnitClass GetARandomFFParentUnitClass()
        {
            int randomNumber = Random.Range(0, _listOfFFParentUnitClasses.Count - 1);
            return _listOfFFParentUnitClasses[randomNumber];
        }
        public ParentUnitClass GetARandomFFParentUnitClassBasedOnControllerLife(float life)
        {
            return GetARandomParentUnitClassFFOrNOtBasedOnLifeAmount(life, _listOfFFParentUnitClasses);
        }
        public ParentUnitClass GetARandomNonFFParentUnitClassBasedOnControllerLife(float life)
        {
            return GetARandomParentUnitClassFFOrNOtBasedOnLifeAmount(life, _listOfRandomParentUnitClasses);
        }
        public ParentUnitClass GetARandomParentUnitClassFFOrNOtBasedOnLifeAmount(float life, List<ParentUnitClass> ffOrNot)
        {
            ParentUnitClass parentUnitClass = GetARandomParentUnitClassFromChangings(ffOrNot);
            if ((parentUnitClass.BasicUnitClass.ControllerLifeCostMult <= life))
            {
                return parentUnitClass;
            }
            // List<ParentUnitClass> ifNotEnoughManaParentUnitClassesNotTried = new List<ParentUnitClass>();
            // foreach (ParentUnitClass parentUnitClassToAdd in ffOrNot)
            // {
            //     if (parentUnitClassToAdd != null)
            //     {
            //         ifNotEnoughManaParentUnitClassesNotTried.Add(parentUnitClassToAdd);
            //     }
            // }
            List<ParentUnitClass> ifNotEnoughManaParentUnitClassesNotTried = CreateListIfNotEnoughMana(ffOrNot);
            if (ifNotEnoughManaParentUnitClassesNotTried.Contains(parentUnitClass))
            {
                ifNotEnoughManaParentUnitClassesNotTried.Remove(parentUnitClass);
                if (_debugBool)
                {
                    Debug.Log($"random parent units length " + ffOrNot.Count);
                }
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
        private List<ParentUnitClass> CreateListIfNotEnoughMana(List<ParentUnitClass> ffOrNot)
        {
            List<ParentUnitClass> ifNotEnoughManaParentUnitClassesNotTried = new List<ParentUnitClass>();
            foreach (ParentUnitClass parentUnitClassToAdd in ffOrNot)
            {
                if (parentUnitClassToAdd != null)
                {
                    ifNotEnoughManaParentUnitClassesNotTried.Add(parentUnitClassToAdd);
                }
            }
            return ifNotEnoughManaParentUnitClassesNotTried;
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
