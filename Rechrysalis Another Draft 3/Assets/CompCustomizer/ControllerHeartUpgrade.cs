using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "ControllerHeartUpgrade" , menuName = "CompCustomizer/CompHeartUpgrade")]
    public class ControllerHeartUpgrade : MonoBehaviour
    {
        [SerializeField] private GameObject _controllerHeartUpgraePrefab;
        public GameObject ControllerHeartUpgradePrefab => _controllerHeartUpgraePrefab;
        [SerializeField] private UpgradeTypeClass.UpgradeType _upgradeType;
        public UpgradeTypeClass.UpgradeType UpgradeType => _upgradeType;
        [SerializeField] private int _heartCount;
        public int HeartCount => _heartCount;
    }
}
