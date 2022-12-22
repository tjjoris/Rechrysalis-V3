using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Rechrysalis.CompCustomizer;

namespace Rechrysalis.Archived
{
    public class CompDrop : MonoBehaviour, IDropHandler
    {
        [SerializeField] private CompVerticalManager[] _compVerticalManagers;
        // public Action<CompUpgradeManager> _droppedInCompMain;
        private CompInitialize _compInitialize;

        public void Initialize(CompVerticalManager[] compVerticalManagers)
        {
            _compVerticalManagers = compVerticalManagers;
            _compInitialize = GetComponent<CompInitialize>();
        }
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"compDrop called");
            GameObject dropped = eventData.pointerDrag;
            CompUpgradeManager compUpgradeManager = dropped.GetComponent<CompUpgradeManager>();
            compUpgradeManager.ParentAfterDrag = transform;
            // _compInitialize.ButtonDroppedInCompMain(compUpgradeManager);
            
            // _droppedInCompMain?.Invoke(compUpgradeManager);
            // DropInComp(CompUpgradeManager);
            
        }
        // private void DropInComp(CompUpgradeManager compUpgradeManager)
        // {
        //     // GetComponent<CompInitialize>()
        // }
    }
}
