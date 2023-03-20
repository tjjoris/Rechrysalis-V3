using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class PauseScript : MonoBehaviour
    {
        [SerializeField] private bool _timeFrozen;
        [SerializeField] private bool _menuPause;
        [SerializeField] private bool _targetingPause;
        [SerializeField] private MainManager _mainMangaer;

        private void Awake()
        {
            _mainMangaer = GetComponent<MainManager>();
        }

        public void SetTimeFrozen (bool timeFrozen)
        {
            _timeFrozen = timeFrozen;
            CheckToCallPauseUnPause();
        }
        public bool GetTimeFrozen()
        {
            return _timeFrozen;
        }
        public void SetMenuPause (bool menuPause)
        {
            _menuPause = menuPause;
            CheckToCallPauseUnPause();
        }
        public bool GetMenuPause()
        {
            return _menuPause;
        }
        public void SetTargetingPause (bool targetingPause)
        {
            _targetingPause = targetingPause;
            CheckToCallPauseUnPause();
        }
        public bool GetTargetingPause()
        {
            return _targetingPause;
        }

        private void CheckToCallPauseUnPause()
        {
            if  (IsPaused())
            {
                _mainMangaer.PauseUnPause(false);
            }
            else
            {
                _mainMangaer.PauseUnPause(true);
            }
        }

        public bool IsPaused()
        {
            if ((!_timeFrozen) && (!_menuPause) && (!_targetingPause))
            {
                return true;
            }
            return false;
        }
    }
}
