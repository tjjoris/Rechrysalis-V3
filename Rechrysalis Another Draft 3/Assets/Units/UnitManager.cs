using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private UnitStatsSO _unitStats;
        public UnitStatsSO UnitStats {get{return _unitStats;}}
        public void Initialize(UnitStatsSO _unitStats)
        {
            this._unitStats = _unitStats;
            GetComponent<ProjectilesPool>()?.CreatePool(10, 5, _unitStats.ProjectileSprite);
        }
    }
}
