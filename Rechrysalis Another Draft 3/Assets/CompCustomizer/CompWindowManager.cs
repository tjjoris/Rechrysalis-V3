using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.CompCustomizer
{
    public class CompWindowManager : MonoBehaviour
    {
        [SerializeField] private GameObject _unitButtonPrefab;
        private UnitButtonManager[] _arrayOfUnitButtonManagers;
        public UnitButtonManager[] ArrayOfUnitButtonManagers {get {return _arrayOfUnitButtonManagers;}}
        [SerializeField]private float _distFromCentreForBasic = 1f;
        [SerializeField]private float _distFromCentreForAdv = 2f;

        public void Initialize(CompSO _compSO, Color _basicColour, Color _advColour)
        {           
            // Vector2 _posOffset = Vector2.zero; 
            _arrayOfUnitButtonManagers = new UnitButtonManager[_compSO.ParentUnitCount * _compSO.ChildUnitCount];
            for (int _parentUnitIndex = 0; _parentUnitIndex < _compSO.ParentUnitCount; _parentUnitIndex++)
            {
                // _posOffset = AnglesMath.PosForUnitInRing(_compSO.ParentUnitCount, _parentUnitIndex, 90, _distFromCentreForBasic);                
                // Vector3 _posOffsetV3 = _posOffset;
                // Vector3 _newPosition = transform.position + _posOffsetV3;
                // GameObject go = Instantiate (_unitButtonPrefab, _newPosition, Quaternion.identity, transform);
                // _ArrayOfUnitButtonManagers[_parentUnitIndex * 3] = go.GetComponent<UnitButtonManager>();
                // if (_compSO.ChildUnitCount >= 2)
                CreateUnitButton(_compSO, _parentUnitIndex, 0, 90, _distFromCentreForBasic, _basicColour, false);
                if (_compSO.ChildUnitCount == 2)
                {
                    CreateUnitButton(_compSO, _parentUnitIndex, 1, 90, _distFromCentreForAdv, _advColour, true);
                }
                else if (_compSO.ChildUnitCount == 3)
                {
                    CreateUnitButton(_compSO, _parentUnitIndex, 1, 70, _distFromCentreForAdv, _advColour, true);
                    CreateUnitButton(_compSO, _parentUnitIndex, 2, 110, _distFromCentreForAdv, _advColour, true);
                }
            }
        }
        private void CreateUnitButton(CompSO _compSO, int _parentUnitIndex, int _childUnitIndex, float _offsetAngle, float _distFromCentre, Color _colour, bool _advUnit)
        {
            Vector2 _posOffset = AnglesMath.PosForUnitInRing(_compSO.ParentUnitCount, _parentUnitIndex, _offsetAngle, _distFromCentre);
            Vector3 _posOffsetV3 = _posOffset;
            Vector3 _newPosition = transform.position + _posOffsetV3;
            GameObject go = Instantiate(_unitButtonPrefab, _newPosition, Quaternion.identity, transform);
            int _indexInButtonManagerArray = (_parentUnitIndex * _compSO.ParentUnitCount) + _childUnitIndex;
            Debug.Log($"index " + _indexInButtonManagerArray);
            _arrayOfUnitButtonManagers[_indexInButtonManagerArray] = go.GetComponent<UnitButtonManager>();
            _arrayOfUnitButtonManagers[_indexInButtonManagerArray].Initialize(_compSO.UnitSOArray[_indexInButtonManagerArray], _indexInButtonManagerArray, _advUnit);
            _arrayOfUnitButtonManagers[_indexInButtonManagerArray].SetBackGColour(_colour);
        }
    }
}
