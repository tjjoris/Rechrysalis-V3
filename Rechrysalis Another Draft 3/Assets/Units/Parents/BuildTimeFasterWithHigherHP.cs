using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{    
    public class BuildTimeFasterWithHigherHP : MonoBehaviour
    {
        private ParentHealth _parentHealth;
        private float[] _ratioThreshHolds = new float[3] {0.3333f, 0.6666f, 1f};
        private float[] _buildSpeedMult = new float[3] {1, 0.5f, 0f};
        private int _buildSPeedMultIndexToUse;

        private void Awake()
        {
            _parentHealth = GetComponent<ParentHealth>();
        }
        public void SetBuildSpeedMult()
        {
            float healthRatio = _parentHealth.GetHealthRatio();
            SetBuildSpeedMultIndex(healthRatio);            
        }
        private void SetBuildSpeedMultIndex(float healthRatio)
        {
            _buildSPeedMultIndexToUse = GetBuildSpeedMultIndex(healthRatio);            
        }
        public int GetBuildSpeedMultIndex(float healthRatio)
        {
            for (int i = 0; i < _ratioThreshHolds.Length; i++)
            {
                if (healthRatio <= _ratioThreshHolds[i])
                {
                    return i;
                }
            }
            return 0;            
        }
        public void SetBuildSpeedMultMax()
        {
            SetBuildSpeedMultIndex(0);
        }
        public float GetBuildSpeedMult()
        {
            return _buildSpeedMult[_buildSPeedMultIndexToUse];
        }
    }
}
