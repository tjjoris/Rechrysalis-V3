using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Rechrysalis.CompCustomizer
{
    public class CompUpgradeManager : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int _parentIndex;
        public int ParentIndex { get{ return _parentIndex; } set{ _parentIndex = value; } }
        [SerializeField] private int _childIndex;
        public int ChildIndex { get{ return _childIndex; } set{ _childIndex = value; } }
        
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        private UpgradeButtonDisplay _upgradeButtonDisplay;
        private float _holdTimerCurrent;
        private float _holdTimerMax = 2f;
        private bool _upgradeClickedPointerWithin;
        private bool _buttonHeldToMove;
        private bool _pointerWithinButton;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;    
        public void Initialize(int parentIndex, int childIndex)
        {
            _parentIndex = parentIndex;
            _childIndex = childIndex;
            // _upgradeType = new UpgradeTypeClass();
            _upgradeButtonDisplay = GetComponent<UpgradeButtonDisplay>();
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
        public void OnPointerDown(PointerEventData pointerData)
        {
            CompUpgradeClicked();
        }
        public void CompUpgradeClicked()
        {
            _upgradeClickedPointerWithin = true;
            _pointerWithinButton = true;
            _holdTimerCurrent = 0;
            _onCompUpgradeClicked?.Invoke(this);
        }
        private void Update()
        {
            if (_upgradeClickedPointerWithin && !_buttonHeldToMove) {
                _holdTimerCurrent += Time.deltaTime;  
                _upgradeClickedPointerWithin = EventSystem.current.IsPointerOverGameObject();                       
                if ((_upgradeClickedPointerWithin)&& (_holdTimerCurrent >= _holdTimerMax))
                {
                    _buttonHeldToMove = true;

                }
            }            
        }
    }
}
