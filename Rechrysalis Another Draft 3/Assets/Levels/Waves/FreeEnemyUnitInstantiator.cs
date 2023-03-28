using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class FreeEnemyUnitInstantiator : MonoBehaviour
    {
        private WaveLayoutsByRange _waveLayoutsByRange;
        private ControllerManager _enemyController;
        [SerializeField] private GameObject _freeUnitPrefab;

        public void Initialize(ControllerManager enemyController)
        {
            _waveLayoutsByRange = GetComponent<WaveLayoutsByRange>();
        }
        public void InstantiateUnit(ParentUnitClass parentUnitClass, int unitInWaveIndex)
        {
            Vector3 newUnitPos = _waveLayoutsByRange.GetWaveLayoutByRange(parentUnitClass.BasicUnitClass.Range).GetUnitPosInWave(unitInWaveIndex);
            newUnitPos = new Vector3(newUnitPos.x, newUnitPos.y + _enemyController.transform.position.y);
            GameObject newFreeEnemy = Instantiate(_freeUnitPrefab, newUnitPos, Quaternion.identity, gameObject.transform);
        }
    }
}
