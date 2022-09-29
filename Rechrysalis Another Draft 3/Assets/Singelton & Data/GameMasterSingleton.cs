using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class GameMasterSingleton : MonoBehaviour
    {
        [SerializeField] private ReferenceManager _referenceManager;
        public ReferenceManager ReferenceManager
        {
            get
            {
                if (_referenceManager == null)
                {
                    _referenceManager = GameObject.FindObjectOfType<ReferenceManager>();
                    if (_referenceManager == null)
                    { Debug.LogError("GameMaster could not find scene's ReferenceManager."); }
                }
                return _referenceManager;
            }
        }
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
