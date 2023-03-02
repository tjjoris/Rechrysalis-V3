using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis.CompCustomizer
{
    public class ContinueStartsFreeUnitLevel : MonoBehaviour

    {
        public void ContinueClicked()
        {
            SceneManager.LoadScene("FreeEnemyLevel");
        }
    }
}
