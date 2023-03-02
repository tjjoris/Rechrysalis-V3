using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rechrysalis.CompCustomizer
{
    public class ContinueReturnsToStart : MonoBehaviour
    {
        public void ContinueClicked()
        {
            SceneManager.LoadScene("Start");
        }
    }
}
