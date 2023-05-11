using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Attacking
{
    public class PriorityScoreDPS : MonoBehaviour
    {
        private ParentUnitManager _parentUnitManager;
        
        private void Awake()
        {

            _parentUnitManager = GetComponent<ParentUnitManager>();
            // Initialize();
        }

        // public void Initialize()
        // {
        // }
        public float GenerateScore()
        {
            Attack attack = _parentUnitManager.CurrentSubUnit.GetComponent<Attack>();
            if (attack != null)
            {                                
                return attack.GetDPS();
            }
            return 0;
        }
    }
}
