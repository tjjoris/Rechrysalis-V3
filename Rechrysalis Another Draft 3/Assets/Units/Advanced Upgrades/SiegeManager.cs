using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Movement;

namespace Rechrysalis.AdvancedUpgrade
{
    public class SiegeManager : MonoBehaviour
    {
        private Mover _mover;
        private float _siegeDuration = 0;
        private float _timerCurrent = 0;
        private bool _hasBeenDeactivated = false;
        public void Initialize(Mover mover)
        {
            _mover = mover;
        }
        public void AddToSiegeDuration(float amountToAdd)
        {
            _siegeDuration += amountToAdd;
        }
        private void OnEnable()
        {
            _mover.AddSiegeInt(1);
            _timerCurrent = 0;
            _hasBeenDeactivated = false;
        }
        public void Tick(float timeAmount)
        {
            if (_hasBeenDeactivated == false)
            {
                _timerCurrent += timeAmount;
                if (_timerCurrent >= _siegeDuration)
                {
                    DeactivateSiege();
                }
            }
        }
        private void OnDisable()
        {
            if (_hasBeenDeactivated == false)
            {
                DeactivateSiege();
            }
        }
        public void DeactivateSiege()
        {
            _mover.AddSiegeInt(-1);
            _hasBeenDeactivated = true;
        }
    }
}
