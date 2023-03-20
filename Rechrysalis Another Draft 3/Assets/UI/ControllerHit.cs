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
            // _mainManager.TimeStopped = true;
            _pauseScript.SetTimeFrozen(true);
            // Time.timeScale = 0;
            StartCoroutine(ControllerHitTimer());
        }
        IEnumerator ControllerHitTimer()
        {
            yield return new WaitForSeconds(0.15f);
            _controllerHitGO.SetActive(false);
            // _mainManager.TimeStopped = false;
            _pauseScript.SetTimeFrozen(false);
            // Time.timeScale = 1;
        }
    }
}
