using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class CompUpgradeManager : MonoBehaviour
    {
        [SerializeField] private int _parentIndex;
        public int ParentIndex { get{ return _parentIndex; } set{ _parentIndex = value; } }
        [SerializeField] private int _childIndex;
        public int ChildIndex { get{ return _childIndex; } set{ _childIndex = value; } }
        
        [SerializeField] private UpgradeTypeClass _upgradeType;
        private UpgradeButtonDisplay _upgradeButtonDisplay;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;
        public void Initialize(int parentIndex, int childIndex)
        {
            _parentIndex = parentIndex;
            _childIndex = childIndex;
            _upgradeType = new UpgradeTypeClass();
            _upgradeButtonDisplay = GetComponent<UpgradeButtonDisplay>();
        }

        public void SetUpgradeType(UpgradeTypeClass.UpgradeType upgradeType)
        {
            _upgradeType.SetUpgradeType(upgradeType);
        }
        public UpgradeTypeClass.UpgradeType GetUpgradeTypeClass()
        {
            return _upgradeType.GetUpgradeType();
        }
        public UpgradeButtonDisplay GetUpgradeButtonDisplay()
        {
            return _upgradeButtonDisplay;
        }
        public void CompUpgradeClicked()
        {
            _onCompUpgradeClicked?.Invoke(this);
        }
    }
}
