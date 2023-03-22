using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "FreeUnitCompSO", menuName = "Comps/FreeUnitCompSO")]
    public class FreeUnitCompSO : ScriptableObject
    {
        [SerializeField] private List<ParentUnitClass> _parentUnitClasses;
        public List<ParentUnitClass> ParentUnitClasses { get => _parentUnitClasses; set => _parentUnitClasses = value; }
        
        [SerializeField] private UnitStatsSO[] _unitSOArray;
        public UnitStatsSO[] UnitSOArray { get { return _unitSOArray; } }
        [SerializeField] private WaveSO[] _waves;
        public WaveSO[] Waves { get {return _waves;}}
        [SerializeField] private WaveLayout _waveLayout;
        public WaveLayout WaveLayout {get {return _waveLayout;}}
        [SerializeField] private WaveLayoutsByRange _waveLayoutsByRange;
        public WaveLayoutsByRange WaveLayoutsByRange => _waveLayoutsByRange;
        [SerializeField] private ControllerUnitsSO _controllerUnitsToChooseFrom;
        public ControllerUnitsSO ControllerUnitsToChooseFrom => _controllerUnitsToChooseFrom;
        [SerializeField] private ControllerUnitsSO _currentChangingControllerUnits;
        public ControllerUnitsSO CurrentChangingControllerUnits => _currentChangingControllerUnits;
        
    }
}
