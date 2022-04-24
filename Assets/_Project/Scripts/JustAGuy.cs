using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustAGuy : MonoBehaviour
{
    [SerializeField] private GameObject _mindCloudImage;
    [SerializeField] private Image _liquidWithColor;
    [SerializeField] private GameObject _currentSkin;
    [SerializeField] private List<GameObject> _skinList; 

    public void Init(Color color)
    {
        _liquidWithColor.color = color;
        _mindCloudImage.SetActive(false);
        SetRandomSkin();
    }

    private void Start()
    {
        StartCoroutine(Enter());
    }

    private IEnumerator Enter()
    {
        yield return new WaitForSeconds(1f);
        _mindCloudImage.SetActive(true);
    }

    private void SetRandomSkin()
    {
        _currentSkin.SetActive(false);

        int randomSkin = Random.Range(0, _skinList.Count);
        _skinList[randomSkin].SetActive(true);
    }
}