using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private Click _click;
        [SerializeField] private TouchSO _touch;
        private Mover _mover;
        private void Awake() {
            _mover = GetComponent<Mover>();
            _mover.Initialize();
            _click?.Initialize(gameObject);
            _touch?.Initialize(gameObject);
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
