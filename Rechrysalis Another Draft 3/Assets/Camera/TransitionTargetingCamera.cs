using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CameraControl
{
    public class TransitionTargetingCamera : MonoBehaviour
    {
        private bool _debugBool = true;
        public void TransitionToTargeting()
        {
            if (_debugBool)
            {
                Debug.Log($"transition to targeting");
            }
        }
    }
}
