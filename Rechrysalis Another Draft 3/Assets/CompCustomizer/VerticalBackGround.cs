using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class VerticalBackGround : MonoBehaviour, IDropHandler
    {
        private bool debugBool = false;
        [SerializeField] Transform _transformToDropUpgrade;
        public Action<CompUpgradeManager> _upgradeDropped;

        public void OnDrop(PointerEventData eventData)
        {
            if (debugBool)
                Debug.Log($"ondrop called");
            GameObject dropped = eventData.pointerDrag;
            if (dropped != null)
            {
                CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
                if (compUpgradeManager != null)
                {
                    DropUpgrade(compUpgradeManager);
                }
            }
        }
        private void DropUpgrade(CompUpgradeManager compUpgradeManager)
        {
            compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
            _upgradeDropped?.Invoke(compUpgradeManager);
        }
    }
}
