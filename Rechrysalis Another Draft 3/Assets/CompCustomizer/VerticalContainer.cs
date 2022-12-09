using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class VerticalContainer : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"ondrop called");
            GameObject dropped = eventData.pointerDrag;
            CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
            compUpgradeManager.ParentAfterDrag = transform;
        }

    }
}
