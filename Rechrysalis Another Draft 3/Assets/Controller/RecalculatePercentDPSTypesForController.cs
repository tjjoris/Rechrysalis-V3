using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class RecalculatePercentDPSTypesForController : MonoBehaviour
    {
        private bool _debugBool = true;
        private ControllerManager _controllerManager;
        [SerializeField] private float _percentDPSToUnit;
        public float PercentDPSToUnit => _percentDPSToUnit;
        [SerializeField] private float _percentDPSToChrysalis;
        public float PercentDPSToChrysalis => _percentDPSToChrysalis;

        private void Awake()
        {
            _controllerManager = GetComponent<ControllerManager>();
        }

        public void RecalculatePercents()
        {
            if (_debugBool)
            {Debug.Log($"recaclulate percent dps");}
            _percentDPSToChrysalis = 0;
            _percentDPSToUnit = 0;
            foreach (ParentUnitManager parentUnitManager in _controllerManager.ParentUnitManagers)
            {
                // if (parentUnitManager == null) continue;
                GameObject currentUnit = parentUnitManager.CurrentSubUnit;
                if (currentUnit == null) 
                {
                    if (_debugBool) Debug.Log($"current sub unit == null");
                    continue;
                }
                if (currentUnit.GetComponent<ChrysalisTimer>()) continue;
                UnitManager unitManager =  currentUnit.GetComponent<UnitManager>();
                _percentDPSToChrysalis += unitManager.UnitClass.PercentDPSToChrysalisMult;
                _percentDPSToUnit += unitManager.UnitClass.PercentDPSToUnitMult;
            }
        }
    }
}
