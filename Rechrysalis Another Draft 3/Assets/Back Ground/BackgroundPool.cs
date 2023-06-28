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
            LoopRows();
            _verticalBoundsYUpdate.UpdateVerticalBoxColliders(Camera.main.transform);
        }

        private void LoopRows()
        {
            for (int _xIndex = 0; _xIndex < _xCount; _xIndex++)
            {
                LoopColumns(_xIndex);
            }
        }

        private void LoopColumns(int _xIndex)
        {
            for (int _yIndex = 0; _yIndex < _yCount; _yIndex++)
            {
                bool _objectExists = false;
                float _xCameraCount = Camera.main.transform.position.x / _tileWidth;
                _xCameraCount = Mathf.Floor(_xCameraCount);
                float _xIndexToCheck = _xCameraCount + _xIndex - ((_xCount - 1) * 0.5f);
                float _yCameraCount = Camera.main.transform.position.y / _tileHeight;
                _yCameraCount = Mathf.Floor(_yCameraCount);
                float _yIndexToCheck = _yCameraCount + _yIndex - ((_yCount - 1) * 0.5f);
                Vector3 _vectorToCheck = new Vector3(((_xIndexToCheck * _tileWidth)), (_yIndexToCheck * _tileHeight), _zOffset);
                _objectExists = LoopActiveTilesCheckTileExistsAtTileToCheck(_objectExists, _vectorToCheck);
                IfTileDoesNotExistActivateTile(_objectExists, _vectorToCheck);
            }
        }

        private bool LoopActiveTilesCheckTileExistsAtTileToCheck(bool _objectExists, Vector3 _vectorToCheck)
        {
            if (_activeTiles.Count == 0) return false;
            for (int _activeIndex = 0; _activeIndex < _activeTiles.Count; _activeIndex++)
            {
                if (_objectExists)
                { return true; }
                _objectExists = CheckTileInRangeOfVector(_activeTiles[_activeIndex].transform.position, _vectorToCheck);
            }

            return _objectExists;
        }

        private void IfTileDoesNotExistActivateTile(bool tileExists, Vector3 location)
        {
            if (tileExists)
            {
                return;
            }
            ActivateTile(location);
        }
        private void ActivateTile (Vector3 _location)
        {
            GameObject go = GetPooledObject();
            go.SetActive(true);
            _activeTiles.Add(go);
            go.transform.position = _location;
        }
        private bool CheckTileInRangeOfVector(Vector2 _objectVector,Vector2 _vectorToCheck)
        {
            // Debug.Log($"check vector" + _objectVector + " " +  _vectorToCheck);
            if ((_objectVector - _vectorToCheck).magnitude < 0.1f)
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
        private void CheckToRemoveTile (GameObject _tile)
        {
            TileOutsideXBounds(_tile);
            TileOutsideYBounds(_tile);
        }

        private void TileOutsideXBounds(GameObject _tile)
        {
            if (!(Mathf.Abs((Camera.main.transform.position.y - _tile.transform.position.y)) > ((_tileHeight * _yCount * 0.6f))))
            { return; }
            RemoveTile(_tile);
        }
        private void TileOutsideYBounds(GameObject _tile)
        {
            if (!(Mathf.Abs((Camera.main.transform.position.x - _tile.transform.position.x)) > ((_tileWidth * _xCount * 0.6f))))
            {return;}
            RemoveTile(_tile);            
        }

        private void RemoveTile (GameObject _tile)
        {

            _tile.SetActive(false);
            // Debug.Log($"remove tile");
            if (_activeTiles.Contains(_tile))
            {
                _activeTiles.Remove(_tile);
            }
        }
    }
}
