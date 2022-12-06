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
        public void Initialize(CompCustomizerSO compCustomizerSO, CompSO playerComp)
        {
            _compCustomizerSO = compCustomizerSO;  
            LoopVerticals(playerComp);
            // SubscribeToVerticalManagers();
        }
        private void CompUpgradeClicked(CompUpgradeManager compUpgradeManager)
        {
            _onCompUpgradeClicked?.Invoke(compUpgradeManager);            
        }
        private void LoopVerticals(CompSO playerComp)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {
                SetUpEachVertical(playerComp, parentIndex);
            }
        }
        private void SetUpEachVertical(CompSO playerComp, int parentIndex)
        {
            _verticalMangers[parentIndex]?.Initialize(playerComp, parentIndex, _compButtonPrefab);
        }
    }
}
