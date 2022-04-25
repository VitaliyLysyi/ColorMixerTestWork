using DG.Tweening;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Color _color;

    public void JumpToPosition(Vector3 position)
    {
        _rigidbody.isKinematic = true;

        DOTween.Sequence()
            .Append(transform.DOJump(position, 0.5f, 1, 1f).SetEase(Ease.InSine))
            .Join(transform.DORotate(Vector3.up * 180, 1f))
            .AppendCallback(() => _rigidbody.isKinematic = false);
    }

    public Color GetColor() => _color;
}