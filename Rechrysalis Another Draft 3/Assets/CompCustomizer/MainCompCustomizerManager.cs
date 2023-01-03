using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using UnityEngine.SceneManagement;


namespace Rechrysalis.CompCustomizer
{
    public class MainCompCustomizerManager : MonoBehaviour
    {
        [SerializeField] private CompCustomizerSO _compCustomizerSO;
        public CompCustomizerSO CompCustomizerSO { get{ return _compCustomizerSO; } set{ _compCustomizerSO = value; } }
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        public CompsAndUnitsSO CompsAndUnitsSO { get{ return _compsAndUnitsSO; } set{ _compsAndUnitsSO = value; } }
        [SerializeField] private SelectionInitialize _selectionInitialize;
        [SerializeField] private CompInitialize _compInitialize;
        
        [SerializeField] private CompSO _compSO;
        public CompSO CompSO { get{ return _compSO; } set{ _compSO = value; } }
        [SerializeField] private bool _debugBool = true;
        [SerializeField] private Transform _movingButtonHolder;

        private void OnEnable()
        {
            _compInitialize._droppedIntoVertical -= ButtonDroppedIntoComp;
            _compInitialize._droppedIntoVertical += ButtonDroppedIntoComp;
        }
        private void OnDisable()
        {
            _compInitialize._droppedIntoVertical -= ButtonDroppedIntoComp;
        }
        private void Start()
        {
            _compSO = _compsAndUnitsSO.CompsSO[0];
            _selectionInitialize.Initialize(_compCustomizerSO, _movingButtonHolder.transform, _compSO);
            _compInitialize.Initialize(_compCustomizerSO, _compsAndUnitsSO.CompsSO[0], _movingButtonHolder, _compsAndUnitsSO);
        }
        public void ContinueClicked()
        {
            if (_compInitialize.CheckIfCanContinue())
            {
                Debug.Log($"continue");
                SetComp();
                SceneManager.LoadScene("FreeEnemyLevel");
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
        private void ButtonDroppedIntoComp(CompVerticalManager compVerticalManager, CompUpgradeManager compUpgradeManager)        
        {
            if (_debugBool)
                Debug.Log($"button dropped into comp" + compUpgradeManager.GetUpgradeType());
            if (compUpgradeManager.GetUpgradeType() == UpgradeTypeClass.UpgradeType.SingleHeart)
            {
                if (_compsAndUnitsSO.ControllerHPTokensCurrent < _compsAndUnitsSO.ControllerHPTokensMax)
                {
                    _compsAndUnitsSO.AddControllerHPTokens(compUpgradeManager.GetUpgradeTypeClass().GetControllerHeartUpgrade().HeartCount);
                    Destroy(compUpgradeManager.gameObject);
                }
            }
        }
    }
}
