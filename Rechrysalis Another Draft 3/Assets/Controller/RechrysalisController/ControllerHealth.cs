using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Attacking;
using Rechrysalis.UI;

namespace Rechrysalis.Controller
{

    public class ControllerHealth : MonoBehaviour
    {
        [SerializeField] private float _healthMax;
        [SerializeField] private float _healthCurrent;
        [SerializeField] private ControllerHPBar _controllerHPBar;
        private List<GameObject> _allUnits;

        public void Initialize(float _healthMax, List<GameObject> _allUnits)
        {
            this._healthMax = _healthMax;
            _healthCurrent = _healthMax;
            this._allUnits = _allUnits;
            _controllerHPBar?.Initialize(_healthMax);
            SubscribeToControllerDamage();
        }
        public void TakeDamage(float _damageAmount)
        {
            _healthCurrent -= _damageAmount;
            _controllerHPBar?.ChangeHPBar(_healthCurrent);
        }
        public void SubscribeToControllerDamage()        
        {            
            if ((_allUnits != null) && (_allUnits.Count > 0))
            {
                foreach (GameObject _unit in _allUnits)
                {
                    if (_unit != null)
                    {
                        DamagesController _damagesController = _unit.GetComponent<DamagesController>();
                        if (_damagesController != null)
                        {
                            // Debug.Log($"adding damages controller");
                            _damagesController._damagesControllerAction -= TakeDamage;
                            _damagesController._damagesControllerAction += TakeDamage;
                        }
                    }
                }
            }
        }
        private void OnEnable()
        {
            SubscribeToControllerDamage();
        }
        private void OnDisable()
        {
            if (_allUnits.Count > 0)
            {
                foreach (GameObject _unit in _allUnits)
                {
                    if (_unit != null)
                    {
                        DamagesController _damagesController = _unit.GetComponent<DamagesController>();
                        if (_damagesController != null)
                        {
                            _damagesController._damagesControllerAction -= TakeDamage;
                        }
                    }
                }
            }
            
        }

    }
}
