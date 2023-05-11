using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class TargetScoreRanking : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        private ControllerManager _enemyControllerManager;
        private List<ParentUnitManager> _scoresRanked;
        public List<ParentUnitManager> ScoresRanked => _scoresRanked;
        private TargetsListSO _targetsListSO;

        private void Awake()
        {
            _controllerManager = GetComponent<ControllerManager>();
        }
        public void Initialize(ControllerManager enemyControllerManager, TargetsListSO targetsListSO)
        {
            _enemyControllerManager = enemyControllerManager;
            _targetsListSO = targetsListSO;
        }
        public void RankScores()
        {
            _scoresRanked = new List<ParentUnitManager>();
            _scoresRanked.Clear();
            _targetsListSO.Targets.Clear();
            foreach (ParentUnitManager parentUnit in _controllerManager.ParentUnitManagers)
            {
                if ((parentUnit != null) && (parentUnit.gameObject.activeInHierarchy))
                {
                    AddToScoreRank(parentUnit);                    
                }
            }
            if ((_scoresRanked != null) && (_scoresRanked.Count > 0))
            {
                _scoresRanked.Sort(SortFunc);
                _targetsListSO.Targets.Sort(SortFuncAsGO);
            }
        }
        private void AddToScoreRank(ParentUnitManager parentUnitManager)
        {
            float currentScoreValue = parentUnitManager.TargetScoreValue.CurrentScoreValue;
            // for (int i = 0; i < _scoresRanked.Count; i++)
            // {
            //     int j=i--;
            //     if (((j<0) || (_scoresRanked[j].TargetScoreValue.CurrentScoreValue > currentScoreValue)) && ((_scoresRanked[i].TargetScoreValue.CurrentScoreValue < currentScoreValue)))
            //     {
            //         _scoresRanked.Insert( i, parentUnitManager);
            //         return;
            //     }
            // }
            _scoresRanked.Add(parentUnitManager);
            _targetsListSO.Targets.Add(parentUnitManager.gameObject);
        }
        private int SortFunc(ParentUnitManager a, ParentUnitManager b) 
        {
            if (a.TargetScoreValue.CurrentScoreValue > b.TargetScoreValue.CurrentScoreValue)
            {
                return -1;
            }
            if (a.TargetScoreValue.CurrentScoreValue < b.TargetScoreValue.CurrentScoreValue)
            {
                return 1;
            }
            return 0;
        }
        private int SortFuncAsGO(GameObject a, GameObject b)
        {
            ParentUnitManager puma = a.GetComponent<ParentUnitManager>();
            ParentUnitManager pumb = b.GetComponent<ParentUnitManager>();
            if ((puma != null) && (pumb != null))
            {
                if (puma.TargetScoreValue.CurrentScoreValue > pumb.TargetScoreValue.CurrentScoreValue)
                {
                    return -1;
                }
                if (puma.TargetScoreValue.CurrentScoreValue < pumb.TargetScoreValue.CurrentScoreValue)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
