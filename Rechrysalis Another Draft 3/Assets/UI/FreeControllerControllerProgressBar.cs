using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.UI
{
    public class FreeControllerControllerProgressBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressFill;
        [SerializeField] private float _progressMax;
        public float ProgressMax { get => _progressMax; set => _progressMax = value; }
        [SerializeField] private float _progressCurrent;
        public float ProgressCurrent => _progressCurrent;
        [SerializeField] private LevelSceneManagement _levelSceneManagement;
        public LevelSceneManagement LevelSceneManagement {set => _levelSceneManagement = value;}

        public void Initialize(float progressMax)
        {
            _progressMax = progressMax;
            _progressCurrent = 0;
            SetBarScale(0);
        }
        public void AddProgress(float amount)
        {
            _progressCurrent += amount;
            float progressPercent = _progressCurrent / _progressMax;
            SetBarScale(progressPercent);
        }
        private void SetBarScale(float percent)
        {
            _progressFill.localScale = new Vector3(percent, 1, 1);
        }
        private void CheckIfProgressFull()
        {
            if (_progressCurrent >= _progressMax)
            {
                _levelSceneManagement.LevelBeat();
            }
        }
    }
}
