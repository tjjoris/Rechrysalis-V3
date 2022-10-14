using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis.Attacking
{
    public class ProjectilesHolder : MonoBehaviour
    {
        private List<ProjectileHandler> _projectileHandlers;
        public List<ProjectileHandler> ProjectileHandlers {set {_projectileHandlers = value;} get {return _projectileHandlers;}}

        public void Initialize ()
        {
            _projectileHandlers = new List<ProjectileHandler>();
        }
        public void Tick(float _timeAmount)
        {
            foreach (ProjectileHandler _projectileHandler in _projectileHandlers)
            {
                if ((_projectileHandler != null) && (_projectileHandler.gameObject.activeInHierarchy))
                {
                    _projectileHandler.Tick(_timeAmount);
                }
            }
        }
    }
}