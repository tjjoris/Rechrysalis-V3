using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class UpgradeRingForUnitManager : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradeIconPrefab;
        private UpgradeIconManager[] _upgradeIconManager;

        public void Initialize (Sprite[] _upgradeIcons, float _ringDistFromCentre, int _parentIndex, GameObject[] _childUnits)
        {
            _upgradeIconManager = new UpgradeIconManager[_upgradeIcons.Length];
            for (int _iconIndex = 0; _iconIndex < _upgradeIcons.Length; _iconIndex ++)
            {
                if (_childUnits[_iconIndex] != null)
                {
                    float _radToOffset = Mathf.Deg2Rad * (((360f / _upgradeIcons.Length) * _iconIndex));
                    Vector3 _unitOffset = new Vector3(Mathf.Cos(_radToOffset) * _ringDistFromCentre, Mathf.Sin(_radToOffset) * _ringDistFromCentre, -0.1f);
                    GameObject go = Instantiate (_upgradeIconPrefab, (transform.position  + _unitOffset), Quaternion.identity, transform);
                    _upgradeIconManager[_iconIndex] = go.GetComponent<UpgradeIconManager>();
                    _upgradeIconManager[_iconIndex].Initialize(_upgradeIcons[_iconIndex]);
                }
            }
            Vector3 _rotation = new Vector3(0, 0, 120 * _parentIndex);
            transform.localEulerAngles = _rotation;
        }
        public void MouseOverForUnit(int _unitIndex)
        {
            for (int _iconIndex = 0; _iconIndex < 3; _iconIndex ++)
            {
                if (_iconIndex == _unitIndex)
                {
                    _upgradeIconManager[_iconIndex].MouseOverThisUpgrade();
                }
                else 
                {
                    _upgradeIconManager[_iconIndex].MouseOffThisUpgrade();
                }
            }
        }
    }
}