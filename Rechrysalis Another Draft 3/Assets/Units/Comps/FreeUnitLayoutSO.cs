using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="FreeUnitLayout", menuName ="Unit/FreeUnitLayout")]
    public class FreeUnitLayoutSO : ScriptableObject
    {
        [SerializeField] private int _enemiesPerRow;
        [SerializeField] private int _numberOfRows;
        [SerializeField] private Vector2[] _row0;
        [SerializeField] private Vector2[] _row1;
        [SerializeField] private Vector2[] _row2;
        [SerializeField] private Vector2[,] _unitPos;
        public Vector2[,] UnitPos
         {
            get
            {
                if (_unitPos == null) {                    
                _unitPos = new Vector2[_numberOfRows,_enemiesPerRow];
                SetEnemeisInRow(_row0, 0);
                SetEnemeisInRow(_row1, 1);
                SetEnemeisInRow(_row2, 2);
                Debug.Log("set up 2d array "+_unitPos[0,0].ToString());
            }
                return _unitPos;   
            }     
        }    

            private void SetEnemeisInRow(Vector2[] _row, int _rowNumber)
            {
                for (int i=0; i<_enemiesPerRow; i++)
                {
                    _unitPos[_rowNumber, i] = _row[i];
                }
            }
    }
}
