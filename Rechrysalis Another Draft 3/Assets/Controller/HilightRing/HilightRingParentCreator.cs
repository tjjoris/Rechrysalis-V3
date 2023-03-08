using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class HilightRingParentCreator : MonoBehaviour
    {
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
            GameObject go = Instantiate(_hilightRingParentPrefab, _hilightRingParentContainer);
            _lastCreatedHilightRingParent = go.GetComponent<HilightRingParentManager>();
        }
        public HilightRingParentManager GetLastCreatedHilightRingParentManager()
        {
            return _lastCreatedHilightRingParent;
        }
    }
}
