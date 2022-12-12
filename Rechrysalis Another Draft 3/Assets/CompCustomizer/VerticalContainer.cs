using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class VerticalContainer : MonoBehaviour, IDropHandler
    {
        public Action<CompUpgradeManager> _buttonDropped;
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"ondrop called");
            GameObject dropped = eventData.pointerDrag;            
            CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
            compUpgradeManager.ParentAfterDrag = transform;
            _buttonDropped?.Invoke(compUpgradeManager);
        }

    }
}
