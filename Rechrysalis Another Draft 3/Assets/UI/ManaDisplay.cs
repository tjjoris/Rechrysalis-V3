using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.UI
{
    public class ManaDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _manaNumberText;

        public void SetManaNumber(float _manaNumber)
        {
            int _manaInt = Mathf.FloorToInt(_manaNumber);
            _manaNumberText.text = _manaInt.ToString();
        }
    }
}
