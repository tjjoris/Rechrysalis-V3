using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.UI;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class DropComp : DropBackGround
    {
        private bool _debugBool = true;
        [SerializeField] private  ControllerHPTokens _controllerHPTokens;
        [SerializeField] private DropSelection _dropSelection;
        public ControllerHPTokens ControllerHPTokens {set { _controllerHPTokens = value;}}
        public override void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {
            // base.DropUpgrade(compUpgradeManager);
            if (compUpgradeManager.GetUpgradeType() != Unit.UpgradeTypeClass.UpgradeType.SingleHeart)
            {
                OnlyOneUnit(compUpgradeManager);
                OnlyOneHatchEffect(compUpgradeManager);
                base.DropUpgrade(compUpgradeManager);
                // compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
                // _buttonDropped?.Invoke(compUpgradeManager);            
            }
            else if ((compUpgradeManager.GetUpgradeType() == Unit.UpgradeTypeClass.UpgradeType.SingleHeart) && (_controllerHPTokens.IsMissingTokens()))
            {
                Debug.Log($"add heart");
                _controllerHPTokens.AddTokens(compUpgradeManager.GetUpgradeTypeClass().GetControllerHeartUpgrade().HeartCount);
                base.DropUpgrade(compUpgradeManager);
                Destroy(compUpgradeManager.gameObject);
            }            
        }
        private void OnlyOneHatchEffect(CompUpgradeManager compUpgradeManager)
        {
            List<CompUpgradeManager> hEChildren = GetAllHEs();
            if (_debugBool) Debug.Log($"all hes "+ hEChildren.Count);
            if ((!PlayerPrefsInteract.GetOnlyOneHatchEffect()) || (compUpgradeManager.GetUpgradeType() != Unit.UpgradeTypeClass.UpgradeType.HatchEffect) || (hEChildren.Count == 0)) return;
            MoveAllHEsToSelection(hEChildren, _dropSelection);
        }
        private void OnlyOneUnit(CompUpgradeManager compUpgradeManager)
        {
            if (compUpgradeManager.GetUpgradeType() != Unit.UpgradeTypeClass.UpgradeType.Basic) return;
            List<CompUpgradeManager> unitChildren = GetAllChildUnits();
            if (unitChildren.Count == 0) return;
            MoveAllHEsToSelection(unitChildren, _dropSelection);
        }
        // private int GetNumberOfHEsInChildren ()
        // {
        //     // int numberOfHEs = 0;
        //     // foreach (Transform child in transform)
        //     // {   
        //     //     if (transform == null) continue;
        //     //     CompUpgradeManager compUpgradeManager = transform.GetComponent<CompUpgradeManager>();
        //     //     if (compUpgradeManager == null) continue;
        //     //     if (compUpgradeManager.GetUpgradeType() == Unit.UpgradeTypeClass.UpgradeType.HatchEffect) numberOfHEs ++;

        //     // }
        //     // return GetAllHEs().Count;
        // }
        private List<CompUpgradeManager> GetAllHEs()
        {
            List<CompUpgradeManager> hEs = new List<CompUpgradeManager>();
            foreach (Transform child in transform)
            {
                if (transform == null) continue;
                if (_debugBool) Debug.Log($"contains child");
                CompUpgradeManager compUpgradeManager = child.GetComponent<CompUpgradeManager>();                
                if (compUpgradeManager == null) continue;
                if (_debugBool) Debug.Log($"contains compUpgradeManager");
                if (compUpgradeManager.GetUpgradeType() == Unit.UpgradeTypeClass.UpgradeType.HatchEffect) 
                {
                    hEs.Add(compUpgradeManager);
                }
            }
            return hEs;
        }
        private List<CompUpgradeManager> GetAllChildUnits()
        {
            List<CompUpgradeManager> unitChildren = new List<CompUpgradeManager>();
            foreach (Transform child in transform)
            {
                if (transform == null) continue;
                CompUpgradeManager compUpgradeManager = child.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager == null) continue;
                if (compUpgradeManager.GetUpgradeType() == Unit.UpgradeTypeClass.UpgradeType.Basic)
                {
                    unitChildren.Add(compUpgradeManager);
                }
            }
            return unitChildren;
        }
        private void MoveAllHEsToSelection(List<CompUpgradeManager> hEsInChildren, DropSelection dropSelection)
        {
            foreach (CompUpgradeManager hE in hEsInChildren)
            {
                if (_debugBool) Debug.Log($"move he to selection");
                dropSelection.DropUpgrade(hE);
                hE.EndDragFunc();
            }
        }
    }
    
}
