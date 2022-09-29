using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private Click _click;
        [SerializeField] private TouchSO _touch;
        [SerializeField] private GameObject[] _parentUnits;
        public GameObject[] ParentUnits{get{return _parentUnits;} set{_parentUnits = value;}}
        [SerializeField] private PlayerUnitsSO _myUnits;
        public PlayerUnitsSO MyUnits {get{return _myUnits;} set{_myUnits = value;}}
        [SerializeField] private PlayerUnitsSO _enemyUnits;
        public  PlayerUnitsSO EnemyUnits {get{return _enemyUnits;} set {_enemyUnits = value;}}

        private Mover _mover;
        private void Awake() {
            _mover = GetComponent<Mover>();
            _mover.Initialize();
            _click?.Initialize(gameObject);
            _touch?.Initialize(gameObject);
            for (int i=0; i<_parentUnits.Length;i++)
            {
                _parentUnits[i].GetComponent<UnitManager>()?.ActivateUnit(0);
            }
        }
        void Start()
        {         
            
        }
        private void Update() 
        {
            _click?.Tick();
            _touch?.Tick();
        }
        private void FixedUpdate() {  
            float _deltaTime = Time.deltaTime;              
            _mover?.Tick(_deltaTime);
        }
    }
}
