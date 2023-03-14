using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.UI
{
    public class ControllerHit : MonoBehaviour
    {
        [SerializeField] private GameObject _controllerHitGO;
        [SerializeField] private MainManager _mainManager;
        
        public void ControllerIsHit()
        {
            _controllerHitGO.SetActive(true);
            _mainManager.Paused = true;
            // Time.timeScale = 0;
            StartCoroutine(ControllerHitTimer());
        }
        IEnumerator ControllerHitTimer()
        {
            yield return new WaitForSeconds(0.15f);
            _controllerHitGO.SetActive(false);
            _mainManager.Paused = false;
            // Time.timeScale = 1;
        }
    }
}
