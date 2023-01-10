using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.UI
{
    public class NewGameStatus : MonoBehaviour
    {
        [SerializeField] private TMP_Text _newGameStatusText;
        [SerializeField] private CompsAndUnitsSO _compsAndUnitsSO;

        public void YouLost()
        {
            _newGameStatusText.text = $"You Lost";
        }
        public void YouWon()
        {
            _newGameStatusText.text = $"Congratulations! \n You won!";
        }
        public void NewGame()
        {
            _newGameStatusText.text = $"New Game";
        }
    }
}
