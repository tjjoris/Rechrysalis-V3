using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Rechrysalis.CompCustomizer;

namespace Rechrysalis.Archived
{
    public class SelectionContainer : MonoBehaviour, IDropHandler
    {
        private bool debugBool = true;
        public void OnDrop(PointerEventData eventData)
        {
            if (debugBool)
                Debug.Log($"selection container ondrop called");
            GameObject dropped = eventData.pointerDrag;
            // if (dropped != null)
            // {
                // CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
                if (dropped.GetComponent<CompUpgradeManager>() != null)
                {
                    DropUpgrade(dropped.GetComponent<CompUpgradeManager>());
                }
            // }
        }
        private void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {
            compUpgradeManager.ParentAfterDrag = transform;
            // _buttonDropped?.Invoke(compUpgradeManager);
        }
    }
}
