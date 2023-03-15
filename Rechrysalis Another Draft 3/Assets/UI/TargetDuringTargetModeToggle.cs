using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rechrysalis.UI
{
    public class TargetDuringTargetModeToggle : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private PlayerPrefsInteract _playerPrefsInteract;

        private void Start()
        {
            _toggle.isOn = _playerPrefsInteract.GetTargetDuringTargetMode();
        }
        public void TogglePressed()
        {
            _playerPrefsInteract.SetTargetDuringTargetMode(_toggle.isOn);
        }
    }
}
