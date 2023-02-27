using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Controller;

namespace Rechrysalis.Unit
{
    public class AIFlawedUpdate : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        [SerializeField] private float _flawedBaseValue = 1.5f;
        [SerializeField] private float _flawedRandomRange = 2;
        [SerializeField] private float _currentFlawedMax = 7;
        [SerializeField] private float _curentFlawedCurrent;
        
        private bool _debugBool = true;
        private ParentFreeEnemyManager _parentFreeEnemyManager;
        private void Awake()
        {
            _controllerManager = GetComponent<ControllerManager>();
            _parentFreeEnemyManager = GetComponent<ParentFreeEnemyManager>();
            SetFlawedMax();
        }
        public void SetFlawedMax()
        {
            float _randomValue = Random.Range(0, _flawedRandomRange);
            _currentFlawedMax = _flawedBaseValue + _randomValue;
        }
        public void Tick(float _timeAmount)
        {
            if (_curentFlawedCurrent < _currentFlawedMax)
            {
                _curentFlawedCurrent += _timeAmount;
            }
            else 
            {
                _curentFlawedCurrent = 0;
                SetFlawedMax();
                CallFlawedUpdate();
            }
        }
        public void CallFlawedUpdate()
        {
            if (_debugBool)
            {
                Debug.Log($"flawed update " );
                _controllerManager.AIFlawedUpdateActivated();
            }
        }
    }
}
