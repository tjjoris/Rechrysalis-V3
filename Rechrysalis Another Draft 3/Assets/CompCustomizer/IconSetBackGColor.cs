using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class IconSetBackGColor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backG;
        private Color _selected;
        private Color _notSelected;

        public void SetBackGColour(Color _colour)
        {
            _backG.GetComponent<SpriteRenderer>();
            _selected = new Color(_colour.r, _colour.g, _colour.b, 1f);
            _notSelected = new Color(_colour.r, _colour.g, _colour.b, 0.5f);
            _backG.color = _notSelected;
        }
        public void NotSelected()
        {
            _backG.color = _notSelected;
        }
        public void Selected()
        {
            _backG.color = _selected;
        }
    }
}
