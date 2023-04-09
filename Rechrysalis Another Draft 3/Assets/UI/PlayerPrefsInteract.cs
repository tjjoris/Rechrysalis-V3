using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.UI
{
    public class PlayerPrefsInteract : MonoBehaviour
    {
        private const string TARGETDURINGTARGETMODE = "TargetDuringTargetMode";
        private const string HEALTHTOBUILDTIME  = "HealthToBuildTime";
        private const string HASBASICUNIT = "HasBasicUnit";
        private const string HASMANA = "HasMana";
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
        public bool GetTargetOnlyDuringTargetMode()
        {
            if (PlayerPrefs.GetInt(TARGETDURINGTARGETMODE) == 1)
            {
                return true;
            }
            return false;
        }
        public void SetHealthToBuildTime(int value)
        {
            if ((value < 0) || (value > 2)) return;
            PlayerPrefs.SetInt(HEALTHTOBUILDTIME, value);
        }
        public int GetHealthToBuildTime()
        {
            return PlayerPrefs.GetInt(HEALTHTOBUILDTIME);
        }
        public void SetHasBasicUnit(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetInt(HASBASICUNIT, 1);
                return;
            }
            PlayerPrefs.SetInt(HASBASICUNIT, 0);
        }
        public int GetHasBasicUnit()
        {
            if (PlayerPrefs.GetInt(HASBASICUNIT) == 1)
            {
            return true;
            }
            return false;
        }
        public void SetHasMana(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetInt(HASMANA, 1);
                return;
            }
            PlayerPrefs.SetInt(HASMANA, 0);
        }
        public bool GetHasMana()
        {
            if (PlayerPrefs.GetInt(HASMANA) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
