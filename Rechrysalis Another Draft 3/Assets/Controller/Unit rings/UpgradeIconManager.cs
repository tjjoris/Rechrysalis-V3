using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class UpgradeIconManager : MonoBehaviour
    {
        // private Sprite _unitSprite;
        // public Sprite UnitSprite {set {_unitSprite = value;}}
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _indicatorWhite;
        public GameObject IndicatorWhite {get {return _indicatorWhite;}}
        [SerializeField] private GameObject _indicatorBlack;
        public GameObject IndicatorBlack {get {return _indicatorBlack;}}

        public void Initialize (Sprite _sprite)
        {
            _spriteRenderer.sprite = _sprite;
            _indicatorWhite.SetActive(false);
        }
        public void MouseOverThisUpgrade()
        {
            _indicatorBlack.SetActive(true);
            _indicatorWhite.SetActive(false);
        }
        public void MouseOffThisUpgrade()
        {
            _indicatorBlack.SetActive(false);
            _indicatorWhite.SetActive(true);
        }
    }
}
