using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ParticleEffectDeteriorate : MonoBehaviour
    {
        [SerializeField] private float _timerMax = 0.7f;
        [SerializeField] private float _timerCurrent;
        
        private void FixedUpdate()
        {
            _timerCurrent += Time.deltaTime;
            if (_timerCurrent >= _timerMax)
            {
                Destroy(gameObject);
            }
        }
    }
}
