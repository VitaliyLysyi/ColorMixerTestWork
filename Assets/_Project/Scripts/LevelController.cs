using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Color _targetColor;
    [SerializeField] private BlenderOperator _blenderOperator;
    [SerializeField] private Blender _blender;
    [SerializeField] private JustAGuy _justAGuy;
    [SerializeField] private List<Plate> _requieredPlates;
    private float _resultPercent;

    private void Start()
    {
        _blenderOperator.Init(_blender, _requieredPlates);
        _blenderOperator._onMixComplete += () => GetColorAndFinish(_blenderOperator.GetResultColor());

        _justAGuy.Init(_targetColor);
    }

    private void GetColorAndFinish(Color result)
    {
        _resultPercent = CompareColorsInPercent(_targetColor, result);
        Debug.Log(_resultPercent.ToString());
    }

    private float CompareColorsInPercent(Color colorA, Color colorB)
    {
        float red = Mathf.Abs(colorA.r - colorB.r);
        float green = Mathf.Abs(colorA.g - colorB.g);
        float blue = Mathf.Abs(colorA.b - colorB.b);

        //D = (sqrt(R^2 + G^2) + sqrt(G^2 + B^2) + sqrt(B^2 + R^2)) / 3
        float deltaColor = 0f;
        deltaColor += Mathf.Sqrt(red * red + green * green);
        deltaColor += Mathf.Sqrt(green * green + blue * blue);
        deltaColor += Mathf.Sqrt(blue * blue + red * red);
        deltaColor /= 3f;

        float matchingInPercent = (1f - deltaColor) * 100f;
        return matchingInPercent;
    }
}