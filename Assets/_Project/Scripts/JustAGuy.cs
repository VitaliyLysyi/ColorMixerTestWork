using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class JustAGuy : MonoBehaviour
{
    [SerializeField] private GameObject _mindCloudImage;
    [SerializeField] private Image _liquidImage;
    [SerializeField] private GameObject _currentSkin;
    [SerializeField] private List<GameObject> _skinList;
    [SerializeField] private Transform _startPoint;

    public void Init(Color color)
    {
        _liquidImage.color = color;
        HideCloud();
        SetRandomSkin();
    }

    private void Start()
    {
        StartCoroutine(SceneEnter());
    }

    private IEnumerator SceneEnter()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(_startPoint.position, 0.5f));
        yield return new WaitForSeconds(1f);
        ShowCloud();
    }

    private void SetRandomSkin()
    {
        _currentSkin.SetActive(false);

        int randomSkin = Random.Range(0, _skinList.Count);
        _currentSkin = _skinList[randomSkin];
        _currentSkin.SetActive(true);
    }

    public void ShowCloud() => _mindCloudImage.SetActive(true);

    public void HideCloud() => _mindCloudImage.SetActive(false);
}