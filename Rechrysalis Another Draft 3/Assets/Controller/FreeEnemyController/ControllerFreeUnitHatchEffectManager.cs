using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Controller
{
    public class ControllerFreeUnitHatchEffectManager : MonoBehaviour
    {
        private GameObject[] _unitsArray;
        private FreeUnitHatchEffect[] _freeHatches;
        private ParentUnitHatchEffects[] _parentUnitHatchEffects;
        public void InitializeUnitsArray(int _length)
        {
            _unitsArray = new GameObject[_length];
        }
        public void SetFreeHatches(FreeUnitHatchEffect[] _freeHatches)
        {
            this._freeHatches = _freeHatches;
        }
        public void SetUnitsArray(GameObject _unit, int _index)
        {
            _unitsArray[_index] = _unit;
        }
        public void SetParentUnitHatchEffects(ParentUnitHatchEffects[] _parentUnitHatchEffects)
        {
            this._parentUnitHatchEffects = _parentUnitHatchEffects;
        }        
        // public void AddHatchEffectsOld(List<GameObject> _allUnitsList, GameObject _hatchEffect, int _unitIndex, bool _allUnits)
        // {
        //     for (int _unitInList = 0; _unitInList < _allUnitsList.Count; _unitInList++)
        //     {
        //         if (_allUnitsList[_unitIndex] != null)
        //         {
        //             if (_unitIndex == _unitInList)
        //             {
        //                 _allUnitsList[_unitInList].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //             }
        //             else if (_allUnits)
        //             {
        //                 _allUnitsList[_unitInList].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
        //             }
        //         }
        //     }            
        // }
        public void AddHatchEffects(GameObject _hatchEffect, int _unitIndex, bool _effectAll)
        {
            Debug.Log($"add hacth effects!!!");
            // {

            // Debug.Log($"add (units array length) " + _unitsArray.Length);


            if ((_parentUnitHatchEffects != null) && (_parentUnitHatchEffects.Length > 0))
            {
                _parentUnitHatchEffects[_unitIndex].AddHatchEffect(_hatchEffect);
            }
            

            // if ((_freeHatches != null)  && (_freeHatches.Length > 0))
            // {
            //     for (int _arrayIndex = 0; _arrayIndex < _freeHatches.Length; _arrayIndex++)
            //     {
            //         if (_freeHatches[_arrayIndex] != null)
            //         {
            //             if (_arrayIndex == _unitIndex)
            //             {
            //                 // _freeHatches[_arrayIndex]._addHatchEffect(_hatchEffect, _unitIndex, _effectAll);
            //             }
            //             else if (_effectAll)
            //             {
            //                 // _freeHatches[_arrayIndex]._addHatchEffect(_hatchEffect, _unitIndex, _effectAll);
            //             }
            //         }
            // }
            if ((_unitsArray != null) && (_unitsArray.Length > 0))
            {
                for (int _arrayIndex =0; _arrayIndex < _unitsArray.Length; _arrayIndex++)
                {
                    if (_unitsArray[_arrayIndex] != null)
                    {
                        if (_unitIndex == _arrayIndex)
                        {
                            _unitsArray[_arrayIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                        }
                        else if (_effectAll)
                        {
                            _unitsArray[_arrayIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                        }
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
            // FreeUnitHatchEffect[] _freeHatches = GetComponentsInChildren<FreeUnitHatchEffect>();
            // Debug.Log($"subscribe length " + _freeHatches.Length);
            if ((_freeHatches != null) && (_freeHatches.Length > 0))
            {
                // Debug.Log($"free hatches length" + _freeHatches.Length);
                // foreach (FreeUnitHatchEffect _freeUnitHatch in _freeHatches)
                for (int _hEIndex = 0; _hEIndex < _freeHatches.Length; _hEIndex ++)
                {
                    if (_freeHatches[_hEIndex] !=null)
                    {
                        _freeHatches[_hEIndex]._addHatchEffect -= AddHatchEffects;
                        _freeHatches[_hEIndex]._addHatchEffect += AddHatchEffects;
                    }
                }
            }
        }
        // public void AddHatchEffect(GameObject _hatchEffect, int _unitIndex, bool _effectAll)
        // {

        // }
    }
}
