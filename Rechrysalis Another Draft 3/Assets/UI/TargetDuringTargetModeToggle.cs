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

        public void TogglePressed()
        {
            _playerPrefsInteract.SetTargetDuringTargetMode(_toggle.isOn);
        }
    }
}
