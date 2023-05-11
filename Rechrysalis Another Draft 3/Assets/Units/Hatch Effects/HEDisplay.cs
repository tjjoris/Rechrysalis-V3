using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _transformToOffset;
        private Vector2 _offset = new Vector2(0.2f, -0.2f);
        public void PositionOffset(int _multiplier)
        {
            _transformToOffset.localPosition = (_offset * _multiplier);
        }
    }
}
