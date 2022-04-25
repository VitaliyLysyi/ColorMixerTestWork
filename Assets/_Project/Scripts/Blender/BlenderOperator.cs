using System;
using System.Collections.Generic;
using UnityEngine;

public class BlenderOperator : MonoBehaviour
{
    private Blender _blender;
    private Color _resultColor;
    private List<Plate> _plates;
    public Action _onMixComplete;

    public void Init(Blender blender, List<Plate> plates)
    {
        _blender = blender;
        _blender._onBlenderClick += () => DoMix(_blender.GetCollected());

        _plates = plates;
        foreach (var plate in _plates)
        {
            plate._onPlateClick += () => CreateAndTossIngredientToBlender(plate);
        }
    }

    private void CreateAndTossIngredientToBlender(Plate plate)
    {
        Ingredient ingredientPrefab = plate.GetFoodPrefab();
        Vector3 spawnPosition = plate.GetPosition();
        Ingredient ingredient = Instantiate(ingredientPrefab, spawnPosition, Quaternion.identity);

        Vector3 targetPosition = _blender.GetCollectionPosition();
        ingredient.JumpToPosition(targetPosition);
        _blender.OpenLid();
    }

    private void DoMix(List<GameObject> gameObjects)
    {
        _resultColor = Color.black;
        _resultColor.a = 0;

        int ingredientCount = 0;
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent<Ingredient>(out Ingredient ingredient))
            {
                ingredientCount++;
                _resultColor += ingredient.GetColor();
            }
        }

        if (ingredientCount > 0)
        {
            _resultColor /= ingredientCount;
        }

        _blender.DestroyCollected();
        _blender.SetConsistenceColor(_resultColor);
        _onMixComplete?.Invoke();
    }

    public Color GetResultColor() => _resultColor;
}