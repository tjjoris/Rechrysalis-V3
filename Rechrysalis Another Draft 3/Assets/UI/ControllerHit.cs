using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.UI
{
    public class ControllerHit : MonoBehaviour
    {
        [SerializeField] private GameObject _controllerHitGO;
        [SerializeField] private MainManager _mainManager;
        [SerializeField] private PauseScript _pauseScript;
        
        public void ControllerIsHit()
        {
            _controllerHitGO.SetActive(true);
            _pauseScript.SetTimeFrozen(true);
            StartCoroutine(ControllerHitTimer());
        }
        IEnumerator ControllerHitTimer()
        {
            yield return new WaitForSeconds(1.15f);
            _controllerHitGO.SetActive(false);
            _pauseScript.SetTimeFrozen(false);
        }
    }
}
