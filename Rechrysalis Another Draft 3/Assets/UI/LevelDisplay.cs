using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rechrysalis.UI
{
    public class LevelDisplay : MonoBehaviour
    {
    [SerializeField] private TMP_Text _levelText;
    public TMP_Text LevelText => _levelText;


    public void SetLevelText(int level)
    {
        _levelText.text = $"Level " + level;
    }
    }
}
