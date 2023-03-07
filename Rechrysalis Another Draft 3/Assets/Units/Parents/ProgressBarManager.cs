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
        [SerializeField] private Color _chrysalisBackG = new Color(0.66f, 0.88f, 0.66f);
        [SerializeField] private Color _chrysalisFill = new Color(0.4f, 0.6f, 0.6f);
        [SerializeField] private Color _chargeUpBackG = new Color(0.74f, 0.76f, 0.8f);
        [SerializeField] private Color _chargeUpFill = new Color(0.44f, 0.46f, 0.6f);
        [SerializeField] private Color _windDownBackG = new Color(0.8f, 0.8f, 0.4f);
        [SerializeField] private Color _windDownFill = new Color (0.55f, 0.55f, 0.28f);

        public void StrechFillByValue(float fillValue)
        {
            Vector2 strechScale = new Vector2 (fillValue, 1);
            _fillTransform.localScale = strechScale;
        }
        public void TintChargeUp()
        {
            _backGSprite.color = _chargeUpBackG;
            _fillSprite.color = _chargeUpFill;
        }
        public void TintWindDown()
        {
            _backGSprite.color = _windDownBackG;
            _fillSprite.color = _windDownFill;
        }
        public void TintChrysalis()
        {
            _backGSprite.color = _chrysalisBackG;
            _fillSprite.color = _chrysalisFill;
        }
    }
}
