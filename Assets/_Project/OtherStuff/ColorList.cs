using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color List", menuName = "Game Data/Color List")]
public class ColorList : ScriptableObject
{
    [SerializeField] public List<Color> colorList;
}
