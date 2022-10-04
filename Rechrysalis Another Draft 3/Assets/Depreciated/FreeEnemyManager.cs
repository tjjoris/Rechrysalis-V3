using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.Unit
{
    public class FreeEnemyManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        public void Initialize(UnitStatsSO _unitStats)
        {
            GetComponent<ProjectilesPool>()?.CreatePool(_unitStats.AmountToPool, _unitStats.ProjectileSpeed, _unitStats.ProjectileSprite);
            _nameText.text = _unitStats.UnitName;
        }
    }
}
