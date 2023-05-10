using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class FreeEnemyUnitInstantiator : MonoBehaviour
    {
        private MainManager _mainManager;
        private ControllerManager _controllerManager;
        private WaveLayoutsByRange _waveLayoutsByRange;
        private ControllerManager _enemyController;
        [SerializeField] private GameObject _freeUnitPrefab;
        private FreeEnemyInitialize _freeEnemeyInitialize;
        private PlayerUnitsSO _thesePlayerUnitsSO;
        // private FreeChrysalisStoresHealth _freeChrysalisStoresHealth;

        private void Awake()
        {

            _freeEnemeyInitialize = GetComponent<FreeEnemyInitialize>();
        }
        public void Initialize(MainManager mainManager, ControllerManager thisController, ControllerManager enemyController)
        {
            _mainManager = mainManager;
            _controllerManager = thisController;
            _enemyController = enemyController;
            _waveLayoutsByRange = _freeEnemeyInitialize.WaveLayoutsByRange;
            _thesePlayerUnitsSO = _freeEnemeyInitialize.PlayerUnitsSO;
            // _freeChrysalisStoresHealth = GetComponent<FreeChrysalisStoresHealth>();
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
            parentUnitManager?.Initialize(_controllerManager.ControllerIndex, unitInWaveIndex, _thesePlayerUnitsSO, transform, null, parentUnitClass, _mainManager);
            parentUnitManager.ParentUnitClass = parentUnitClass;
            _controllerManager.ParentUnitManagers.Add(parentUnitManager);
            if (parentUnitManager.GetComponent<ParentHealth>() != null)
            {
                _controllerManager.ParentHealths.Add(parentUnitManager.GetComponent<ParentHealth>());
            }
            Mover parentUnitMover = parentUnitManager.GetComponent<Mover>();
            if (parentUnitMover != null)
            {
                _controllerManager.ParentUnitMovers.Add(parentUnitMover);
            }
            ParentFreeEnemyManager _freeParentManager = newFreeEnemy.GetComponent<ParentFreeEnemyManager>();
            _freeParentManager?.Initialize(_controllerManager, parentUnitClass.BasicUnitClass, unitInWaveIndex, _mainManager.CompsAndUnitsSO, _controllerManager.ControllerIndex);            
            parentUnitManager.ChildUnitManagers.Add(_freeParentManager.BasicUnitManager);
            parentUnitManager.ChildChrysaliiUnitManagers.Add(_freeParentManager.ChrysalisUnitManager);
            // _freeParentManager.ChrysalisUnitManager.Initialize(_controllerManager, _controllerManager.ControllerIndex, parentUnitClass.BasicUnitClass, )
            _freeParentManager.ChrysalisUnitManager.GetComponent<ChrysalisTimer>()?.Initialize(parentUnitClass.BasicUnitClass.BuildTime, 0, null, parentUnitManager);
            newFreeEnemy.GetComponent<ParentHealth>()?.SetMaxHealth(parentUnitClass.BasicUnitClass.HPMax);
            newFreeEnemy.GetComponent<FreeChrysalisStoresHealth>()?.SetStoredHealth(parentUnitClass.BasicUnitClass.ChrysalisHPMax);
            newFreeEnemy.GetComponent<Mover>()?.Initialize(_controllerManager.ControllerIndex, _mainManager);
            // _thesePlayerUnitsSO.ActiveUnits.Add(newFreeEnemy);
            // _allUnits.Add(_freeParentManager.UnitManager.gameObject);
            AIFocusFireOnInitialzie(basicUnitStatsSO, parentUnitManager);
            Die die = parentUnitManager.GetComponent<Die>();
            if (die != null)
            {
                die.ControllerProgressValue = parentUnitClass.BasicUnitClass.ControllerLifeCostMult;
                die.FreeControllerProgressBar = _freeEnemeyInitialize.FreeControllerControllerProgressBar;
            }
        }
        private static void AIFocusFireOnInitialzie(UnitStatsSO unitStats, ParentUnitManager parentUnitManager)
        {

            foreach (UnitManager childUnit in parentUnitManager.ChildUnitManagers)
            {
                if (childUnit != null)
                {
                    if ((!unitStats.AIFocusFire))
                    {
                        if ((childUnit.GetComponent<TargetPrioratizeByScore>() != null))
                        {
                            Destroy(childUnit.GetComponent<TargetPrioratizeByScore>());
                        }
                    }
                    else
                    {
                        childUnit.ControllerUnitSpriteHandler.TintSpriteRed();
                        //this unit has focus fire, and already starts with the focus fire component.
                    }
                }
            }
        }
    }
}
