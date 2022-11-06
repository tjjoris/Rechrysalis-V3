using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectManager : MonoBehaviour
    {
        private HatchEffectSO _hatchEffectSO;
        private HETimer _hETimer;
        private bool _affectAll = true;
        public bool AffectAll {get{return _affectAll;}}

        public void Initialize(HatchEffectSO _hatchEffectSO)
        {
            this._hatchEffectSO = _hatchEffectSO;
            _hETimer = GetComponent<HETimer>();

        }
    }
}
