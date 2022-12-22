using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.CompCustomizer
{
    public class ShowCompErrorText : MonoBehaviour
    {
        [SerializeField] private GameObject _needOneBasic;
        [SerializeField] private GameObject _upgradesNeedBasic;
        [SerializeField] private GameObject _canOnlyHaveOneHE;
        [SerializeField] private GameObject _canOnlyHaveOneBasic;

        public void DisableAll()
        {
            _needOneBasic.SetActive(false);
            _upgradesNeedBasic.SetActive(false);
            _canOnlyHaveOneHE.SetActive(false);
            _canOnlyHaveOneBasic.SetActive(false);
        }
        public void NeedOneBasic()
        {
            DisableAll();
            _needOneBasic.SetActive(true);
        }
        public void UpgradesNeedBasic()
        {
            DisableAll();
            _upgradesNeedBasic.SetActive(true);
        }
        public void CanOnlyHaveOneHE()
        {
            DisableAll();
            _canOnlyHaveOneHE.SetActive(true);
        }
        public void CanOnlyHaveOneBasic()
        {
            DisableAll();
            _canOnlyHaveOneBasic.SetActive(true);
        }
    }
}
