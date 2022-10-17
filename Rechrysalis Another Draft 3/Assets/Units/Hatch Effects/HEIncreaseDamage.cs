using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HEIncreaseDamage : MonoBehaviour
    {
        // [SerializeField] private float _duration;
        // [SerializeField] private float _timeCurrent;
        [SerializeField]private float _damageIncrease;
        [SerializeField] private bool _allUnits;

        // public void Tick(float _timeAmount)
        // {
        //     _timeCurrent += _timeAmount;
        // }
        // public bool CheckIsExpired()
        // {
        //     if (_timeCurrent >= _duration)
        //     {
        //         return true;
        //     }
        //     return false;
        // }
        // public void DestroySelf()
        // {
        //     Destroy(gameObject);
        // }
        public float GetDamageIncrease()
        {
            return _damageIncrease;
        }
    }
}
