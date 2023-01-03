using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    [System.Serializable]
    [CreateAssetMenu (fileName = "ControllerHeartUpgrade" , menuName = "CompCustomizer/CompHeartUpgrade")]
    public class ControllerHeartUpgrade : ScriptableObject
    {
        [SerializeField] private GameObject _controllerHeartUpgraePrefab;
        public GameObject ControllerHeartUpgradePrefab => _controllerHeartUpgraePrefab;
        [SerializeField] private Sprite _image;
        public Sprite Image => _image;
        [SerializeField] private UpgradeTypeClass _upgradeTypeClass;
        public UpgradeTypeClass UpgradeTypeClass => _upgradeTypeClass;
        [SerializeField] private int _heartCount;
        public int HeartCount => _heartCount;
        [SerializeField] private float _cost;
        public float Cost => _cost;
        [SerializeField] private string _heartName;
        public string HeartName => _heartName;

    }
}
