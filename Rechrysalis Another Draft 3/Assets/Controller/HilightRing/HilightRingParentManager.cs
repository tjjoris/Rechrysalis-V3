using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Controller
{
    public class HilightRingParentManager : MonoBehaviour
    {
        [SerializeField] private List<HilightRingUnitManager> _hilightRingUnitManagers = new List<HilightRingUnitManager>();
        [SerializeField] private List<HilightRingUnitManager> _hilightRingChryslisManagers = new List<HilightRingUnitManager>();
        [SerializeField] private GameObject _hilightRingUnitPrefab;
        private HilightRingUnitManager _lastCreatedHilightRingUnitManager;
        

        public void CreateHilightRingUnit(Sprite sprite)
        {
            InstantiateHilightRingUnit(sprite);
            if (_lastCreatedHilightRingUnitManager != null)
            {         
                _hilightRingUnitManagers.Add(_lastCreatedHilightRingUnitManager);
            }
        }
        public void CreateHilightRingChrysalis(Sprite sprite)
        {
            InstantiateHilightRingUnit(sprite);
            if (_lastCreatedHilightRingUnitManager != null)
            {
                _hilightRingChryslisManagers.Add(_lastCreatedHilightRingUnitManager);
            }
        }
        private void InstantiateHilightRingUnit(Sprite sprite)
        {
            GameObject go = Instantiate(_hilightRingUnitPrefab, transform);
            _lastCreatedHilightRingUnitManager = go.GetComponent<HilightRingUnitManager>();
            _lastCreatedHilightRingUnitManager.SetSprite(sprite);
        }
        public void ActivateUnit(int index)
        {
            for (int i=0; i< _hilightRingUnitManagers.Count; i++)
            {
                if (_hilightRingUnitManagers[i] != null)
                {
                    if (i== index)
                    {
                        _hilightRingUnitManagers[i].gameObject.SetActive(true);
                    }
                    else {
                        _hilightRingUnitManagers[i].gameObject.SetActive(false);
                    }                
                }
            }
            for (int i=0; i< _hilightRingChryslisManagers.Count; i++)
            {
                if (_hilightRingChryslisManagers[i] != null)
                {
                    _hilightRingChryslisManagers[i].gameObject.SetActive(false);
                }
            }
        }
        public void ActivateChrysalis(int index)
        {
            for (int i=0; i< _hilightRingChryslisManagers.Count; i++)
            {
                if (_hilightRingChryslisManagers[i] != null)
                {
                    if (i == index)
                    {
                        _hilightRingChryslisManagers[i].gameObject.SetActive(true);
                    }
                    else 
                    {
                        _hilightRingChryslisManagers[i].gameObject.SetActive(false);
                    }
                }
            }
            for (int i=0; i< _hilightRingUnitManagers.Count; i++)
            {
                if (_hilightRingUnitManagers[i] != null)
                {
                    _hilightRingUnitManagers[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
