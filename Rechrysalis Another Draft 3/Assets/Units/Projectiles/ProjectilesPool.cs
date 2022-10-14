using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class ProjectilesPool : MonoBehaviour
    {
        private List<GameObject> pooledObjects;
        [SerializeField] private GameObject objectToPool;
        private int amountToPool;

        public void CreatePool(int amountToPool, float _projectileSpeed, Sprite _projectileSprite)
        {
            this.amountToPool = amountToPool;
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < this.amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, GameMaster.Instance.ReferenceManager.ProjectilesHolder.transform);
                ProjectileHandler ph = tmp.GetComponent<ProjectileHandler>();
                ph.Initialize(_projectileSprite);
                // tmp.GetComponent<SpriteRenderer>().sprite = _projectileSprite;
                tmp.SetActive(false);
                // tmp.transform.SetParent(GameMaster.Instance.ReferenceManager.ProjectilesHolder.transform);
                // ph.ProjectileDuration = _projectileDuration;
                // ph.Damage = _damage;
                ph.ParentUnit = gameObject;
                pooledObjects.Add(tmp);
            }
        }
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }

        public void TickProjectiles(float _timeAmount)
        {
            foreach (GameObject _projectile in pooledObjects)
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
