using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.UI;

namespace Rechrysalis.Unit
{
    public class UnitManaCostText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _manaText;
        private void Awake()
        {
            if (!PlayerPrefsInteract.GetHasMana())
            {
                _manaText.gameObject.SetActive(false);
            }
        }
        public void SetManaText(string manaText)
        {
            if (_manaText != null)
            {
                _manaText.text = manaText;
            }
        }
    }
}
