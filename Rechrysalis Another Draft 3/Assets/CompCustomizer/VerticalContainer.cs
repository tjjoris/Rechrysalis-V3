using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class VerticalContainer : MonoBehaviour, IDropHandler
    {
        private bool debugBool = false;
        public Action<CompUpgradeManager> _buttonDropped;
        public void OnDrop(PointerEventData eventData)
        {
            if (debugBool)
            Debug.Log($"ondrop called");
            GameObject dropped = eventData.pointerDrag;            
            CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
            if (compUpgradeManager != null)
            {
                compUpgradeManager.ParentAfterDrag = transform;
                _buttonDropped?.Invoke(compUpgradeManager);
            }
        }

    }
}
