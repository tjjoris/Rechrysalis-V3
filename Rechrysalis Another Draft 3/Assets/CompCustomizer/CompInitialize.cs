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
        [SerializeField] private ShowCompErrorText _showCompErrorText;
        public void Initialize(CompCustomizerSO compCustomizerSO, CompSO playerComp, Transform movingButtonHolder)
        {
            _movingButtonHolder = movingButtonHolder;
            _playerComp = playerComp;
            _compCustomizerSO = compCustomizerSO;  
            LoopVerticalsToSetUp(playerComp, movingButtonHolder);
        }
        private void LoopVerticalsToSetUp(CompSO playerComp, Transform movingButtonHolder)
        {
            for (int parentIndex = 0; parentIndex < 3; parentIndex ++)
            {
                SetUpThisVertical(playerComp, parentIndex);

            }
        }
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
                        _showCompErrorText.UpgradesNeedBasic();
                        return false;
                    }
                }
                if (numberOfBasic > 1)
                {
                    _showCompErrorText.CanOnlyHaveOneBasic();
                    return false;
                }
                if (numberOfHatchEffects > 1)
                {
                    _showCompErrorText.CanOnlyHaveOneHE();
                    return false;
                }

            }
            if (atLeastOneBasic)
            return true;
            _showCompErrorText.NeedOneBasic();
            return false;
        }
    }
}
