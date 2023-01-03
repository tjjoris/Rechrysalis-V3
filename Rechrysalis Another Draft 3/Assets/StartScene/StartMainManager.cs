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
        public void NewGameClicked()
        {
            _compsAndUnits.Level = 0;
            _compsAndUnits.CompsSO[0].ParentUnitClassList.Clear();
            _compsAndUnits.ControllerHPTokensCurrent = _compsAndUnits.ControllerHPTokensMax;
            // _compsAndUnits.CompsSO[0].ParentUnitClassList = new List<ParentUnitClass>();
            SceneManager.LoadScene("CompCustomizer");
        }
    }
}
