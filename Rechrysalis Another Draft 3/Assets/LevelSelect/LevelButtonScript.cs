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
        private bool _deubBool = false;
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        private int _levelForThisButton;
        [SerializeField] private TMP_Text _buttonName;
        public void SetLevelForThisButton(int level)
        {
            _levelForThisButton = level;
            if (_deubBool)
            {
                Debug.Log($"level " + level);
            }
            int levelNumber = level + 0;
            _buttonName.text = $"Level " + levelNumber;

        }
        public void LevelButtonClicked()
        {
            _compsAndUnitsSO.Level = _levelForThisButton;
            SceneManager.LoadScene("FreeEnemyLevel");
        }
    }
}
