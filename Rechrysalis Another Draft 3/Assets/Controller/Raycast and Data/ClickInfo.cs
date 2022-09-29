using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    [CreateAssetMenu(fileName = "Click Info", menuName = "Controller/Click/ClickInfo")]
    [System.Serializable]
    public class ClickInfo : ScriptableObject
    {
        [SerializeField] private GameObject _controlledController;
        public GameObject ControlledController{set{_controlledController = value;} get{return _controlledController;}}
        private GameObject _controlledUnit;
        public GameObject ControlledUnit {set{_controlledUnit = value;} get{return _controlledUnit;}}
        private bool _hilightRingVisable;
        public bool HilightRingVisibile{set{_hilightRingVisable = value;} get {return _hilightRingVisable;}}
        [SerializeField] private int _fingerIDMove;
        public int FingerIDMove {set{_fingerIDMove = value;}get {return _fingerIDMove;}}

    }
}
