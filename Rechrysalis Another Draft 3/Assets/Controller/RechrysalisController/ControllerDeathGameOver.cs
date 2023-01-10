using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis.Controller
{
    public class ControllerDeathGameOver : MonoBehaviour
    {
        public void GameOver (float _currentHP, CompsAndUnitsSO compsAndUnitsSO)
        {
            if (_currentHP <= 0)
            {
                compsAndUnitsSO.NewGameStatusEnum = CompsAndUnitsSO.NewGameStatus.Lost;
                SceneManager.LoadScene("Start");
            }
        }
    }
}
