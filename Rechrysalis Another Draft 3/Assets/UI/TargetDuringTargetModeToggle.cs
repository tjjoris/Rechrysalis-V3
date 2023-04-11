using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rechrysalis.UI
{
    public class TargetDuringTargetModeToggle : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        private void Start()
        {
            _toggle.isOn = PlayerPrefsInteract.GetTargetOnlyDuringTargetMode();
        }
        public void TogglePressed()
        {
            PlayerPrefsInteract.SetTargetDuringTargetMode(_toggle.isOn);
        }
    }
}
