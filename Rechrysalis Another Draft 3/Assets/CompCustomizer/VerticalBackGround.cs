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
            CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
            compUpgradeManager.ParentAfterDrag = _transformToDropUpgrade.transform;
            _upgradeDropped?.Invoke(compUpgradeManager);
        }
    }
}
