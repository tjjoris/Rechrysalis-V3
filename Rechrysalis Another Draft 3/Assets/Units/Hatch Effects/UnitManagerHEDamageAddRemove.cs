using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.HatchEffect;

namespace Rechrysalis.Unit
{
    public class UnitManagerHEDamageAddRemove : MonoBehaviour
    {
        private UnitManager _unitManager;
        private void Awake()
        {
            _unitManager = GetComponent<UnitManager>();
        }
        public void HEContainsDamageChangeDamage(GameObject hatchEffect)
        {
            if (hatchEffect.GetComponent<HEIncreaseDamage>() == null) return;
            _unitManager.ReCalculateDamageChanges();
        }
    }
}
