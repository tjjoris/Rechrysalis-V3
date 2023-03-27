using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis
{
    public class LevelSceneManagement : MonoBehaviour
    {
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;

        public void Initialize (CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
        }
        public void LevelBeat()
        {
            Debug.Log($"level beat");
        }
    }
}
