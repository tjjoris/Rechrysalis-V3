using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rechrysalis.Unit
{
    public class HealthToBuildTimeLinear : MonoBehaviour
    {
        private ParentHealth _parentHealth;
        private float _buildTimeMultMax = 0.5f;
        private float _buildTimeMult;
        private void Awake()
        {
            Initaialize();
        }
        public void Initaialize()
        {
            _parentHealth = GetComponent<ParentHealth>();
        }
        public void SetBuildTimeMult()
        {
            float mult = _parentHealth.GetHealthRatio();
            if (mult == 0)
            {
                Debug.Log($"return max");
                _buildTimeMult = _buildTimeMultMax;
            return;
            }
            mult *= _buildTimeMult;
            mult =  1 - mult;
            Debug.Log($"return {mult}");
            _buildTimeMult = mult;
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
