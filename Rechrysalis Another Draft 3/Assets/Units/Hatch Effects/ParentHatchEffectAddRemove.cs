using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.HatchEffect
{
    public class ParentHatchEffectAddRemove : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        private ParentHealth _parentHealth;

        public void Initialzie(ParentUnitManager parentUnitManager)
        {
            _parentUnitManager = parentUnitManager;
            _parentHealth = GetComponent<ParentHealth>();
        }
        public void AddHatchEffect(GameObject _hatchEffect)
        {
            Debug.Log($"parent unit add hatch effect called");
            // _hatchEffectManagersToDamage.Add(_hatchEffect.GetComponent<HatchEffectManager>());
            // _pUHE?.AddHatchEffect(_hatchEffect);
            if (_parentUnitManager.SubUnits.Length > 0)
            {
                for (int _childIndex = 0; _childIndex < _parentUnitManager.SubUnits.Length; _childIndex++)
                {
                    if (_parentUnitManager.SubUnits[_childIndex] != null)
                    {
                        Debug.Log($"add hatch effect to child");
                        _parentUnitManager.SubUnits[_childIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                    }
                }
                for (int _childIndex = 0; _childIndex < _parentUnitManager.SubChrysalii.Length; _childIndex++)
                {
                    if (_parentUnitManager.SubChrysalii[_childIndex] != null)
                    {
                        _parentUnitManager.SubChrysalii[_childIndex].GetComponent<UnitManager>()?.AddHatchEffect(_hatchEffect);
                    }
                }
            }
            CheckToModifyParentDefencesFromHEChanges(_hatchEffect);
        }
        public void RemoveHatchEffect(GameObject _hatchEffect)
        {
            // HatchEffectManager _hatchEffectManager = _hatchEffect.GetComponent<HatchEffectManager>();
            // if (_hatchEffectManager != null)
            // {
            //     if (_hatchEffectManagersToDamage.Contains(_hatchEffectManager))
            //     {
            //         _hatchEffectManagersToDamage.Remove(_hatchEffectManager);
            //     }
            // }
            // _pUHE?.RemoveHatchEffect(_hatchEffect);
            if (_parentUnitManager.SubUnits.Length > 0)
            {
                for (int _childIndex = 0; _childIndex < _parentUnitManager.SubUnits.Length; _childIndex++)
                {
                    if (_parentUnitManager.SubUnits[_childIndex] != null)
                    {
                        _parentUnitManager.SubUnits[_childIndex].GetComponent<UnitManager>()?.RemoveHatchEffect(_hatchEffect);
                    }
                }
                for (int _childIndex = 0; _childIndex < _parentUnitManager.SubChrysalii.Length; _childIndex++)
                {
                    if (_parentUnitManager.SubChrysalii[_childIndex] != null)
                    {
                        _parentUnitManager.SubChrysalii[_childIndex].GetComponent<UnitManager>()?.RemoveHatchEffect(_hatchEffect);
                    }
                }
            }
            CheckToModifyParentDefencesFromHEChanges(_hatchEffect);
        }
        private void CheckToModifyParentDefencesFromHEChanges(GameObject hatchEffect)
        {
            if (hatchEffect == null) return;
            if (hatchEffect.GetComponent<HEIncreaseDefence>() == null) return;
            List<HEIncreaseDefence> hEIncraseDefenceList = new List<HEIncreaseDefence>();
            foreach (GameObject hatchEffectToLoop in _parentUnitManager.SubUnits[0].GetComponent<UnitManager>().CurrentHatchEffects)
            {
                if (hatchEffectToLoop != null)
                {
                    if (hatchEffectToLoop.GetComponent<HEIncreaseDefence>() != null)
                    {
                        hEIncraseDefenceList.Add(hatchEffectToLoop.GetComponent<HEIncreaseDefence>());
                    }
                }
            }
            _parentHealth.ReCalculateIncomingDamageModifier(hEIncraseDefenceList);
        }
    }
}
