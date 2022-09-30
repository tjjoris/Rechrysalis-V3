using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private Click _click;
        [SerializeField] private TouchSO _touch;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits{get{return _parentUnits;} set{_parentUnits = value;}}
        [SerializeField] private PlayerUnitsSO[] _playerUnitsSO;
        public PlayerUnitsSO[] PlayerUnitsSO {get{return _playerUnitsSO;} set{_playerUnitsSO = value;}}    
        [SerializeField] private CompSO _compSO;       

        private Mover _mover;
        public void Initialize(PlayerUnitsSO[] _playerUnitsSO) {
            this._playerUnitsSO = _playerUnitsSO;
            _mover = GetComponent<Mover>();
            _mover.Initialize();
            _click?.Initialize(gameObject);
            _touch?.Initialize(gameObject);
            for (int i=0; i<_parentUnits.Length;i++)
            {
                for (int j=0; j<3; j++)
                {
                    ParentUnitManager _parentUnitManager = _parentUnits[i].GetComponent<ParentUnitManager>();
                    _parentUnitManager.SubUnits[j].GetComponent<UnitManager>()?.Initialize(_compSO.UnitSOArray[(i*3) + j]);
                }
                _parentUnits[i].GetComponent<ParentUnitManager>()?.ActivateUnit(0);
            }
        }
        private void Update() 
        {
            _click?.Tick();
            _touch?.Tick();
        }
        public void Tick() {  
            float _deltaTime = Time.deltaTime;              
            _mover?.Tick(_deltaTime);
        }
    }
}
