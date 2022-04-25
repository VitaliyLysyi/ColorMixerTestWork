using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelColorSettings", menuName = "Game Data/LevelColorSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] public List<Color> colorList;
}