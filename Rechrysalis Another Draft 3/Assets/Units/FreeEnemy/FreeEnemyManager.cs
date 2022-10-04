using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    public class FreeEnemyManager : MonoBehaviour
    {
        public void Initialize(UnitStatsSO _unitStats)
        {
            GetComponent<ProjectilesPool>()?.CreatePool(_unitStats.AmountToPool, _unitStats.ProjectileSpeed, _unitStats.ProjectileSprite);
        }
    }
}
