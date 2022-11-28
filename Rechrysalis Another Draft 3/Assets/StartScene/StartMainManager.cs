using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rechrysalis.Unit;

namespace Rechrysalis
{
    public class StartMainManager : MonoBehaviour
    {
        [SerializeField]private CompsAndUnitsSO _compsAndUnits;
        public void StartButtonClicked()
        {
            _compsAndUnits.Level = 0;
            SceneManager.LoadScene("CompCustomizer");
        }
    }
}
