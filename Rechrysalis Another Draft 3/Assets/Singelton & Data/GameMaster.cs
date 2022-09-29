using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class GameMaster
    {
        private static GameMasterSingleton _instance;
        public static GameMasterSingleton Instance { get { return GetSingleton(); } }

        public static GameMasterSingleton GetSingleton()
        {
            //Debug.Log("Called Get Singleton.");
            if (_instance == null)
            {
                Debug.Log("Created singleton");
                GameObject go = new GameObject();
                go.name = "GameMaster";
                _instance = go.AddComponent<GameMasterSingleton>();
            }
            return _instance;
        }
    }
}
