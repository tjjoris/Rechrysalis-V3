using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using System;

namespace Rechrysalis.CompCustomizer
{
    public class CompInitialize : MonoBehaviour
    {
        bool _debugBool = false;
        private CompSO _playerComp;
        private CompCustomizerSO _compCustomizerSO;
        [SerializeField] private GameObject _compButtonPrefab;
        private Transform _movingButtonHolder;
        [SerializeField] private List<CompVerticalManager> _verticalMangers;
        public List<CompVerticalManager> VerticalManagers => _verticalMangers;
        // public Action<CompUpgradeManager> _onCompUpgradeClicked;

        // public void OnEnable()
        // {
        //     // SubscribeToVerticalManagers();
        // }
        // public void SubscribeToVerticalManagers()        
        // {
        //     // if (_verticalMangers != null)
        //     // {
        //     //     for (int _index = 0; _index < _verticalMangers.Count; _index ++)
        //     //     {
        //     //         if (_verticalMangers[_index] != null)
        //     //         {
        //     //             _verticalMangers[_index]._onCompUpgradeClicked -= CompUpgradeClicked;
        //     //             _verticalMangers[_index]._onCompUpgradeClicked += CompUpgradeClicked;
        //     //         }
        //     //     }
        //     // }
        // }
        // private void OnDisable()
        // {
        //     // if (_verticalMangers != null)
        //     // {
        //     //     for (int _index = 0; _index < _verticalMangers.Count; _index++)
        //     //     {
        //     //         if (_verticalMangers[_index] != null)
        //     //         {
        //     //             _verticalMangers[_index]._onCompUpgradeClicked -= CompUpgradeClicked;
        //     //         }
        //     //     }
        //     // }
        // }
        public void Initialize(CompCustomizerSO compCustomizerSO, CompSO playerComp, Transform movingButtonHolder)
        {
            _movingButtonHolder = movingButtonHolder;
            _playerComp = playerComp;
            _compCustomizerSO = compCustomizerSO;  
            LoopVerticalsToSetUp(playerComp, movingButtonHolder);
            // SubscribeToVerticalManagers();
        }
        private void CompUpgradeClicked(CompUpgradeManager compUpgradeManager)
        {
            // _onCompUpgradeClicked?.Invoke(compUpgradeManager);            
        }
        private void LoopVerticalsToSetUp(CompSO playerComp, Transform movingButtonHolder)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {
                SetUpThisVertical(playerComp, parentIndex);

            }
        }
        // public void EnableScrollRect()
        // {
        //     // for (int i=0; i<_verticalMangers.Count; i++)
        //     // {
        //     //     _verticalMangers[i].EnableScrollRect();
        //     // }
        // }
        private void SetUpThisVertical(CompSO playerComp, int parentIndex)
        {   
            if (_debugBool)
            Debug.Log($"vertical to set up " + parentIndex);
            _verticalMangers[parentIndex]?.Initialize(_movingButtonHolder);
            if (playerComp.ParentUnitClassList.Count > parentIndex)
            {
                if (_debugBool)
                Debug.Log($"parent unit class count "+playerComp.ParentUnitClassList.Count);
                {
                    // _verticalMangers[parentIndex]?.CreateAndSetUpCompButtonsOld(playerComp, parentIndex, _compButtonPrefab, _movingButtonHolder, playerComp.ParentUnitClassList[parentIndex]);
                    _verticalMangers[parentIndex]?.CreateAndSetUpCompButtons(playerComp.ParentUnitClassList[parentIndex], _compButtonPrefab);
                }
            }
        }
        // public void ButtonDroppedInCompMain(CompUpgradeManager compUpgradeManager)
        // {
        //     // if (((compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.Basic)) )
        //     // {
        //     //     _playerComp.ParentUnitClassList.Add(new ParentUnitClass());
        //     //     _verticalMangers[_playerComp.ParentUnitClassList.Count-1].CreateAndSetUpCompButtonsOld(_playerComp, _playerComp.ParentUnitClassList.Count-1, _compButtonPrefab, _movingButtonHolder, _playerComp.ParentUnitClassList[_playerComp.ParentUnitClassList.Count -1]);


        //     // }
        // }
        // public void SetCompUpgradeDisplay(int parentIndex, int childIndex, UpgradeTypeClass upgradeTypeClass)
        // {
        //     // _verticalMangers[parentIndex]?.SetCompUpgradeDisplay(childIndex, upgradeTypeClass);
        // }
        public bool CheckIfCanContinue()
        {
            bool atLeastOneBasic = false;
            for (int i=0; i<_verticalMangers.Count; i++)
            {
                int numberOfBasic = _verticalMangers[i].GetNumberOfBasic();
                int numberOfHatchEffects = _verticalMangers[i].GetNumberOfHatchEffects();
                bool isAtLeastOneUpgrade = _verticalMangers[i].IsAtLeastOneAdvUpgrade();                
                if (numberOfBasic == 1)
                atLeastOneBasic = true;
                else 
                {
                    if ((numberOfHatchEffects > 0) || (isAtLeastOneUpgrade))
                    {
                        return false;
                    }
                }
                // if (!_verticalMangers[i].IsNoErrorsInThisUnitUpgrades())
                if ((numberOfBasic > 1) || (numberOfHatchEffects > 1))
                return false;
            }
            if (atLeastOneBasic)
            return true;
            return false;
        }
    }
}
