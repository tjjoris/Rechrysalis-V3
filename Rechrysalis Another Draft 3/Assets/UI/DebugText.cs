using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.UI
{
    public class DebugText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _debugText;

        private void Start()
        {
            DebugTextStatic.DebugText = this;
        }
        public void DisplayText(string textToDisplay)
        {
            _debugText.text = textToDisplay;
        }
    }
}
