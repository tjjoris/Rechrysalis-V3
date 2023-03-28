using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis
{
    public class LevelSceneManagement : MonoBehaviour
    {
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;

        public void Initialize (CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
        }
        public void LevelBeat()
        {
            Debug.Log($"level beat");
            _compsAndUnitsSO.Level++;
            if (_compsAndUnitsSO.Level < _compsAndUnitsSO.Levels.Length)
            {
                GoToCompCustomizer();
            }
            else
            {
                _compsAndUnitsSO.NewGameStatusEnum = CompsAndUnitsSO.NewGameStatus.Won;
                SceneManager.LoadScene("Start");
            }
        }
        private void GoToCompCustomizer()
        {
            // _compCustomizerSO.NumberOfUpgrades = 1;
            SceneManager.LoadScene("CompCustomizer");
        }
    }
}
