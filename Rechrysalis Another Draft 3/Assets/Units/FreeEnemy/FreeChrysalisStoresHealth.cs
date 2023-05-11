using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class FreeChrysalisStoresHealth : MonoBehaviour
    {
        private bool _debugBool = false;
        [SerializeField] private float _storedHealth;
        public float StoredHealth => _storedHealth;

        public void SetStoredHealth(float amount)
        {
            if (_debugBool) Debug.Log($"set stored health " + amount);
            _storedHealth = amount;
        }
    }
}
