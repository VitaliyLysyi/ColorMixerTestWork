using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mixer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MeshRenderer _rendererTemp; //-------------
    //[SerializeField] private GameObject _capGO;
    [SerializeField] private Transform _ingredientsLoadPoint;
    public Action _onMixComplete;
    private Vector3 _defaultPosition;
    private List<Ingredient> _collectedFood;
    private Color _resultColor;
    private bool _capIsOpen;

    private void Start()
    {
        _defaultPosition = transform.position;
        _collectedFood = new List<Ingredient>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MixAndDestroyFood();
    }

    private void MixAndDestroyFood()
    {
        _resultColor = Color.black;
        _resultColor.a = 0;
        int foodCount = _collectedFood.Count;
        if (foodCount > 0)
        {
            foreach (Ingredient food in _collectedFood)
            {
                _resultColor += food.GetColor();
                Destroy(food.gameObject);
            }
            _collectedFood.Clear();
            _resultColor /= foodCount;
        }

        _rendererTemp.material.color = _resultColor;
        _onMixComplete?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Ingredient>(out Ingredient food))
        {
            Shake();
            _collectedFood.Add(food);
        }
    }

    public void OpenCap()
    {
        if (_capIsOpen)
        {
            return;
        }

        Debug.Log("CapOpened");
        _capIsOpen = true;
        DOTween.Sequence()
            .AppendInterval(2f)
            .AppendCallback(() => _capIsOpen = false)
            .AppendCallback(() => Debug.Log("CapClosed!"));
    }

    public void Shake()
    {
        DOTween.Sequence()
            .AppendInterval(0.2f)
            .Append(transform.DOShakePosition(1f, 0.05f, 20).SetEase(Ease.Linear))
            .Append(transform.DOMove(_defaultPosition, 0.2f));
    }

    public Color GetResultColor() => _resultColor;

    public Vector3 GetLoadPosition() => _ingredientsLoadPoint.position;
}