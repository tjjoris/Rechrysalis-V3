using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using UnityEngine.SceneManagement;
using Rechrysalis.Controller;
using Rechrysalis.UI;


namespace Rechrysalis.CompCustomizer
{
    public class MainCompCustomizerManager : MonoBehaviour
    {
        private bool _debugBool = false;
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        public CompsAndUnitsSO CompsAndUnitsSO { get{ return _compsAndUnitsSO; } set{ _compsAndUnitsSO = value; } }
        [SerializeField] private SelectionInitializeMain _selectionInitializeMain;
        public SelectionInitializeMain SelectionInitializeMain => _selectionInitializeMain;
        [SerializeField] private SelectionInitialize _selectionInitialize;
        [SerializeField] private CompInitialize _compInitialize;
        
        [SerializeField] private CompSO _compSO;
        [SerializeField] private ControllerHPTokens _controllerHPTokens;
        public CompSO CompSO { get{ return _compSO; } set{ _compSO = value; } }
        [SerializeField] private Transform _movingButtonHolder;
        [SerializeField] private LevelDisplay _levelDisplay;
        private ContinueReturnsToStart _continueReturnsToStart;
        private ContinueStartsFreeUnitLevel _continueStartsFreeUnitLevel;

        // private void OnEnable()
        // {
        //     _compInitialize._droppedIntoVertical -= ButtonDroppedIntoComp;
        //     _compInitialize._droppedIntoVertical += ButtonDroppedIntoComp;
        // }
        // private void OnDisable()
        // {
        //     _compInitialize._droppedIntoVertical -= ButtonDroppedIntoComp;
        // }
        private void Awake()
        {
            _continueReturnsToStart = GetComponent<ContinueReturnsToStart>();
            _continueStartsFreeUnitLevel = GetComponent<ContinueStartsFreeUnitLevel>();
        }
        private void Start()
        {
            _compSO = _compsAndUnitsSO.CompsSO[0];
            _selectionInitializeMain.Initialize(_compCustomizerSO, _movingButtonHolder, _compSO);
            _selectionInitialize = _selectionInitializeMain.SelectionInitialize;
            // if (PlayerPrefsInteract.GetCustomizeOnlyHEAndUnit())
            // {
            //     _selectionInitialize = _selectionInitializeMain.AddComponent<SelectionInitialize>();
            //     _selectionInitialize.Initialize(_compCustomizerSO, _movingButtonHolder.transform, _compSO);
            // }
            
            _compInitialize.Initialize(_compCustomizerSO, _compsAndUnitsSO.CompsSO[0], _movingButtonHolder, _compsAndUnitsSO, _controllerHPTokens);
            _controllerHPTokens.Initialize(_compsAndUnitsSO);
            _levelDisplay.SetLevelText(_compsAndUnitsSO.Level);
        }
        public void ContinueClicked()
        {
            if (_compInitialize.CheckIfCanContinue())
            {
                if (_debugBool)
                {
                    Debug.Log($"continue");
                }
                SetComp();
                // if (_compsAndUnitsSO.Level < _compsAndUnitsSO.Levels.Length)
                // SceneManager.LoadScene("FreeEnemyLevel");
                // else
                // SceneManager.LoadScene("Start");
                if (_continueStartsFreeUnitLevel != null)
                {                    
                    _continueStartsFreeUnitLevel?.ContinueClicked();
                    return;
                }
                    _continueReturnsToStart?.ContinueClicked();
            }
        }
        private void SetComp()
        {
            _compSO.ParentUnitClassList.Clear();
            for (int i=0; i<_compInitialize.VerticalManagers.Count; i++)
            {                
            _compSO.ParentUnitClassList.Add(_compInitialize.VerticalManagers[i].GetParentUnitClass());
            }
        }
        // private void ButtonDroppedIntoComp(CompVerticalManager compVerticalManager, CompUpgradeManager compUpgradeManager)        
        // {
        //     if (_debugBool)
        //         Debug.Log($"button dropped into comp" + compUpgradeManager.GetUpgradeType());
        //     if ((compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.SingleHeart) && (_controllerHPTokens.IsMissingTokens()))
        //     {
        //         _controllerHPTokens.AddTokens(compUpgradeManager.GetUpgradeTypeClass().GetControllerHeartUpgrade().HeartCount);
        //         Destroy(compUpgradeManager.gameObject);
        //         // if (_compsAndUnitsSO.ControllerHPTokensCurrent < _compsAndUnitsSO.ControllerHPTokensMax)
        //         // {
        //         //     _compsAndUnitsSO.AddControllerHPTokens(compUpgradeManager.GetUpgradeTypeClass().GetControllerHeartUpgrade().HeartCount);
        //         //     Destroy(compUpgradeManager.gameObject);
        //         // }
        //     }
        // }
    }
}
