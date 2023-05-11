using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseBuildSpeed : MonoBehaviour
    {
        [SerializeField] private float _increaseBuildSpeed;

        public float GetIncreaseBuildSpeed()
        {
            return _increaseBuildSpeed;
        }
    }
}
