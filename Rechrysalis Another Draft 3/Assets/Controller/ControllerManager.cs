using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private Click _click;
        [SerializeField] private TouchSO _touch;
        void Start()
        {
            _click?.Initialize(gameObject);
            _touch?.Initialize(gameObject);
            
        }
        void Update()
        {
            _click?.Tick();
            _touch?.Tick();
        }
    }
}
