using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "ControllerUnitsSO" , menuName = "Comps/ControllerUnitsSO")]   
    public class ControllerUnitsSO : ScriptableObject
    {
        [SerializeField] private List<UnitStatsSO> _controllerUnits;
        public List<UnitStatsSO> ControllerUnits => _controllerUnits;
        [SerializeField] private UnitStatsSO _ffType;
        public UnitStatsSO FFType => _ffType;
    }
}
