using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blender : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MeshRenderer _consistenceRenderer;
    [SerializeField] private GameObject _blenderLidGO;
    [SerializeField] private Transform _lidOpenTransform;
    [SerializeField] private Transform _ingredientsLoadPoint;
    private List<GameObject> _collectedGameObjects;
    private Color _resultColor;
    private Vector3 _lidClosedPosition;
    private Vector3 _lidOpenPosition;
    private Sequence _openLidSequence;
    private Sequence _closeLidSequence;
    private Sequence _shakeSequence;
    public Action _onBlenderClick;

    private void Start()
    {
        _collectedGameObjects = new List<GameObject>();

        _lidClosedPosition = _blenderLidGO.transform.position;
        _lidOpenPosition = _lidOpenTransform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Shake();
        _onBlenderClick?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        Shake();

        if (!_collectedGameObjects.Contains(other.gameObject))
        {
            _collectedGameObjects.Add(other.gameObject);
        }
    }

    private void Shake()
    {
        _shakeSequence.Kill(true);
        _shakeSequence = DOTween.Sequence()
            .Append(transform.DOShakeRotation(1f, 5f, 20));
    }

    public void OpenLid()
    {
        if (_openLidSequence != null)
        {
            _openLidSequence.Kill(false);
        }

        _openLidSequence = DOTween.Sequence()
                .Append(_blenderLidGO.transform.DOMove(_lidOpenPosition, 1f))
                .AppendCallback(() => CloseLid());
    }

    private void CloseLid()
    {
        if (_closeLidSequence != null)
        {
            _closeLidSequence.Kill(true);
        }

        _closeLidSequence = DOTween.Sequence()
            .Append(_blenderLidGO.transform.DOMove(_lidClosedPosition, 1f));
    }

    public void DestroyCollected()
    {
        foreach (GameObject gameObject in _collectedGameObjects)
        {
            Destroy(gameObject);
        }

        _collectedGameObjects.Clear();
    }

    public void SetConsistenceColor(Color color) => _consistenceRenderer.material.color = color;

    public Color GetResultColor() => _resultColor;

    public List<GameObject> GetCollectedObjects() => _collectedGameObjects;

    public Vector3 GetLoadPosition() => _ingredientsLoadPoint.position;
}