using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.UI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private MainManager _mainManager;
        [SerializeField] private PauseScript _pauseScript;

        public void MenuButtonClicked()
        {
            if (_menu.activeInHierarchy)
            {
                // Time.timeScale = 1;
                // _mainManager.TimeStopped = false;
                // _mainManager.Paused = false;
                _pauseScript.SetMenuPause(false);
                _menu.SetActive(false);                
            }
            else
            {
                // Time.timeScale = 0;
                // _mainManager.TimeStopped = true;
                // _mainManager.Paused = true;
                _pauseScript.SetMenuPause(true);
                _menu.SetActive(true);
            }
        }
    }
}
