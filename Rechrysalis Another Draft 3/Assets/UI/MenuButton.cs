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
                _pauseScript.SetMenuPause(false);
                _menu.SetActive(false);                
            }
            else
            {
                _pauseScript.SetMenuPause(true);
                _menu.SetActive(true);
            }
        }
    }
}
