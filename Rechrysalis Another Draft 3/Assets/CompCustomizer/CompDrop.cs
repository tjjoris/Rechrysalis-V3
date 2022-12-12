using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class CompDrop : MonoBehaviour, IDropHandler
    {
        [SerializeField] private CompVerticalManager[] _compVerticalManagers;
        // public Action<CompUpgradeManager> _droppedInCompMain;

        public void Initialize(CompVerticalManager[] compVerticalManagers)
        {
            _compVerticalManagers = compVerticalManagers;
        }
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"compDrop called");
            GameObject dropped = eventData.pointerDrag;
            CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
            compUpgradeManager.ParentAfterDrag = transform;
            // _droppedInCompMain?.Invoke(compUpgradeManager);
            DropInComp(CompUpgradeManager);
        }
        private void DropInComp(CompUpgradeManager compUpgradeManager)
        {
            // GetComponent<CompInitialize>()
        }
    }
}
