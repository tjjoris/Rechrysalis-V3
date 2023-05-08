using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.UI
{
    public static class PlayerPrefsInteract 
    {
        private const string TARGETDURINGTARGETMODE = "TargetDuringTargetMode";
        private const string HEALTHTOBUILDTIME  = "HealthToBuildTime";
        private const string HASBASICUNIT = "HasBasicUnit";
        private const string HASMANA = "HasMana";
        private const string CUSTOMIZEONLYHEANDUNIT = "CustomizeOnlyHEAndUnit";
        private const string ONLYONEHATCHEFFECT = "OnlyOneHatchEffect";
        public static Action _changePlayerPrefs;

        public static void SetTargetDuringTargetMode(bool value)
        {
            int number = 0;
            if (value)
            {
                number = 1;
            }
            PlayerPrefs.SetInt(TARGETDURINGTARGETMODE, number);
            _changePlayerPrefs?.Invoke();
        }
        public static bool GetTargetOnlyDuringTargetMode()
        {
            if (PlayerPrefs.GetInt(TARGETDURINGTARGETMODE) == 1)
            {
                return true;
            }
            return false;
        }
        public static void SetHealthToBuildTime(int value)
        {
            if ((value < 0) || (value > 2)) return;
            PlayerPrefs.SetInt(HEALTHTOBUILDTIME, value);
        }
        public static int GetHealthToBuildTime()
        {
            return PlayerPrefs.GetInt(HEALTHTOBUILDTIME);
        }
        public static void SetHasBasicUnit(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetInt(HASBASICUNIT, 1);
                return;
            }
            PlayerPrefs.SetInt(HASBASICUNIT, 0);
        }
        public static bool GetHasBasicUnit()
        {
            if (PlayerPrefs.GetInt(HASBASICUNIT) == 1)
            {
            return true;
            }
            return false;
        }
        public static void SetHasMana(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetInt(HASMANA, 1);
                return;
            }
            PlayerPrefs.SetInt(HASMANA, 0);
        }
        public static bool GetHasMana()
        {
            if (PlayerPrefs.GetInt(HASMANA) == 1)
            {
                return true;
            }
            return false;
        }
        public static void SetCustomizeOnlyHEAndUnit(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetInt(CUSTOMIZEONLYHEANDUNIT, 1);
                return;
            }
            PlayerPrefs.SetInt(CUSTOMIZEONLYHEANDUNIT, 0);
        }
        public static bool GetCustomizeOnlyHEAndUnit()
        {
            if (PlayerPrefs.GetInt(CUSTOMIZEONLYHEANDUNIT) == 1)
            {
                return true;
            }
            return false;
        }
        public static void SetOnlyOneHatchEffect(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetInt(ONLYONEHATCHEFFECT, 1);
                return;                
            }
            PlayerPrefs.SetInt(ONLYONEHATCHEFFECT, 0);            
        }
        public static bool GetOnlyOneHatchEffect()
        {
            if (PlayerPrefs.GetInt(ONLYONEHATCHEFFECT) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
