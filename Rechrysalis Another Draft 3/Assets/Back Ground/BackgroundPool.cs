using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Background
{
    public class BackgroundPool : MonoBehaviour
    {
        private List<GameObject> pooledObjects;
        [SerializeField] private GameObject objectToPool;
        private List<GameObject> _activeObjects;
        // private GameObject _projectilesHolderGO;
        // private ProjectilesHolder _projectilesHolderScript;
        private int amountToPool;
        private int _xCount = 4;
        private int _yCount = 8;
        private float _tileWidth = 100;
        private float _tileHeight = 100;

        public void CreatePool(int amountToPool)
        {
            _activeObjects = new List<GameObject>();
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
        public void Tick ()
        {
            
            for (int _xIndex = 0; _xIndex < _xCount; _xIndex ++)
            {
                for (int _yIndex = 0; _yIndex < _yCount; _yIndex ++)
                {
                    bool _objectExists = false;
                    float _xCameraCount = Camera.main.transform.position.x / _tileWidth;
                    _xCameraCount = Mathf.Floor(_xCameraCount);
                    float _xIndexToCheck = _xCameraCount + _xIndex;
                    float _yCameraCount = Camera.main.transform.position.y / _tileHeight;
                    _yCameraCount = Mathf.Floor(_yCameraCount);
                    float _yIndexToCheck = _yCameraCount + _yIndex;
                    Vector2 _vectorToCheck = new Vector2((_xIndex * _tileWidth), (_yIndex * _tileHeight));
                    if (_activeObjects.Count > 0)
                    {
                        for (int _activeIndex = 0; _activeIndex < _activeObjects.Count; _activeIndex ++)
                        {
                            _objectExists = CheckIfInRange(_activeObjects[_activeIndex].transform.position, _vectorToCheck);
                        }
                        if (!_objectExists)
                        {
                            ActivateTile(_vectorToCheck);
                        }
                    }
                }
            }
        }
        private void ActivateTile (Vector2 _location)
        {
            GameObject go = GetPooledObject();
            go.SetActive(true);
            _activeObjects.Add(go);
            go.transform.position = _location;
        }
        private bool CheckIfInRange(Vector2 _objectVector,Vector2 _vectorToCheck)
        {
            if (_objectVector == _vectorToCheck)
            {
                return true;
            }
            else return false;
        }
    }
}
