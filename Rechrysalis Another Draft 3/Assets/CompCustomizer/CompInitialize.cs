using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class CompInitialize : MonoBehaviour
    {
        private CompCustomizerSO _compCustomizerSO;
        [SerializeField] private GameObject _compButtonPrefab;
        [SerializeField] private CompVerticalManager[] _verticalMangers;
        public Action<CompUpgradeManager> _onCompUpgradeClicked;

        public void OnEnable()
        {
            SubscribeToVerticalManagers();
        }
        public void SubscribeToVerticalManagers()        
        {
            if (_verticalMangers != null)
            {
                for (int _index = 0; _index < _verticalMangers.Length; _index ++)
                {
                    if (_verticalMangers[_index] != null)
                    {
                        _verticalMangers[_index]._onCompUpgradeClicked -= CompUpgradeClicked;
                        _verticalMangers[_index]._onCompUpgradeClicked += CompUpgradeClicked;
                    }
                }
            }
        }
        private void OnDisable()
        {
            if (_verticalMangers != null)
            {
                for (int _index = 0; _index < _verticalMangers.Length; _index++)
                {
                    if (_verticalMangers[_index] != null)
                    {
                        _verticalMangers[_index]._onCompUpgradeClicked -= CompUpgradeClicked;
                    }
                }
            }
        }
        public void Initialize(CompCustomizerSO compCustomizerSO, CompSO playerComp, GameObject movingButtonHolder)
        {
            _compCustomizerSO = compCustomizerSO;  
            LoopVerticalsToSetUp(playerComp, movingButtonHolder);
            // SubscribeToVerticalManagers();
        }
        private void CompUpgradeClicked(CompUpgradeManager compUpgradeManager)
        {
            _onCompUpgradeClicked?.Invoke(compUpgradeManager);            
        }
        private void LoopVerticalsToSetUp(CompSO playerComp, GameObject movingButtonHolder)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {
                SetUpEachVertical(playerComp, parentIndex, movingButtonHolder);
            }
        }
        public void LoopVerticalsToChangeDisplay(CompSO tempComp)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {
                // _verticalMangers[parentIndex].
            }
        }
        private void SetUpEachVertical(CompSO playerComp, int parentIndex, GameObject movingButtonHolder)
        {
            _verticalMangers[parentIndex]?.Initialize(playerComp, parentIndex, _compButtonPrefab, movingButtonHolder);
        }
        public void SetCompUpgradeDisplay(int parentIndex, int childIndex, UpgradeTypeClass upgradeTypeClass)
        {
            _verticalMangers[parentIndex]?.SetCompUpgradeDisplay(childIndex, upgradeTypeClass);
        }
    }
}
