using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rechrysalis.Unit
{
    public class HealthToBuildTimeLinear : MonoBehaviour
    {
        private bool _debugBool = true;
        private ParentHealth _parentHealth;
        private float _buildTimeMultMax = 0.5f;
        private float _buildTimeMult;
        private void Awake()
        {
            _parentHealth = GetComponent<ParentHealth>();
        }
        public void SetBuildTimeMult()
        {
            float mult = _parentHealth.GetHealthRatio();
            if (mult == 0)
            {
                Debug.Log($"return min");
                // _buildTimeMult = _buildTimeMultMax;
                _buildTimeMult = 1;
            return;
            }
            mult *= _buildTimeMultMax;
            mult =  1 - mult;
            Debug.Log($"return {mult}");
            _buildTimeMult = mult;
            if (_debugBool) Debug.Log($"set build time mult " + _buildTimeMult);
            return;
        }
        public void SetBuildTimeMax()
        {
            _buildTimeMult = 1 - _buildTimeMultMax;
        }
        public float GetBuildTimeMult()
        {
            return _buildTimeMult;
        }
    }
}
