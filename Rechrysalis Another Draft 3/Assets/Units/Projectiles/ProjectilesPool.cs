using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class ProjectilesPool : MonoBehaviour
    {
        private List<GameObject> _pooledObjects;
        [SerializeField] private GameObject objectToPool;
        private GameObject _projectilesHolderGO;
        private ProjectilesHolder _projectilesHolderScript;
        private int amountToPool;
        public System.Action<float> _projectileDealsDamage;

        public void CreatePool(int amountToPool, float projectileSpeed, Sprite projectileSprite)
        {
            this.amountToPool = amountToPool;
            _pooledObjects = new List<GameObject>();
            GameObject tmp;
            _projectilesHolderGO = GameMaster.Instance.ReferenceManager.ProjectilesHolder;
            _projectilesHolderScript = _projectilesHolderGO.GetComponent<ProjectilesHolder>();
            for (int i = 0; i < this.amountToPool; i++)
            {                
                tmp = Instantiate(objectToPool, _projectilesHolderGO.transform);                
                ProjectileHandler ph = tmp.GetComponent<ProjectileHandler>();
                _projectilesHolderScript.ProjectileHandlers.Add(ph);
                ph.Initialize(projectileSprite, projectileSpeed);
                // tmp.GetComponent<SpriteRenderer>().sprite = _projectileSprite;
                tmp.SetActive(false);
                // tmp.transform.SetParent(GameMaster.Instance.ReferenceManager.ProjectilesHolder.transform);
                // ph.ProjectileDuration = _projectileDuration;
                // ph.Damage = _damage;
                ph.ParentUnit = gameObject;
                _pooledObjects.Add(tmp);
            }
            SubscribeToProjectiles();
        }
        private void SubscribeToProjectiles()
        {
            if ((_pooledObjects != null) && (_pooledObjects.Count > 0))
            {
                for (int _index =0; _index < _pooledObjects.Count; _index ++)
                {
                    _pooledObjects[_index].GetComponent<ProjectileHandler>()._parentUnitDealsDamage -= ProjectilePoolDealsDamage;
                    _pooledObjects[_index].GetComponent<ProjectileHandler>()._parentUnitDealsDamage += ProjectilePoolDealsDamage;
                }
            }
        }
        private void UnsubscribeToProjectiles()
        {
            if ((_pooledObjects != null) && (_pooledObjects.Count > 0))
            {
                for (int _index = 0; _index < _pooledObjects.Count; _index++)
                {
                    if (_pooledObjects[_index] != null)
                    {
                        _pooledObjects[_index].GetComponent<ProjectileHandler>()._parentUnitDealsDamage -= ProjectilePoolDealsDamage;
                    }
                }
            }
        }
        private void OnEnable()
        {
            SubscribeToProjectiles();
        }
        private void OnDisable()
        {
            UnsubscribeToProjectiles();
        }
        private void ProjectilePoolDealsDamage(float _damage)
        {
            _projectileDealsDamage?.Invoke(_damage);
        }
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }
            return null;
        }

        public void TickProjectiles(float _timeAmount)
        {
            foreach (GameObject _projectile in _pooledObjects)
            {
                if (_projectile.activeInHierarchy)
                {
                    _projectile.GetComponent<ProjectileHandler>().Tick(_timeAmount);
                }
            }
        }
        // public void Tick(float _timeAmount)
        // {
        //     foreach (ProjectileHandler _projectileHandler in _projectilehandlers)
        //     {
        //         if ((_projectileHandler != null) && (_projectileHandler.gameObject.activeInHierarchy))
        //         {
        //             _projectileHandler.Tick(_timeAmount);
        //         }
        //     }
        // }
    }
}
