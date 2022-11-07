using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ParentClickManager : MonoBehaviour
    {
        private int _controllerIndex;
        public int ControllerIndex {get {return _controllerIndex;}}
        public void Initialize (int _controllerIndex)
        {
            this._controllerIndex = _controllerIndex;
        }
        public bool IsEnemy(int _sourceControllerIndex)
        {
            if (_controllerIndex != _sourceControllerIndex)
            {
                return true;
            }
            return false;
        }
    }
}
