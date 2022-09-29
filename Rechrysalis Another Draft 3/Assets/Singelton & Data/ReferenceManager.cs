using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class ReferenceManager : MonoBehaviour
    {
        [SerializeField] private GameObject _backG;
        public GameObject BackG {get{return _backG;}}
        [SerializeField] private GameObject _projectilesHolder;
        public GameObject ProjectilesHolder {get{return _projectilesHolder;}}
    }
}
