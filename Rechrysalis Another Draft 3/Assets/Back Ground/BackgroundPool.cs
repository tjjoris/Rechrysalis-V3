using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Background
{
    public class BackgroundPool : MonoBehaviour
    {
        private List<GameObject> pooledObjects;
        [SerializeField] private GameObject objectToPool;
        // private GameObject _projectilesHolderGO;
        // private ProjectilesHolder _projectilesHolderScript;
        private int amountToPool;

        public void CreatePool(int amountToPool)
        {
            this.amountToPool = amountToPool;
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            // _projectilesHolderGO = GameMaster.Instance.ReferenceManager.ProjectilesHolder;
            // _projectilesHolderScript = _projectilesHolderGO.GetComponent<ProjectilesHolder>();
            for (int i = 0; i < this.amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, transform);
                // ProjectileHandler ph = tmp.GetComponent<ProjectileHandler>();
                // _projectilesHolderScript.ProjectileHandlers.Add(ph);
                // ph.Initialize(_projectileSprite);
                // tmp.GetComponent<SpriteRenderer>().sprite = _projectileSprite;
                tmp.SetActive(false);
                // tmp.transform.SetParent(GameMaster.Instance.ReferenceManager.ProjectilesHolder.transform);
                // ph.ProjectileDuration = _projectileDuration;
                // ph.Damage = _damage;
                // ph.ParentUnit = gameObject;
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
    }
}
