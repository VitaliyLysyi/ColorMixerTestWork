using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plate : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Ingredient _ingredientPrefab;
    public Action _onPlateClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onPlateClick?.Invoke();
    }

    public Vector3 GetPosition() => transform.position;

    public Ingredient GetFoodPrefab() => _ingredientPrefab;
}