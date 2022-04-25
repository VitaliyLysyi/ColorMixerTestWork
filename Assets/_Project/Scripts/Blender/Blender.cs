using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blender : MonoBehaviour, IPointerClickHandler
{
    [Space(5)]
    [Header("Requiered Components")]
    [SerializeField] private MeshRenderer _consistenceRenderer;
    [SerializeField] private GameObject _blenderLidGO;
    [SerializeField] private Transform _openLidTransform;
    [SerializeField] private Transform _closedLidTransform;
    [SerializeField] private Transform _collectionPoint;
    private List<GameObject> _collectedGameObjects;
    private Sequence _openLidSequence;
    private Sequence _closeLidSequence;
    private Sequence _shakeSequence;
    public Action _onBlenderClick;

    private void Start()
    {
        _collectedGameObjects = new List<GameObject>();
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
        _openLidSequence.Kill(false);
        _closeLidSequence.Kill(false);

        Vector3 openLidPosition = _openLidTransform.localPosition;
        Vector3 openLidRotation = _openLidTransform.localRotation.eulerAngles;
        _openLidSequence = DOTween.Sequence()
                .Append(_blenderLidGO.transform.DOLocalMove(openLidPosition, 1f))
                .Join(_blenderLidGO.transform.DOLocalRotate(openLidRotation, 1f))
                .AppendCallback(() => CloseLid());
    }

    private void CloseLid()
    {
        _closeLidSequence.Kill(true);

        Vector3 closedLidPosition = _closedLidTransform.localPosition;
        Vector3 closedLidRotation = _closedLidTransform.localRotation.eulerAngles;
        _closeLidSequence = DOTween.Sequence()
            .Append(_blenderLidGO.transform.DOLocalMove(closedLidPosition, 1f))
            .Join(_blenderLidGO.transform.DOLocalRotate(closedLidRotation, 1f));
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

    public List<GameObject> GetCollected() => _collectedGameObjects;

    public Vector3 GetCollectionPosition() => _collectionPoint.position;
}