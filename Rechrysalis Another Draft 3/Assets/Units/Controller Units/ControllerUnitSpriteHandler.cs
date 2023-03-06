using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class ControllerUnitSpriteHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSpriteFunction (Sprite _sprite) {
            _spriteRenderer.sprite = _sprite;
            
        }
        public void TintSpriteRed ()
        {
            _spriteRenderer.color = Color.red;
        }
        public void TintSpriteMagenta()
        {
            _spriteRenderer.color = Color.magenta;
        }
    }
}
