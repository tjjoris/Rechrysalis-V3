using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class DropBackGround : MonoBehaviour, IDropHandler
    {
        private bool debugBool = false;
        [SerializeField] Transform _transformToDropUpgrade;
        public Action _buttonDropped;

        public void OnDrop(PointerEventData eventData)
        {
            if (debugBool)
                Debug.Log($"ondrop called");
            CompUpgradeManager compUpgradeManager = eventData.pointerDrag.GetComponent<CompUpgradeManager>();
            if (compUpgradeManager != null)
            {
                Debug.Log($"the button is " + compUpgradeManager.GetUpgradeTypeClass().GetUpgradeType());
                DropUpgrade(compUpgradeManager);
            }
        }
        private void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {
            compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
            _buttonDropped?.Invoke();
        }        
    }
}
