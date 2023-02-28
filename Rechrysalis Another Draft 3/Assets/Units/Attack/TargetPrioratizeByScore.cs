using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class TargetPrioratizeByScore : MonoBehaviour
    {
        private TargetScoreRanking _targetScoreRanking;
        private TargetHolder _targetHolder;

        public void Initialize(TargetScoreRanking targetScoreRanking)
        {
            _targetScoreRanking = targetScoreRanking;
            _targetHolder = GetComponent<TargetHolder>();
        }
        public void SetTargetByScore()
        {
            foreach (ParentUnitManager parentUnitManager in _targetScoreRanking.ScoresRanked)
            {
                if ((parentUnitManager != null) && (parentUnitManager.gameObject.activeInHierarchy))
                {
                    if (_targetHolder.GetThisTargetInRange(parentUnitManager.gameObject))
                    {
                        _targetHolder.Target = parentUnitManager.gameObject;
                    }
                }
            }
        }

    }
}
