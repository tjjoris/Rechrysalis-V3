using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Unit
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="TargetsList", menuName ="Unit/TargetList")]
    public class TargetsListSO : ScriptableObject
    {
        // [SerializeField] private int _listLength;
        // public int ListLength {set {_listLength = value;}}
        private bool _debugBool = true;
        [SerializeField] private List<GameObject> _targets;
        public List<GameObject> Targets {get{return _targets;}}
        public void Initialize()
        {
            _targets.Clear();
        }
        public void SetNewTarget(GameObject _newTarget)
        {   
            if (_debugBool)
            {
                Debug.Log($"target set " + _newTarget.name);
            }
            for (int _listIndex = 0; _listIndex < _targets.Count; _listIndex ++)
            {
                if (_targets[_listIndex] == _newTarget)
                {
                    _targets.Remove(_targets[_listIndex]);
                }
            }
            _targets.Add(_newTarget);
        }
    }
}
