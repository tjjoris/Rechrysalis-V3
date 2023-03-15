using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.UI
{
    public class PlayerPrefsInteract : MonoBehaviour
    {
        private const string TARGETDURINGTARGETMODE = "TargetDuringTargetMode";
        public Action _changePlayerPrefs;

        public void SetTargetDuringTargetMode(bool value)
        {
            int number = 0;
            if (value)
            {
                number = 1;
            }
            PlayerPrefs.SetInt(TARGETDURINGTARGETMODE, number);
            _changePlayerPrefs?.Invoke();
        }
        public bool GetTargetDuringTargetMode()
        {
            if (PlayerPrefs.GetInt(TARGETDURINGTARGETMODE) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
