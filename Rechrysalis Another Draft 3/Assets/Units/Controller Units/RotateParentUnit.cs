using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class RotateParentUnit : MonoBehaviour
    {
        private Transform _controllerTransform;
        public void Initialize(Transform _controllerTranfsorm)
        {
            this._controllerTransform = _controllerTranfsorm;
        }
        public void Tick()
        {
            if (transform.rotation != _controllerTransform.transform.rotation)
            {
                transform.rotation = _controllerTransform.transform.rotation;
            }
        }
    }
}
