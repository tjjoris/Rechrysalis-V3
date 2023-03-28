using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class FreeEnemyUnitInstantiator : MonoBehaviour
    {
        private MainManager _mainManager;
        private ControllerManager _thisController;
        private WaveLayoutsByRange _waveLayoutsByRange;
        private ControllerManager _enemyController;
        [SerializeField] private GameObject _freeUnitPrefab;
        private FreeEnemyInitialize _freeEnemeyInitialize;
        private PlayerUnitsSO _thesePlayerUnitsSO;

        public void Initialize(MainManager mainManager, ControllerManager thisController, ControllerManager enemyController)
        {
            _mainManager = mainManager;
            _thisController = thisController;
            _waveLayoutsByRange = GetComponent<WaveLayoutsByRange>();
            _freeEnemeyInitialize = GetComponent<FreeEnemyInitialize>();
            _thesePlayerUnitsSO = _freeEnemeyInitialize.PlayerUnitsSO;
        }
        public void InstantiateUnit(ParentUnitClass parentUnitClass, int unitInWaveIndex)
        {
            UnitStatsSO basicUnitStatsSO = parentUnitClass.UTCBasicUnit.GetUnitStatsSO();
            Vector3 newUnitPos = _waveLayoutsByRange.GetWaveLayoutByRange(parentUnitClass.BasicUnitClass.Range).GetUnitPosInWave(unitInWaveIndex);
            newUnitPos = new Vector3(newUnitPos.x, newUnitPos.y + _enemyController.transform.position.y);
            GameObject newFreeEnemy = Instantiate(_freeUnitPrefab, newUnitPos, Quaternion.identity, gameObject.transform);
            newFreeEnemy.transform.Rotate(new Vector3(0, 0, 180f));
            _thesePlayerUnitsSO.ParentUnits.Add(newFreeEnemy);
            newFreeEnemy.name = basicUnitStatsSO.UnitName + " " + unitInWaveIndex.ToString();
            ParentUnitManager parentUnitManager = newFreeEnemy.GetComponent<ParentUnitManager>();
            parentUnitManager?.Initialize(_thisController.ControllerIndex, unitInWaveIndex, _thesePlayerUnitsSO, transform, null, parentUnitClass, _mainManager);
        }
    }
}
