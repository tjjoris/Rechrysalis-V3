using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class ClickInfo : ScriptableObject
    {
        private GameObject _controlledController;
        public GameObject ControlledController{set{_controlledController = value;} get{return _controlledController;}}
        private GameObject _controlledUnit;
        public GameObject ControlledUnit {set{_controlledUnit = value;} get{return _controlledUnit;}}
        private bool _hilightRingVisable;
        public bool HilightRingVisibile{set{_hilightRingVisable = value;} get {return _hilightRingVisable;}}

    }
}
