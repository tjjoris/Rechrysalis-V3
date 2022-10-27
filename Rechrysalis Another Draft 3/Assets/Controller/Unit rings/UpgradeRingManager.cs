using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class UpgradeRingManager : MonoBehaviour
    {
        [SerializeField] private float _currentAngle;
        [SerializeField] private GameObject _upgradeRingForUnitPrefab;
        private UpgradeRingForUnitManager[] _upgradeRingForUnit;
        public float CurrentAngle {get{return _currentAngle;}}

        public void Initialize (float _currentAngle, CompSO _compSO, float _ringDistFromCentre)
        {
            _upgradeRingForUnit = new UpgradeRingForUnitManager[_compSO.ParentUnitCount];            
            this._currentAngle = _currentAngle;
            for (int _parentIndex = 0; _parentIndex < _compSO.ParentUnitCount; _parentIndex ++)
            {
                GameObject go = Instantiate (_upgradeRingForUnitPrefab, transform);
                _upgradeRingForUnit[_parentIndex] =  go.GetComponent<UpgradeRingForUnitManager>();
                Sprite[] _upgradeIcons = new Sprite[_compSO.UpgradeCountArray[_parentIndex]];
                for (int _childIndex = 0; _childIndex < _compSO.UpgradeCountArray[_parentIndex]; _childIndex ++)
                {
                    _upgradeIcons[_childIndex] = _compSO.UnitSOArray[(_parentIndex * 3) + _childIndex].UnitSprite;
                }
                _upgradeRingForUnit[_parentIndex]?.Initialize(_upgradeIcons, _ringDistFromCentre, _parentIndex);
            }
        }
        public void SetCurrentAngle (float  _currentAngle)
        {
            this._currentAngle = _currentAngle;
            Vector3 _rotationAngle = new Vector3(0, 0, _currentAngle + 90);
            transform.eulerAngles = _rotationAngle;
        }
        public void SetActiveUpgradeRing(int _parentUnit)
        {
            for (int _ringIndex = 0; _ringIndex < 3; _ringIndex ++) 
            {
                if (_ringIndex == _parentUnit)
                {
                    _upgradeRingForUnit[_ringIndex].gameObject.SetActive(true);
                }
                else {
                    _upgradeRingForUnit[_ringIndex].gameObject.SetActive(false);
                }
            }
        }
    }
}
