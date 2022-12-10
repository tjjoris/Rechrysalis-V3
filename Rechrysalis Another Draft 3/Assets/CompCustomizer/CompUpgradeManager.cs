using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rechrysalis.CompCustomizer
{
    public class CompUpgradeManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IPointerDownHandler, IPointerMoveHandler
    {
        [SerializeField] private int _parentIndex;
        public int ParentIndex { get{ return _parentIndex; } set{ _parentIndex = value; } }
        [SerializeField] private int _childIndex;
        public int ChildIndex { get{ return _childIndex; } set{ _childIndex = value; } }
        private Transform _movingButtonHolder;
        [SerializeField] private Transform _parentAfterDrag;
        public Transform ParentAfterDrag { get{ return _parentAfterDrag; } set{ _parentAfterDrag = value; } }
        [SerializeField] private int _siblingIndex;
        public int PositionInSlot { get{ return _siblingIndex; } set{ _siblingIndex = value; } }
        private Image _image;
        
        
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        private UpgradeButtonDisplay _upgradeButtonDisplay;
        private float _holdTimerCurrent;
        private float _holdTimerMax = 0.4f;
        private bool _upgradeClickedPointerWithin;
        private bool _buttonHeldToMove;
        private bool _pointerWithinButton;
        private float _yPointerStart;
        private float _yMaxYDistAllowedToHold = 15;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;    
        public Action _disableVerticalScroll;
        public void Initialize(int parentIndex, int childIndex, Transform movingButtonHolder)
        {
            _movingButtonHolder = movingButtonHolder;
            _parentIndex = parentIndex;
            _childIndex = childIndex;
            // _upgradeType = new UpgradeTypeClass();
            _upgradeButtonDisplay = GetComponent<UpgradeButtonDisplay>();
            _image = GetComponent<Image>();
        }

        public void SetUpgradeType(UpgradeTypeClass upgradeTypeClass)
        {
            _upgradeTypeClass = upgradeTypeClass;
        }
        public UpgradeTypeClass.UpgradeType GetUpgradeTypeClass()
        {
            return _upgradeTypeClass.GetUpgradeType();
        }
        public UpgradeButtonDisplay GetUpgradeButtonDisplay()
        {
            return _upgradeButtonDisplay;
        }
        // public void OnPointerMove(PointerEventData pointerData)
        // {
        //     if (_buttonHeldToMove)
        //     transform.position = Input.mousePosition;
        // }
        // public void OnPointerDown(PointerEventData pointerData)
        // {
        //     CompUpgradeClicked();
        // }
        public void CompUpgradeClicked()
        {
            _upgradeClickedPointerWithin = true;
            _pointerWithinButton = true;
            _buttonHeldToMove = false;            
            _holdTimerCurrent = 0;
            _yPointerStart = Input.mousePosition.y;
            _onCompUpgradeClicked?.Invoke(this);
        }
        private void Update()
        {
            // if (_upgradeClickedPointerWithin && !_buttonHeldToMove) {
            //     _holdTimerCurrent += Time.deltaTime;  
            //     // _upgradeClickedPointerWithin = EventSystem.current.IsPointerOverGameObject();                       
            //     if (Mathf.Abs(_yPointerStart - Input.mousePosition.y) > _yMaxYDistAllowedToHold)
            //     {
            //         _upgradeClickedPointerWithin = false;
            //     }
            //     if ((_upgradeClickedPointerWithin)&& (_holdTimerCurrent >= _holdTimerMax))
            //     {
            //         _buttonHeldToMove = true;
            //         transform.SetParent(_movingButtonHolder.transform);
            //         _disableVerticalScroll?.Invoke();
            //     }
            // }            
        }

        public void OnBeginDrag(PointerEventData eventData)
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
            Debug.Log($"sibling index " + _siblingIndex);
            transform.SetParent(_parentAfterDrag);
            transform.SetSiblingIndex(_siblingIndex);
            _image.raycastTarget = true;
        }
    }
}
