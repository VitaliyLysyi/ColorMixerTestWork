using UnityEngine;

public class AlwaysLookAt : MonoBehaviour
{
    [SerializeField] private Transform _lookAtThisTransform;
    private bool _warning = false;

    private void Update()
    {
        if (_lookAtThisTransform != null)
        {
            transform.LookAt(_lookAtThisTransform.position);
        }
        else if (!_warning)
        {
            _warning = true;
            Debug.LogWarning("_lookAtThisTransform = null");
        }
    }
}