using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private float _minX = -3;
        public float MinX {get{return _minX;}}
        [SerializeField] private float _maxX = 3;
        public float MaxX {get{return _maxX;}}
        [SerializeField] private float _minY = -14;
        public float MinY {get{return _minY;}}
        [SerializeField] private float _maxY = 14;
        public float MaxY {get{return _maxY;}}
    }
}
