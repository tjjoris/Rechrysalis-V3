using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class Health : MonoBehaviour
    {
       [SerializeField] private float _healthMax;
       [SerializeField] private float _healthCurrent;
       private Die _die;
       private Rechrysalize _rechrysalize;

       public void Initialize(float _healthMax)
       {
        this._healthMax = _healthMax;
        this._healthCurrent = _healthMax;
        _rechrysalize = GetComponent<Rechrysalize>();
        _die = GetComponent<Die>();
       }
       public void TakeDamage(float _damageAmount)
       {
        this._healthCurrent -= _damageAmount;     
        if (_healthCurrent <= 0)
        {
            _die?.UnitDies();  
            _rechrysalize?.UnitDies(); 
        }
       }
    }
}
