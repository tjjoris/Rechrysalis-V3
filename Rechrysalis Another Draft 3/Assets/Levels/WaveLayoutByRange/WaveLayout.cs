using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "new WaveLayout", menuName = "Unit/FreeUnitLayout/WaveLayout")]
    public class WaveLayout : ScriptableObject
    {

        
        [SerializeField] private Vector2[] _unitInWave;
        public Vector2[] UnitInWave => _unitInWave;
        [SerializeField] private float _zOffset = -1f;

        public Vector3 GetUnitPosInWave(int _unitIndex)
        {
            return new Vector3 (_unitInWave[_unitIndex].x, _unitInWave[_unitIndex].y, _zOffset);
        }
    }
}
