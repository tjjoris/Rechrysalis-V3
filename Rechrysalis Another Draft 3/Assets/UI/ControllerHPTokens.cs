using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Rechrysalis.Controller
{
    public class ControllerHPTokens : MonoBehaviour
    {
        // [SerializeField] private ControllerDeathGameOver _controllerDeathGameOver;
        [SerializeField] private GameObject[] _controllerHPTokens;
        private CompsAndUnitsSO _compsAndUnitsSO;
        [SerializeField] private int _tokenActiveIndex = 3;
        [SerializeField] private int _tokenMax = 3;

        public void Initialize(CompsAndUnitsSO compsAndUnitsSO)
        {
            _compsAndUnitsSO = compsAndUnitsSO;
            _tokenActiveIndex = compsAndUnitsSO.ControllerHPTokensCurrent;
            UpdateTokenUI();
        }
        private void UpdateTokenUI()
        {
            for(int i=0; i<=_tokenMax; i++)
            {
                if ((_tokenActiveIndex >= i) && (!_controllerHPTokens[i].activeInHierarchy))
                {
                    _controllerHPTokens[i].SetActive(true);
                }
                else if ((_tokenActiveIndex < i) && (_controllerHPTokens[i].activeInHierarchy))              
                {
                    _controllerHPTokens[i].SetActive(false);
                }
            }
        }
        public void RemoveToken()
        {
            if (_tokenActiveIndex <= 0)
            {
                SceneManager.LoadScene("Start");
                // controllerDeathGameOver?.GameOver(_healthCurrent);
            }
            _controllerHPTokens[_tokenActiveIndex].SetActive(false);
            _tokenActiveIndex --;   
            SetSOTokens();         
        }
        public void AddTokens(int count)
        {
            for (int i = 0; i<count; i++)
            {
                _controllerHPTokens[_tokenActiveIndex].SetActive(true);
                _tokenActiveIndex ++;
            }
            if (_tokenActiveIndex > _tokenMax)
            {
                _tokenActiveIndex = _tokenMax;
            }
            SetSOTokens();
        }
        public bool IsMissingTokens()
        {
            if (_tokenActiveIndex < _tokenMax)
            return true;
            return false;
        }
        private void SetSOTokens()
        {
            _compsAndUnitsSO.ControllerHPTokensCurrent = _tokenActiveIndex;
        }
    }
}
