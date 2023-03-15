using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class HilightRingParentCreator : MonoBehaviour
    {
        private bool _debugBool = false;
        [SerializeField] private HilightRingParentManager _lastCreatedHilightRingParent;
        public HilightRingParentManager LastCreatedHilightRingParent => _lastCreatedHilightRingParent;

        [SerializeField] private Transform _hilightRingParentContainer;
        [SerializeField] private GameObject _hilightRingParentPrefab;

        public void Initialize(Transform hilightRingParentContainer)
        {
            _hilightRingParentContainer = hilightRingParentContainer;
        }
        public void CreateHilightRingParent(int index, int parentCount, Vector2 parentUnitOffset)
        {
            if (_debugBool)
            {
                Debug.Log($"create hilight parent");
            }
            Vector3 goPosition = parentUnitOffset;
            goPosition += _hilightRingParentContainer.position;
            GameObject go = Instantiate(_hilightRingParentPrefab, goPosition, Quaternion.identity, _hilightRingParentContainer);
            _lastCreatedHilightRingParent = go.GetComponent<HilightRingParentManager>();
        }
        public HilightRingParentManager GetLastCreatedHilightRingParentManager()
        {
            return _lastCreatedHilightRingParent;
        }
    }
}
