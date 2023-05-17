using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.HatchEffect
{
    public class DisplayUnitHEIcon : MonoBehaviour
    {
        private bool _debugBool = true;
        [SerializeField] private GameObject _damageIconPrefab;
        [SerializeField] private GameObject _defenceIconPrefab;
        [SerializeField] private GameObject _rangeIconPrefab;
        [SerializeField] private GameObject _speedIconPrefab;
        [SerializeField] private GameObject _buildSpeedIconPrefab;
        [SerializeField] private GameObject _burstHealIconPrefab;
        [SerializeField] private GameObject _aoeIconPrefab;
        [SerializeField] private float _yDisplacement;
        [SerializeField] private float _xDisplacement;
        [SerializeField] private float _xStartDisplacement;
        private Vector3 _iconPos;
        private List<SpriteRenderer> _iconSprites;
        [SerializeField] private Color _inactiveColour;
        [SerializeField] private Color _activeColour;

        private void Awake()
        {
            _iconSprites = new List<SpriteRenderer>();
        }
        public void DisplayForHEGOList(List<GameObject> hatchEffectGOs)
        {
            if (_debugBool) Debug.Log($"hatchEffectGOs count " + hatchEffectGOs.Count);
            foreach (GameObject he in hatchEffectGOs)
            {
                if (he == null) continue;
                if (_debugBool) Debug.Log($"he != null");
                HatchEffectManager heManager = he.GetComponent<HatchEffectManager>();
                if (heManager == null) continue;
                if (_debugBool) Debug.Log($"heManager != null");
                DisplayForHE(heManager);
            }
        }
        public void DisplayForHE(HatchEffectManager hatchEffectManager)
        {
            _iconPos = (new Vector3 (_xStartDisplacement, _yDisplacement, 0));
            // _iconPos.y += _yDisplacement;
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
            InstantiateIcon(_burstHealIconPrefab);
            // Instantiate(_burstHealIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void CreateDefenceIcon(HEIncreaseDefence heIncreaseDefence)
        {
            if (heIncreaseDefence == null) return;
            InstantiateIcon(_defenceIconPrefab);
            // Instantiate(_defenceIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void CreateOffenceIcon(HEIncreaseDamage heIncreaseDamage)
        {
            if (heIncreaseDamage == null) return;
            InstantiateIcon(_damageIconPrefab);
            // Instantiate(_damageIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void CreateRangeIcon(HEIncreaseRange heIncreaseRange)
        {
            if (heIncreaseRange == null) return;
            InstantiateIcon(_rangeIconPrefab);
            // Instantiate(_rangeIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void CreateSpeedIcon(HEIncreaseSpeed heIncreaseSpeed)
        {
            if (heIncreaseSpeed == null) return;
            InstantiateIcon(_speedIconPrefab);
            // Instantiate(_speedIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void CreateBuildSpeedIcon(HEIncreaseBuildSpeed heIncreaseBuildSpeed)
        {
            if (heIncreaseBuildSpeed == null) return;
            InstantiateIcon(_buildSpeedIconPrefab);
            // Instantiate(_buildSpeedIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void CreateAoEIcon(OnHatchAOEManager onHatchAOEManager)
        {
            if (onHatchAOEManager == null) return;
            InstantiateIcon(_aoeIconPrefab);
            // Instantiate(_aoeIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private void InstantiateIcon(GameObject iconPrefab)
        {
            GameObject icon = Instantiate(iconPrefab, transform.position, Quaternion.identity, transform);
            icon.transform.localPosition = _iconPos;
            IncrementIconPos();
            SpriteRenderer spriteRenderer = icon.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;
            _iconSprites.Add(spriteRenderer);            
        }
        public void SetIconsToActive()
        {
            foreach (SpriteRenderer spriteRenderer in _iconSprites)
            {
                if (spriteRenderer == null) continue;
                spriteRenderer.color = _activeColour;
            }
        }
        public void SetIconsToInactive()
        {
            foreach (SpriteRenderer spriteRenderer in _iconSprites)
            {
                if (spriteRenderer == null) continue;
                spriteRenderer.color = _inactiveColour;
            }
        }
    }
}
