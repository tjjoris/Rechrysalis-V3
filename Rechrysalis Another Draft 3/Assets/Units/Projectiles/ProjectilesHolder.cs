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
            // foreach (ProjectileHandler _projectileHandler in _projectileHandlers)
            for (int _projectileIndex = 0; _projectileIndex < _projectileHandlers.Count; _projectileIndex ++)
            {
                if ((_projectileHandlers[_projectileIndex] != null) && (_projectileHandlers[_projectileIndex].gameObject.activeInHierarchy))
                {
                    _projectileHandlers[_projectileIndex].Tick(_timeAmount);
                }
            }
        }
    }
}
