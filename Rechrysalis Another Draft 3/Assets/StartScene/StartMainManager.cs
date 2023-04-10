using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rechrysalis.Unit;
using Rechrysalis.UI;
using UnityEngine.UI;

namespace Rechrysalis
{
    public class StartMainManager : MonoBehaviour
    {
        [SerializeField]private CompsAndUnitsSO _compsAndUnits;
        [SerializeField] private NewGameStatus _newGameStatus;
        [SerializeField] private Toggle _hasManaToggle;
        private void Awake()
        {
            PlayerPrefsChanged();
        }
        private void Start()
        {
            SetNewGameStatus();
        }
        private void OnEnable()
        {
            PlayerPrefsInteract._changePlayerPrefs +=  PlayerPrefsChanged;
        }
        private void OnDisable()
        {
            PlayerPrefsInteract._changePlayerPrefs -= PlayerPrefsChanged;
        }
        private void PlayerPrefsChanged()
        {
            _hasManaToggle.isOn = PlayerPrefsInteract.GetHasMana();
        }
        public void LevelSelect()
        {
            _compsAndUnits.Level = 0;
            SceneManager.LoadScene("LevelSelect");
        }
        public void NewGameClicked()
        {
            _compsAndUnits.Level = 0;
            _compsAndUnits.CompsSO[0].ParentUnitClassList.Clear();
            _compsAndUnits.ControllerHPTokensCurrent = _compsAndUnits.ControllerHPTokensMax;
            // _compsAndUnits.CompsSO[0].ParentUnitClassList = new List<ParentUnitClass>();
            SceneManager.LoadScene("CompCustomizer");
        }
        public void CustomCompClicked()
        {
            SceneManager.LoadScene("CustomCustomizer");
        }
        private void SetNewGameStatus()
        {
            if (_compsAndUnits.NewGameStatusEnum == CompsAndUnitsSO.NewGameStatus.NewGame)
            {
                _newGameStatus.NewGame();
            }
            else if (_compsAndUnits.NewGameStatusEnum == CompsAndUnitsSO.NewGameStatus.Lost)
            {
                _newGameStatus.YouLost();
            }
            else if (_compsAndUnits.NewGameStatusEnum == CompsAndUnitsSO.NewGameStatus.Won)
            {
               _newGameStatus.YouWon();
            }
        }
        public void ToggleHasMana()
        {
            PlayerPrefsInteract.SetHasMana(_hasManaToggle.isOn);
        }
    }
}
