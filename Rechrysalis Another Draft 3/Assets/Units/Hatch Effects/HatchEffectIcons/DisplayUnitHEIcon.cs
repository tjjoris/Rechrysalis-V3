using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class DisplayUnitHEIcon : MonoBehaviour
    {
        [SerializeField] private GameObject _damageIconPrefab;
        [SerializeField] private GameObject _defenceIconPrefab;
        [SerializeField] private GameObject _rangeIconPrefab;
        [SerializeField] private GameObject _speedIconPrefab;
        [SerializeField] private GameObject _buildSpeedIconPrefab;
        [SerializeField] private GameObject _burstHealIconPrefab;
        [SerializeField] private GameObject _aoeIconPrefab;
    }
}
