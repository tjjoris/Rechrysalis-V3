using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.HatchEffect
{
    public class HatchEffectManager : MonoBehaviour
    {
        private HatchEffectSO _hatchEffectSO;
        private HETimer _hETimer;
        private bool _affectAll = true;
        public bool AffectAll {get{return _affectAll;}}
        [SerializeField] private TMP_Text _name;

        public void Initialize(HatchEffectSO _hatchEffectSO)
        {
            Debug.Log($"Name ");
            this._hatchEffectSO = _hatchEffectSO;
            _hETimer = GetComponent<HETimer>();
            _name.text = _hatchEffectSO.HatchEffectName;


        }
    }
}
