using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Background
{
    public class BackgroundPool : MonoBehaviour
    {
        private List<GameObject> pooledObjects;
        [SerializeField] private GameObject objectToPool;
        private List<GameObject> _activeTiles;
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
            _activeTiles = new List<GameObject>();
            this.amountToPool = amountToPool;
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < this.amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, transform);
                tmp.SetActive(false);
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
            LoopToRemoveTiles();
            LoopColunns();
            _verticalBoundsYUpdate.UpdateVerticalBoxColliders(Camera.main.transform);
        }

        private void LoopColunns()
        {
            for (int column = 0; column < _xCount; column++)
            {
                LoopRows(column);
            }
        }

        private void LoopRows(int column)
        {
            for (int row = 0; row < _yCount; row++)
            {
                float xCameraCount = Camera.main.transform.position.x / _tileWidth;
                xCameraCount = Mathf.Floor(xCameraCount);
                float xIndexToCheck = xCameraCount + column - ((_xCount - 1) * 0.5f);
                float yCameraCount = Camera.main.transform.position.y / _tileHeight;
                yCameraCount = Mathf.Floor(yCameraCount);
                float yIndexToCheck = yCameraCount + row - ((_yCount - 1) * 0.5f);
                Vector3 vectorToCheck = new Vector3(((xIndexToCheck * _tileWidth)), (yIndexToCheck * _tileHeight), _zOffset);
                bool objectExists = LoopActiveTilesCheckTileExistsAtLocationToCheck(vectorToCheck);
                IfTileDoesNotExistActivateTile(objectExists, vectorToCheck);
            }
        }

        private bool LoopActiveTilesCheckTileExistsAtLocationToCheck(Vector3 locationToCheck)
        {
            bool objectExists = false;
            if (_activeTiles.Count == 0) return false;
            for (int _activeIndex = 0; _activeIndex < _activeTiles.Count; _activeIndex++)
            {
                if (objectExists)
                { return true; }
                objectExists = CheckTileInRangeOfVector(_activeTiles[_activeIndex].transform.position, locationToCheck);
            }

            return objectExists;
        }

        private void IfTileDoesNotExistActivateTile(bool tileExists, Vector3 location)
        {
            if (tileExists)
            {
                return;
            }
            ActivateTile(location);
        }
        private void ActivateTile (Vector3 location)
        {
            GameObject go = GetPooledObject();
            go.SetActive(true);
            _activeTiles.Add(go);
            go.transform.position = location;
        }
        private bool CheckTileInRangeOfVector(Vector2 tileVector,Vector2 vectorToCheck)
        {
            // Debug.Log($"check vector" + _objectVector + " " +  _vectorToCheck);
            if ((tileVector - vectorToCheck).magnitude < 0.1f)
            {
                return true;
            }
            else return false;
        }
        private void LoopToRemoveTiles()
        {
            if (_activeTiles.Count == 0) 
            {return;}
            for (int _activeIndex = 0; _activeIndex < _activeTiles.Count; _activeIndex++)
            {
                CheckToRemoveTile(_activeTiles[_activeIndex]);
            }
        }
        private void CheckToRemoveTile (GameObject tile)
        {
            TileOutsideXBounds(tile);
            TileOutsideYBounds(tile);
        }

        private void TileOutsideXBounds(GameObject tile)
        {
            if (!(Mathf.Abs((Camera.main.transform.position.y - tile.transform.position.y)) > ((_tileHeight * _yCount * 0.6f))))
            { return; }
            RemoveTile(tile);
        }
        private void TileOutsideYBounds(GameObject tile)
        {
            if (!(Mathf.Abs((Camera.main.transform.position.x - tile.transform.position.x)) > ((_tileWidth * _xCount * 0.6f))))
            {return;}
            RemoveTile(tile);            
        }

        private void RemoveTile (GameObject tile)
        {

            tile.SetActive(false);
            // Debug.Log($"remove tile");
            if (_activeTiles.Contains(tile))
            {
                _activeTiles.Remove(tile);
            }
        }
    }
}
