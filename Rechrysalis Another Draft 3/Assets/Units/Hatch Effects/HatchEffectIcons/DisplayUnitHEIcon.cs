using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class DisplayUnitHEIcon : MonoBehaviour
    {
        [SerializeField] private GameObject _damageIconPrefab;
        [SerializeField] private GameObject _defenceIconPrefab;
        [SerializeField] private GameObject _rangeIconPrefab;
        [SerializeField] private GameObject _speedIconPrefab;
        [SerializeField] private GameObject _buildSpeedIconPrefab;
        [SerializeField] private GameObject _burstHealIconPrefab;
        [SerializeField] private GameObject _aoeIconPrefab;
        [SerializeField] private float _yDisplacement;
        [SerializeField] private float _xDisplacement;
        private Vector3 _iconPos;


        public void DisplayForHEGOList(List<GameObject> hatchEffectGOs)
        {
            foreach (GameObject he in hatchEffectGOs)
            {
                if (he == null) continue;
                HatchEffectManager heManager = he.GetComponent<HatchEffectManager>();
                if (heManager == null) continue;
                DisplayForHE(heManager);
            }
        }
        public void DisplayForHE(HatchEffectManager hatchEffectManager)
        {
            _iconPos = transform.position;
            _iconPos.y += _yDisplacement;
            CreateBurstHealIcon(hatchEffectManager.BurstHealAmount);
            CreateDefenceIcon(hatchEffectManager.GetComponent<HEIncreaseDefence>());
            CreateOffenceIcon(hatchEffectManager.GetComponent<HEIncreaseDamage>());
            CreateRangeIcon(hatchEffectManager.GetComponent<HEIncreaseRange>());
            CreateSpeedIcon(hatchEffectManager.GetComponent<HEIncreaseSpeed>());
            CreateBuildSpeedIcon(hatchEffectManager.GetComponent<HEIncreaseBuildSpeed>());
            CreateAoEIcon(hatchEffectManager.GetComponent<OnHatchAOEManager>());
        }
        private void IncrementIconPos()
        {
            _iconPos.x += _xDisplacement;
        }
        private void CreateBurstHealIcon(float burstHealAmount)
        {
            if (burstHealAmount <= 0) return;
            Instantiate(_burstHealIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
        private void CreateDefenceIcon(HEIncreaseDefence heIncreaseDefence)
        {
            if (heIncreaseDefence == null) return;
            Instantiate(_defenceIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
        private void CreateOffenceIcon(HEIncreaseDamage heIncreaseDamage)
        {
            if (heIncreaseDamage == null) return;
            Instantiate(_damageIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
        private void CreateRangeIcon(HEIncreaseRange heIncreaseRange)
        {
            if (heIncreaseRange == null) return;
            Instantiate(_rangeIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
        private void CreateSpeedIcon(HEIncreaseSpeed heIncreaseSpeed)
        {
            if (heIncreaseSpeed == null) return;
            Instantiate(_speedIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
        private void CreateBuildSpeedIcon(HEIncreaseBuildSpeed heIncreaseBuildSpeed)
        {
            if (heIncreaseBuildSpeed == null) return;
            Instantiate(_speedIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
        private void CreateAoEIcon(OnHatchAOEManager onHatchAOEManager)
        {
            if (onHatchAOEManager == null) return;
            Instantiate(_aoeIconPrefab, _iconPos, Quaternion.identity, transform.parent);
            IncrementIconPos();
        }
    }
}
