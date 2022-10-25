using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Background
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private float _minX = -3;
        public float MinX {get{return _minX;}}
        [SerializeField] private float _maxX = 3;
        public float MaxX {get{return _maxX;}}
        [SerializeField] private float _minY = -14;
        public float MinY {get{return _minY;}}
        [SerializeField] private float _maxY = 14;
        public float MaxY {get{return _maxY;}}
        private BackgroundPool _backGroundPool;
        private int _tickCount = 1000;
        private int _tickMax = 1000;

        public void Initialize ()
        {
            _backGroundPool = GetComponent<BackgroundPool>();
            _backGroundPool?.CreatePool(20);
        }
        public void Tick()
        {
            if (_tickCount >= _tickMax)
            {
                _backGroundPool?.Tick();
                _tickCount = 0;
            }
            else {
                _tickCount ++;
            }
        }
    }
}
