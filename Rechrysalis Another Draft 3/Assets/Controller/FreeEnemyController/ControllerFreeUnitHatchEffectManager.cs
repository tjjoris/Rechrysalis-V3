using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Controller
{
    public class ControllerFreeUnitHatchEffectManager : MonoBehaviour
    {

        public void AddHatchEffects(List<GameObject> _allUnitsList, GameObject _hatchEffect, int _unitIndex, bool _allUnits)
        {
            for (int _unitInList = 0; _unitInList < _allUnitsList.Count; _unitInList++)
            {
                if (_allUnitsList[_unitIndex] != null)
                {
                    if (_unitIndex == _unitInList)
                    {
                        _allUnitsList[_unitInList].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                    }
                    else if (_allUnits)
                    {
                        _allUnitsList[_unitInList].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                    }
                }
            }
        }
        public void Tick(float _timeAmount, List<GameObject> _allUnitsList)
        {
            if (_allUnitsList.Count > 0)
            {
                for (int _allUnitsIndex = 0; _allUnitsIndex < _allUnitsList.Count; _allUnitsIndex ++)
                {
                    _allUnitsList[_allUnitsIndex].GetComponent<HETimer>()?.Tick(_timeAmount);
                }
            }
        }
        
        private void OnEnable()
        {
            SubscribeToUnits();
        }
        public void SubscribeToUnits()
        {
            FreeUnitHatchEffect[] _freeHatches = GetComponentsInChildren<FreeUnitHatchEffect>();
            foreach (FreeUnitHatchEffect _freeUnitHatch in _freeHatches)
            {
                if (_freeUnitHatch !=null)
                {
                    _freeUnitHatch._addHatchEffect -= AddHatchEffect;
                    _freeUnitHatch._addHatchEffect += AddHatchEffect;
                }
            }
        }
        public void AddHatchEffect(GameObject _hatchEffect, int _unitIndex, bool _effectAll)
        {

        }
    }
}
