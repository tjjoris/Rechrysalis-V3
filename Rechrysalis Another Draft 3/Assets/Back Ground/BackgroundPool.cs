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
        [SerializeField] private VerticalBoundsYUpdate _verticalBoundsYUpdate;
        private int amountToPool;
        private int _xCount = 4;
        private int _yCount = 7;
        private float _tileWidth = 2.4f;
        private float _tileHeight = 2.4f;
        private float _zOffset = 7f;

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
            // Debug.Log($"background tick");
            if (_activeObjects.Count > 0)
            {
                for (int _activeIndex = 0; _activeIndex < _activeObjects.Count; _activeIndex++)
                {
                    CheckToRemoveTile(_activeObjects[_activeIndex]);
                }
            }
            for (int _xIndex = 0; _xIndex < _xCount; _xIndex ++)
            {
                for (int _yIndex = 0; _yIndex < _yCount; _yIndex ++)
                {
                    bool _objectExists = false;
                    float _xCameraCount = Camera.main.transform.position.x / _tileWidth;
                    _xCameraCount = Mathf.Floor(_xCameraCount);
                    float _xIndexToCheck = _xCameraCount + _xIndex - ((_xCount -1) * 0.5f);
                    float _yCameraCount = Camera.main.transform.position.y / _tileHeight;
                    _yCameraCount = Mathf.Floor(_yCameraCount);
                    float _yIndexToCheck = _yCameraCount + _yIndex - ((_yCount -1) * 0.5f);
                    Vector3 _vectorToCheck = new Vector3(((_xIndexToCheck * _tileWidth)), (_yIndexToCheck * _tileHeight), _zOffset);
                    // Debug.Log($"vector " + _vectorToCheck + "x Index " + _xIndex + " x camera count " + _xCameraCount);
                    if (_activeObjects.Count > 0)
                    {
                        for (int _activeIndex = 0; _activeIndex < _activeObjects.Count; _activeIndex ++)
                        {
                            if (!_objectExists)
                            {
                                _objectExists = CheckIfInRange(_activeObjects[_activeIndex].transform.position, _vectorToCheck);
                            }
                        }
                    }
                    if (!_objectExists)
                    {
                        ActivateTile(_vectorToCheck);
                    }
                }
            }
            _verticalBoundsYUpdate.UpdateVerticalBoxColliders(Camera.main.transform);
        }
        private void ActivateTile (Vector3 _location)
        {
            GameObject go = GetPooledObject();
            go.SetActive(true);
            _activeObjects.Add(go);
            go.transform.position = _location;
        }
        private bool CheckIfInRange(Vector2 _objectVector,Vector2 _vectorToCheck)
        {
            // Debug.Log($"check vector" + _objectVector + " " +  _vectorToCheck);
            if ((_objectVector - _vectorToCheck).magnitude < 0.1f)
            {
                return true;
            }
            else return false;
        }
        private void CheckToRemoveTile (GameObject _tile)
        {
            if (Mathf.Abs((Camera.main.transform.position.y - _tile.transform.position.y)) > ((_tileHeight * _yCount * 0.6f)))
            {
                RemoveTile(_tile);
            }
            if (Mathf.Abs((Camera.main.transform.position.x - _tile.transform.position.x)) > ((_tileWidth * _xCount * 0.6f)))
            {
                RemoveTile(_tile);
            }
        }
        private void RemoveTile (GameObject _tile)
        {

            _tile.SetActive(false);
            // Debug.Log($"remove tile");
            if (_activeObjects.Contains(_tile))
            {
                _activeObjects.Remove(_tile);
            }
        }
    }
}
