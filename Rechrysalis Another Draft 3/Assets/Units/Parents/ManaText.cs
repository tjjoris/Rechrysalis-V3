using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.Unit
{
    public class ManaText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _manaText;
        public void SetManaText(string manaText)
        {
            if (_manaText != null)
            {
                _manaText.text = manaText;
            }
        }
    }
}
