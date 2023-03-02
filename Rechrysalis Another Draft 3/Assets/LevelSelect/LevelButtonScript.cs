using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Rechrysalis;

namespace Rechrysalis.LevelSelect
{
    public class LevelButtonScript : MonoBehaviour
    {
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        private int _levelForThisButton;
        [SerializeField] private TMP_Text _buttonName;
        public void SetLevelForThisButton(int level)
        {
            _levelForThisButton = level;

        }
        public void LevelButtonClicked()
        {
            _compsAndUnitsSO.Level = _levelForThisButton;
            SceneManager.LoadScene("FreeEnemyLevel");
        }
    }
}
