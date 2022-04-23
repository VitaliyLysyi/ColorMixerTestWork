using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    [SerializeField] private Mixer _mixer;
    [SerializeField] private List<Plate> _plates;
    [SerializeField] private Color _targetColor;

    private void Start()
    {
        _mixer._onMixComplete += () => OnMixComplete(_mixer.GetResultColor()); // --------------

        foreach (var plate in _plates)
        {
            plate._onPlateClick += () => LoadFoodToMixer(plate);
        }
    }

    private void LoadFoodToMixer(Plate plate)
    {
        Ingredient foodPrefab = plate.GetFoodPrefab();
        Vector3 spawnPosition = plate.GetPosition();
        Vector3 targetPosition = _mixer.GetLoadPosition();

        Ingredient food = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        food.JumpToPosition(targetPosition);

        _mixer.OpenCap();
    }

    private void OnMixComplete(Color resultColor) //--------------
    {
        Debug.Log("Operator: result: " + resultColor);
        Debug.Log("Operator: " + (_targetColor - resultColor));
        float temp = CompareColorsInPercent(_targetColor, resultColor);
        Debug.Log("Operator: comparation %: " + temp);
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
