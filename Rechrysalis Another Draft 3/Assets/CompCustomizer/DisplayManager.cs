using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.CompCustomizer
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField]private TMP_Text _info;
        private string _initialText = "Pick a unit or hatch effect and apply it to a comp slot twice.  At least one must be a unit.";
        public void Initialize()
        {
            _info.text = _initialText;
        }
    }
}
