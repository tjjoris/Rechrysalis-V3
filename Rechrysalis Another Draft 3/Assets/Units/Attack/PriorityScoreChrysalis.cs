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

        public void Initialize(ControllerManager controllerManager)
        {
            _enemyControllerManager = controllerManager;
            _parentUnitManager = GetComponent<ParentUnitManager>();
        }
        private void Awake()
        {
            _targetScoreValue = GetComponent<TargetScoreValue>();
        }

        public float GenerateScore()
        {
            if (_parentUnitManager.CurrentSubUnit.GetComponent<ChrysalisManager>() != null)
            {
                return 100;
            }
            return _parentUnitManager.ParentHealth.GetHealthMissingRatio() * 100f;
        }
    }
}
