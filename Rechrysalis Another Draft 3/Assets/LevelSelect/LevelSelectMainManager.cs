using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis;

namespace Rechrysalis.LevelSelect
{
    public class LevelSelectMainManager : MonoBehaviour
    {
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;
        [SerializeField] private GameObject _levelButtonPrefab;
        [SerializeField] private Transform _levelButtonContainer;

        private void Start()
        {
            if (_compsAndUnitsSO.Levels.Length > 0)
            {
                for (int i=0; i< _compsAndUnitsSO.Levels.Length; i++)
                {
                    CreateLevelButton(i);
                }
            }
        }
        private void CreateLevelButton(int level)
        {
            GameObject go = Instantiate (_levelButtonPrefab, _levelButtonContainer);
            go.GetComponent<LevelButtonScript>().SetLevelForThisButton(level);
        }
    }
}
