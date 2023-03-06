using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis.UI
{
    public class MainMenuButton : MonoBehaviour
    {
        public void MainMenuClicked()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Start");
        }
    }
}
