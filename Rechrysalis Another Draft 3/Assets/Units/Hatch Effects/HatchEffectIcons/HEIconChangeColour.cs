using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIconChangeColour : MonoBehaviour
    {
        private DisplayUnitHEIcon _displayUnitHEIcon;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void Initialize (DisplayUnitHEIcon displayUnitHEIcon)
        {
            _displayUnitHEIcon = displayUnitHEIcon;
        }
        public void SetColourToActive()
        {
            _spriteRenderer.color = _displayUnitHEIcon.ActiveColour;
        }
        public void SetColorToInactive()
        {
            _spriteRenderer.color = _displayUnitHEIcon.InactiveColour;
        }
    }
}
