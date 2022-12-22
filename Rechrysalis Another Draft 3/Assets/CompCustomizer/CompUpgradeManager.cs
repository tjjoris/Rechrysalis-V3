using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CompUpgradeManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IPointerDownHandler, IPointerMoveHandler
    {
        private bool debugBool = false;
        private Transform _movingButtonHolder;
        [SerializeField] private Transform _parentAfterDrag;
        public Transform ParentAfterDrag { get{ return _parentAfterDrag; } set{ _parentAfterDrag = value; } }
        [SerializeField] private int _siblingIndex;
        public int SiblingIndex { get{ return _siblingIndex; } set{ _siblingIndex = value; } }
        private Image _image;
        
        
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        private UpgradeButtonDisplay _upgradeButtonDisplay;
        public void Initialize(Transform movingButtonHolder)
        {
            _movingButtonHolder = movingButtonHolder;
            _upgradeButtonDisplay = GetComponent<UpgradeButtonDisplay>();
            _upgradeButtonDisplay.Initialzie();
            _image = GetComponent<Image>();
        }
        public void SetUpgradeTypeClass(UpgradeTypeClass upgradeTypeClass)
        {
            _upgradeTypeClass = upgradeTypeClass;
        }
        public UpgradeTypeClass.UpgradeType GetUpgradeType()
        {
            return _upgradeTypeClass.GetUpgradeType();
        }
        public UpgradeTypeClass GetUpgradeTypeClass()
        {
            return _upgradeTypeClass;
        }
        public UpgradeButtonDisplay GetUpgradeButtonDisplay()
        {
            return _upgradeButtonDisplay;
        }
        public void SetDisplay(UpgradeTypeClass upgradeTypeClass)
        {
            _upgradeButtonDisplay.SetButotnDisplay(upgradeTypeClass);
        }        public void OnBeginDrag(PointerEventData eventData)
        {
            _siblingIndex = transform.GetSiblingIndex();
            _parentAfterDrag = transform.parent;
            transform.SetParent(_movingButtonHolder);
            transform.SetAsLastSibling();
            _image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (debugBool)
            Debug.Log($"sibling index " + _siblingIndex);
            transform.SetParent(_parentAfterDrag);
            transform.SetSiblingIndex(_siblingIndex);
            _image.raycastTarget = true;
        }
    }
}
