using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rechrysalis.Controller
{
    public class ControllerHPTokens : MonoBehaviour
    {
        [SerializeField] private GameObject[] _controllerHPTokens;
        [SerializeField] private int _tokenActiveIndex = 3;
        [SerializeField] private int _tokenMax = 3;

        public void RemoveToken()
        {
            _controllerHPTokens[_tokenActiveIndex].SetActive(false);
            _tokenActiveIndex --;
        }
        public void AddTokens(int count)
        {
            for (int i = 0; i<count; i++)
            _controllerHPTokens[_tokenActiveIndex].SetActive(true);
            _tokenActiveIndex ++;
        }
    }
}
