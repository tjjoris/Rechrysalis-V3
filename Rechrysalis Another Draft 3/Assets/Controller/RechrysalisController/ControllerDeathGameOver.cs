using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis.Controller
{
    public class ControllerDeathGameOver : MonoBehaviour
    {
        public void TakeDamage (float _currentHP)
        {
            if (_currentHP <= 0)
            {
                SceneManager.LoadScene("Start");
            }
        }
    }
}
