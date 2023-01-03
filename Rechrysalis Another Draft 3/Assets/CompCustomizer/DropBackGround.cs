using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public abstract class DropBackGround : MonoBehaviour, IDropHandler
    {
        private bool debugBool = false;
        [SerializeField] protected Transform _transformToDropUpgrade;
        protected CompsAndUnitsSO _compsAndUnitsSO;
        public Action<CompUpgradeManager> _buttonDropped;

        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (debugBool)
                Debug.Log($"ondrop called");
            CompUpgradeManager compUpgradeManager = eventData.pointerDrag.GetComponent<CompUpgradeManager>();
            if (compUpgradeManager != null)
            {
                if (debugBool)  Debug.Log($"the button is " + compUpgradeManager.GetUpgradeTypeClass().GetUpgradeType());
                DropUpgrade(compUpgradeManager);
            }
        }
        protected virtual void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {            
            compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
        }        
    }
}
