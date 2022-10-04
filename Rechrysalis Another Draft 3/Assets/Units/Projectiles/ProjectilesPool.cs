using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
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
                tmp = Instantiate(objectToPool);
                tmp.GetComponent<SpriteRenderer>().sprite = _projectileSprite;
                tmp.SetActive(false);
                tmp.transform.SetParent(GameMaster.Instance.ReferenceManager.ProjectilesHolder.transform);
                ProjectileHandler ph = tmp.GetComponent<ProjectileHandler>();
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
        // public void TickProjectiles(float _amount)
        // {
        //     foreach (GameObject projectile in pooledObjects)
        //     {
        //         if (projectile == isActiveAndEnabled)
        //         {
        //             projectile.GetComponent<ProjectileHandler>().TickProjectile(_amount);
        //         }
        //     }
        // }

    }
}
