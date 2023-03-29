using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class FreeChrysalisStoresHealth : MonoBehaviour
    {
        [SerializeField] private float _storedHealth;
        public float StoredHealth => _storedHealth;

        public void SetStoredHealth(float amount)
        {
            Debug.Log($"set stored health " + amount);
            _storedHealth = amount;
        }
    }
}
