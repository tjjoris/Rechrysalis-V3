using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;
using Rechrysalis.Controller;

namespace Rechrysalis.HatchEffect
{
    public class HEAoEColliderManager : MonoBehaviour
    {
        private bool _debugBool = false;
        private ControllerManager _controllerManager;
        [SerializeField] private List<ParentHealth> _parentHealthList;

        private void Awake()
        {
            _parentHealthList = new List<ParentHealth>();
        }
        public void Initialize(ControllerManager controllerManager)
        {
            controllerManager = _controllerManager;
        }

        private 
        void OnTriggerEnter2D(Collider2D col)
        {
            if (_debugBool) Debug.Log($"collisoin enter " + col.gameObject.name);
            if (col == null) return;
            ParentHealth parentHealth = col.gameObject.GetComponent<ParentHealth>();
            if (parentHealth == null) return;
            if (parentHealth.ControllerManager.ControllerIndex != _controllerManager.ControllerIndex)
                AddUnitToListOfColliders(parentHealth);
        }
        void OnTriggerExit2D(Collider2D col)
        {
            if (col == null) return;
            ParentHealth parentHealth = col.gameObject.GetComponent<ParentHealth>();
            if (parentHealth == null) return;
            RemoveUnitFromListOfColliders(parentHealth);
        }
        private void AddUnitToListOfColliders(ParentHealth parentHealth)
        {
            if (_parentHealthList.Contains(parentHealth)) return;
            _parentHealthList.Add(parentHealth);
            if (_debugBool) Debug.Log($"add unit to list of colliders " + parentHealth.gameObject.name);
        }
        private void RemoveUnitFromListOfColliders(ParentHealth parentHealth)
        {
            if (!_parentHealthList.Contains(parentHealth)) return;
            _parentHealthList.Remove(parentHealth);
        }
        private List<ParentHealth> CopyParentHealthList()
        {
            List<ParentHealth> listCopy = new List<ParentHealth>();
            foreach (ParentHealth parentHealth in _parentHealthList)
            {
                if (parentHealth == null) continue;
                listCopy.Add(parentHealth);
            }
            return listCopy;
        }
        public void DealDamage(float damage)
        {
            foreach (ParentHealth parentHealth in CopyParentHealthList())
            {
                if (parentHealth == null) return;
                parentHealth.TakeDamage(damage);

                if (_debugBool) Debug.Log($"take aoe damage");
            }
        }
    }
}
