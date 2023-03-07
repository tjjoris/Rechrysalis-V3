using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class ProgressBarManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backGSprite;
        public SpriteRenderer   BackGSprite => _backGSprite;
        [SerializeField] private SpriteRenderer _fillSprite;
        public SpriteRenderer FillSprite => _fillSprite;
        [SerializeField] private Transform _fillTransform;
        public Transform FillTransform => _fillTransform;

        public void StrechFillByValue(float fillValue)
        {
            Vector2 strechScale = new Vector2 (fillValue, 1);
            _fillTransform.localScale = strechScale;
        }
    }
}
