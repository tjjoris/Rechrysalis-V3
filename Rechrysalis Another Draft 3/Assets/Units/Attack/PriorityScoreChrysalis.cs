using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class PriorityScoreChrysalis : MonoBehaviour
    {
        private TargetScoreValue _targetScoreValue;
        private ControllerManager _enemyControllerManager;
        private ParentUnitManager _parentUnitManager;

        private void Awake()
        {

            _parentUnitManager = GetComponent<ParentUnitManager>();
            _targetScoreValue = GetComponent<TargetScoreValue>();
        }
        public void Initialize(ControllerManager controllerManager)
        {
            _enemyControllerManager = controllerManager;
        }

        public float GenerateScore()
        {
            if (_parentUnitManager.CurrentSubUnit.GetComponent<ChrysalisManager>() != null)
            {
                return 60;
            }
            return _parentUnitManager.ParentHealth.GetHealthMissingRatio() * 30f;
        }
    }
}
