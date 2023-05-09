using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public abstract class DropBackGround : MonoBehaviour, IDropHandler
    {
        private bool _debugBool = false;
        [SerializeField] protected Transform _transformToDropUpgrade;
        [SerializeField] protected CompsAndUnitsSO _compsAndUnitsSO;
        // public Action<CompUpgradeManager> _buttonDropped;

        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (_debugBool)
                Debug.Log($"ondrop called");
            CompUpgradeManager compUpgradeManager = eventData.pointerDrag.GetComponent<CompUpgradeManager>();
            if (compUpgradeManager != null)
            {
                if (_debugBool)  Debug.Log($"the button is " + compUpgradeManager.GetUpgradeTypeClass().GetUpgradeType());
                DropUpgrade(compUpgradeManager);
            }
        }
        public virtual void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {            
            if (_debugBool)
            {
                Debug.Log($"drop to transform ");
            }
            compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
        }        
    }
}
