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
        private List<HEIconChangeColour> _heIconChangeColours;
        [SerializeField] private Color _inactiveColour;
        public Color InactiveColour => _inactiveColour;
        [SerializeField] private Color _activeColour;
        public Color ActiveColour => _activeColour;

        private void Awake()
        {
            _iconSprites = new List<SpriteRenderer>();
            _heIconChangeColours = new List<HEIconChangeColour>();
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
        delegate HEIconChangeColour CreateIconDelegate(HatchEffectManager hatchEffectManager);
        // CreateIconDelegate CreateIconDeleagate;
        public List<HEIconChangeColour> DisplayForHE(HatchEffectManager hatchEffectManager)
        {
            _iconPos = (new Vector3 (_xStartDisplacement, _yDisplacement, 0));
            // List<HEIconChangeColour> heIconChangeColours = new List<HEIconChangeColour>();
            _heIconChangeColours = new List<HEIconChangeColour>();
            
            // _heIconChangeColours.Add(CreateBurstHealIcon(hatchEffectManager.BurstHealAmount));
            // _heIconChangeColours.Add(CreateDefenceIcon(hatchEffectManager.GetComponent<HEIncreaseDefence>()));
            // _heIconChangeColours.Add(CreateOffenceIcon(hatchEffectManager.GetComponent<HEIncreaseDamage>()));
            // _heIconChangeColours.Add(CreateRangeIcon(hatchEffectManager.GetComponent<HEIncreaseRange>()));
            // _heIconChangeColours.Add(CreateSpeedIcon(hatchEffectManager.GetComponent<HEIncreaseSpeed>()));
            // _heIconChangeColours.Add(CreateBuildSpeedIcon(hatchEffectManager.GetComponent<HEIncreaseBuildSpeed>()));
            // _heIconChangeColours.Add(CreateAoEIcon(hatchEffectManager.GetComponent<OnHatchAOEManager>()));
            // CreateIconDelegate createIconDelegate = CreateBurstHealIcon;
            CreateIconWithDelegate(CreateBurstHealIcon, hatchEffectManager);
            // createIconDelegate = CreateDefenceIcon;
            CreateIconWithDelegate(CreateDefenceIcon, hatchEffectManager);
            // createIconDelegate = 
            CreateIconWithDelegate(CreateOffenceIcon, hatchEffectManager);
            CreateIconWithDelegate(CreateRangeIcon, hatchEffectManager);
            CreateIconWithDelegate(CreateSpeedIcon, hatchEffectManager);
            CreateIconWithDelegate(CreateBuildSpeedIcon, hatchEffectManager);
            CreateIconWithDelegate(CreateAoEIcon, hatchEffectManager);


            return _heIconChangeColours;
        }
        public List<HEIconChangeColour> GetHEIconChangeColours (HatchEffectManager hatchEffectManager)          
        {
            return _heIconChangeColours;
        }
        private void IncrementIconPos()
        {
            _iconPos.x += _xDisplacement;
        }
        private HEIconChangeColour CreateIconWithDelegate(CreateIconDelegate createIconWithDelegate, HatchEffectManager hatchEffectManager)
        {
            HEIconChangeColour heIconChangeColour = createIconWithDelegate(hatchEffectManager);
            if (heIconChangeColour == null) return null;
            _heIconChangeColours.Add(heIconChangeColour);
            return heIconChangeColour;
        }
        private HEIconChangeColour CreateBurstHealIcon(HatchEffectManager hatchEffectManager)
        {            
            if (hatchEffectManager.BurstHealAmount <= 0) return null;
            return InstantiateIcon(_burstHealIconPrefab);
            // Instantiate(_burstHealIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour CreateDefenceIcon(HatchEffectManager hatchEffectManager)
        {
            HEIncreaseDefence heIncreaseDefence = hatchEffectManager.GetComponent<HEIncreaseDefence>();
            if (heIncreaseDefence == null) return null;
            return InstantiateIcon(_defenceIconPrefab);
            // Instantiate(_defenceIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour CreateOffenceIcon(HatchEffectManager hatchEffectManager)
        {
            HEIncreaseDamage heIncreaseDamage = hatchEffectManager.GetComponent<HEIncreaseDamage>();
            if (heIncreaseDamage == null) return null;
            return InstantiateIcon(_damageIconPrefab);
            // Instantiate(_damageIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour CreateRangeIcon(HatchEffectManager hatchEffectManager)
        {
            HEIncreaseRange heIncreaseRange = hatchEffectManager.GetComponent<HEIncreaseRange>();
            if (heIncreaseRange == null) return null;
            return InstantiateIcon(_rangeIconPrefab);
            // Instantiate(_rangeIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour CreateSpeedIcon(HatchEffectManager hatchEffectManager)
        {
            HEIncreaseSpeed heIncreaseSpeed = hatchEffectManager.GetComponent<HEIncreaseSpeed>();
            if (heIncreaseSpeed == null) return null;
            return InstantiateIcon(_speedIconPrefab);
            // Instantiate(_speedIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour CreateBuildSpeedIcon(HatchEffectManager hatchEffectManager)
        {
            HEIncreaseBuildSpeed heIncreaseBuildSpeed = hatchEffectManager.GetComponent<HEIncreaseBuildSpeed>();
            if (heIncreaseBuildSpeed == null) return null;
            return InstantiateIcon(_buildSpeedIconPrefab);
            // Instantiate(_buildSpeedIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour CreateAoEIcon(HatchEffectManager hatchEffectManager)
        {
            OnHatchAOEManager onHatchAOEManager = hatchEffectManager.GetComponent<OnHatchAOEManager>();
            if (onHatchAOEManager == null) return null;
            return InstantiateIcon(_aoeIconPrefab);
            // Instantiate(_aoeIconPrefab, _iconPos, Quaternion.identity, transform);
            // IncrementIconPos();
        }
        private HEIconChangeColour InstantiateIcon(GameObject iconPrefab)
        {
            GameObject icon = Instantiate(iconPrefab, transform.position, Quaternion.identity, transform);
            icon.transform.localPosition = _iconPos;
            IncrementIconPos();
            SpriteRenderer spriteRenderer = icon.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return null;
            _iconSprites.Add(spriteRenderer); 
            HEIconChangeColour heIconChangeColour = icon.GetComponent<HEIconChangeColour>();           
            return heIconChangeColour;
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
