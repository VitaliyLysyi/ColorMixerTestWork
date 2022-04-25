using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [Space(5)]
    [Header("Level Actors")]
    [SerializeField] private LevelEndPanel _levelEndPanel;
    [SerializeField] private BlenderOperator _blenderOperator;
    [SerializeField] private Blender _blender;
    [SerializeField] private JustAGuy _justAGuy;
    [SerializeField] private List<Plate> _availablePlates;
    private Color _targetColor;
    private float _resultPercent;

    private void Start()
    {
        GameController.GameInit(_gameSettings);
        _targetColor = GameController.GetLevelColor();

        _blenderOperator.Init(_blender, _availablePlates);
        _blenderOperator._onMixComplete += () => GetColorAndFinish(_blenderOperator.GetResultColor());

        _justAGuy.Init(_targetColor);
    }

    private void GetColorAndFinish(Color resultColor)
    {
        _resultPercent = CompareColorsInPercent(_targetColor, resultColor);
        _justAGuy.HideCloud();
        _levelEndPanel.Init(resultColor, Mathf.RoundToInt(_resultPercent));
        _levelEndPanel.Show();
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