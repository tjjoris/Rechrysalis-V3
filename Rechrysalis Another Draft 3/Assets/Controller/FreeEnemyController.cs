using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rechrysalis.Unit;

namespace Rechrysalis.Controller
{
    public class FreeEnemyController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _unitsList;
        public List<GameObject> UnitsList {get{return _unitsList;} set{_unitsList = value;}}
        [SerializeField] private CompSO[] _unitListSOArray;
        public CompSO[] UnitListSoArray {get{return _unitListSOArray;}set{_unitListSOArray = value;}}
    }
}
