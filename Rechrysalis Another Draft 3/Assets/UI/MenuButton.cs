using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.UI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private MainManager _mainManager;

        public void MenuButtonClicked()
        {
            if (_menu.activeInHierarchy)
            {
                Time.timeScale = 1;
                _mainManager.Paused = false;
                _menu.SetActive(false);                
            }
            else
            {
                Time.timeScale = 0;
                _mainManager.Paused = true;
                _menu.SetActive(true);
            }
        }
    }
}
