using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class TargetScoreRanking : MonoBehaviour
    {
        private ControllerManager _enemyControllerManager;
        private List<ParentUnitManager> _scoresRanked;
        public List<ParentUnitManager> ScoresRanked => _scoresRanked;

        public void Initialize(ControllerManager enemyControllerManager)
        {
            _enemyControllerManager = enemyControllerManager;
        }
        public void RankScores()
        {
            _scoresRanked = new List<ParentUnitManager>();
            _scoresRanked.Clear();
            foreach (ParentUnitManager parentUnit in _enemyControllerManager.ParentUnitManagers)
            {
                if ((parentUnit != null) && (parentUnit.gameObject.activeInHierarchy))
                {
                    AddToScoreRank(parentUnit);                    
                }
            }
        }
        private void AddToScoreRank(ParentUnitManager parentUnitManager)
        {
            float currentScoreValue = parentUnitManager.TargetScoreValue.CurrentScoreValue;
            for (int i = 0; i < _scoresRanked.Count; i++)
            {
                int j=i--;
                if (((j<0) || (_scoresRanked[j].TargetScoreValue.CurrentScoreValue > currentScoreValue)) && ((_scoresRanked[i].TargetScoreValue.CurrentScoreValue < currentScoreValue)))
                {
                    _scoresRanked.Insert( i, parentUnitManager);
                    return;
                }
            }
            _scoresRanked.Add(parentUnitManager);
        }
    }
}
