using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class SelectionContainer : MonoBehaviour, IDropHandler
    {
        private bool debugBool = true;
        public void OnDrop(PointerEventData eventData)
        {
            if (debugBool)
                Debug.Log($"selection container ondrop called");
            GameObject dropped = eventData.pointerDrag;
            if (dropped != null)
            {
                CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
                compUpgradeManager.ParentAfterDrag = transform;
                // _buttonDropped?.Invoke(compUpgradeManager);
            }
        }
    }
}
