using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class HilightRingUnitManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _body;

        public void SetSprite(Sprite sprite)
        {
            _body.sprite = sprite;
        }
        public void EnableGO()
        {
            gameObject.SetActive(true);
        }
        public void DisableGO()
        {
            gameObject.SetActive(false);
        }
    }
}
