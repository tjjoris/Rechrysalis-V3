using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Movement
{
    public class BlockCharacterCollision : MonoBehaviour
    {
        private bool _deubBool = false;
        [SerializeField] private CircleCollider2D _characterCollider;
        [SerializeField] private CircleCollider2D _characterBlockerCollider;

        private void Awake()
        {
            Physics2D.IgnoreCollision(_characterCollider, _characterBlockerCollider, true);
        }
        private void OnEnable()
        {
            if (_deubBool)
            {
                Debug.Log($"collision ignoring enabled");
            }
            Physics2D.IgnoreCollision(_characterCollider, _characterBlockerCollider, true);
        }
        // private void Update()
        // {
        //     // if (GetComponent<Controller.ManaGenerator>() != null)
        //     // {
        //     //     Debug.Log($" ignoring? " +Physics2D.GetIgnoreCollision(_characterCollider, _characterBlockerCollider));
        //     // }
        // }

    }
}
