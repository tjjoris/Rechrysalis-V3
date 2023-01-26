using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class VerticalBoundsYUpdate : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _leftBoxCollider;
        [SerializeField] private BoxCollider2D _rightBoxCollider;

        public void UpdateVerticalBoxColliders(Transform controllerTransform)
        {
            _leftBoxCollider.transform.position = new Vector2(_leftBoxCollider.transform.position.x, controllerTransform.position.y);
            _rightBoxCollider.transform.position = new Vector2 (_rightBoxCollider.transform.position.x, controllerTransform.position.y);
        }
    }
}
