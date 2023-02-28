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
        private TargetsListSO _targetsListSO;

        public void Initialize(TargetScoreRanking targetScoreRanking, TargetsListSO targetsListSO)
        {
            _targetScoreRanking = targetScoreRanking;
            _targetHolder = GetComponent<TargetHolder>();
            _targetsListSO = targetsListSO;
        }
        public void SetTargetByScore()
        {
            foreach (ParentUnitManager parentUnitManager in _targetScoreRanking.ScoresRanked)
            {
                if ((_targetsListSO.Targets[0] != null) && (_targetsListSO.Targets[0].activeInHierarchy))
                _targetHolder.Target = _targetsListSO.Targets[0];
                // if ((parentUnitManager != null) && (parentUnitManager.gameObject.activeInHierarchy))
                // {
                //     // if (_targetHolder.GetThisTargetInRange(parentUnitManager.gameObject))
                //     {
                //         _targetHolder.Target = parentUnitManager.gameObject;
                //     }
                // }
            }
        }

    }
}
