using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public class OnHatchAOEManager : HatchEffectFunctionParent
    {
        private bool _debugBool = false;
        [SerializeField] private HatchEffectManager _hatchEffectManager;
        // [SerializeField] private ControllerManager _controllerManager;
        [SerializeField] private float _damage;
        [SerializeField] private float _tickRate;
        [SerializeField] private float _tickCurrent;
        [SerializeField] private List<ParentHealth> _parentHealthList;
        [SerializeField] private GameObject _heAOEPrefab;
        [SerializeField] private HEAoEColliderManager _heAoeColliderManager;

        
        private void Awake()
        {
            _hatchEffectManager = GetComponent<HatchEffectManager>();
        }
        public override void Initialize(ControllerManager controllerManager, float hatchMult)
        {
            base.Initialize(controllerManager, hatchMult);
            // _controllerManager = controllerManager;
            GameObject go = Instantiate (_heAOEPrefab, _controllerManager.transform.position, Quaternion.identity, _controllerManager.transform);
            _heAoeColliderManager = go.GetComponent<HEAoEColliderManager>();
            _heAoeColliderManager.Initialize(controllerManager);
        }
        public override void Tick(float timeAmount)
        {
            _tickCurrent += timeAmount;
            if (_tickCurrent >= _tickRate)
            {
                _tickCurrent -= _tickRate;
                DealDamage();
            }
        }
        private void DealDamage()
        {
            // foreach (ParentHealth parentHealth in CopyParentHealthList())
            // {
            //     if (parentHealth == null) return;
            //     parentHealth.TakeDamage(_damage);

            //     if (_debugBool) Debug.Log($"take aoe damage");
            // }
            _heAoeColliderManager.DealDamage(_damage);
        }
        // private List<ParentHealth> CopyParentHealthList()
        // {
        //     List<ParentHealth> listCopy = new List<ParentHealth>();
        //     foreach (ParentHealth parentHealth in _parentHealthList)
        //     {
        //         if (parentHealth == null) continue;
        //         listCopy.Add(parentHealth);
        //     }
        //     return listCopy;
        // }
        // void OnTriggerEnter2D(Collider2D col)
        // {
        //     if (_debugBool) Debug.Log($"collisoin enter " + col.gameObject.name);
        //     if (col == null) return;
        //     ParentHealth parentHealth = col.gameObject.GetComponent<ParentHealth>();
        //     if (parentHealth == null) return;
        //     if (parentHealth.ControllerManager.ControllerIndex != _controllerManager.ControllerIndex)
        //     AddUnitToListOfColliders(parentHealth);
        // }
        // void OnTriggerExit2D(Collider2D col) 
        // {
        //     if (col == null) return;
        //     ParentHealth parentHealth = col.gameObject.GetComponent<ParentHealth>();
        //     if (parentHealth == null) return;
        //     RemoveUnitFromListOfColliders(parentHealth);
        // }
    
        // private void AddUnitToListOfColliders(ParentHealth parentHealth)
        // {
        //     if (_parentHealthList.Contains(parentHealth)) return;
        //     _parentHealthList.Add(parentHealth);
        //     if (_debugBool) Debug.Log($"add unit to list of colliders " + parentHealth.gameObject.name);
        // }
        // private void RemoveUnitFromListOfColliders(ParentHealth parentHealth)
        // {
        //     if (!_parentHealthList.Contains(parentHealth)) return;
        //     _parentHealthList.Remove(parentHealth);
        // }
        public override void Die()
        {
            _heAoeColliderManager.Die();
        }
    }
}
