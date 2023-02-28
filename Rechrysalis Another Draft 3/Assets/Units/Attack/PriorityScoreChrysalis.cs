using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Attacking
{
    public class PriorityScoreChrysalis : MonoBehaviour
    {
        private TargetScoreValue _targetScoreValue;
        private ControllerManager _enemyControllerManager;

        public void Initialize(ControllerManager controllerManager)
        {
            _enemyControllerManager = controllerManager;
        }
        private void Awake()
        {
            _targetScoreValue = GetComponent<TargetScoreValue>();
        }

        public float GenerateScore()
        {
            return 0;
        }
    }
}
