using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] private Click _click;
        void Start()
        {
            _click.Initialize(gameObject);
            
        }
        void Update()
        {
        
        }
    }
}
