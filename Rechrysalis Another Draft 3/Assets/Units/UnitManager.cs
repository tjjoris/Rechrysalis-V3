using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rechrysalis.Movement;
using Rechrysalis.Attacking;

namespace Rechrysalis.Unit
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private int _controllerIndex;
        public int ControllerIndex {get{return _controllerIndex;}}
        [SerializeField] private UnitStatsSO _unitStats;
        [SerializeField] private TMP_Text _nameText;
        public UnitStatsSO UnitStats {get{return _unitStats;}}
        private Mover _mover;
        private Attack _attack;
        [SerializeField] private bool _isStopped;
        public bool IsStopped {set{_isStopped = value;}}
        public void Initialize(int _controllerIndex, UnitStatsSO _unitStats)
        {
            this._controllerIndex = _controllerIndex;
            this._unitStats = _unitStats;
            GetComponent<ProjectilesPool>()?.CreatePool(_unitStats.AmountToPool, _unitStats.ProjectileSpeed, _unitStats.ProjectileSprite);
            _nameText.text = _unitStats.UnitName;
            _mover = GetComponent<Mover>();
            _attack = GetComponent<Attack>();
        }
        public void Tick(float _timeAmount)
        {
            _mover?.Tick(_timeAmount);
            _attack?.Tick(_timeAmount);
        }
    }
}
