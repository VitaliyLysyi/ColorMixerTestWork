using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndPanel : MonoBehaviour
{
    [Space(5)]
    [Header("Buttons")]
    [SerializeField] Button _restartButton;
    [SerializeField] Button _nextLevelButton;
    [Space(5)]
    [Header("Content")]
    [SerializeField] Image _contetnImage;
    [SerializeField] TextMeshProUGUI _percentText;
    [SerializeField] Slider _progresSlider;
    [SerializeField] Image _progresSliderFill;
    [SerializeField] Image _goldMedalImage;
    [SerializeField] Image _silverMedalImage;
    private int _similarityPercent;
    private Color _resultColor;

    private const int PERCENT_TO_WIN = 90;
    private const int PERCENT_TO_PERFECT_WIN = 100;
    private const float POP_UP_SCALE = 1.2f;
    private const float POP_UP_TIME = 0.5f;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _nextLevelButton.onClick.AddListener(BeginNextLevel);
        _progresSlider.value = 0;
        _progresSlider.maxValue = 100;
    }

    public void Init(Color resultColor, int similarityPercent)
    {
        _resultColor = resultColor;
        _similarityPercent = similarityPercent;
    }

    private IEnumerator ShowContent()
    {
        DoPopUp(_contetnImage.gameObject);
        yield return new WaitForSeconds(POP_UP_TIME);
        ShowProgress();
        yield return new WaitForSeconds(POP_UP_TIME);
        ShowButtons();
        ShowMedals();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowContent());
    }

    private void ShowProgress()
    {
        _percentText.text = string.Format("{0}%", _similarityPercent);
        _progresSliderFill.color = _resultColor;

        _percentText.gameObject.SetActive(true);
        DoPopUp(_percentText.gameObject);

        DOTween.Sequence()
            .Append(_progresSlider.DOValue(_similarityPercent, POP_UP_TIME).SetEase(Ease.InExpo));
    }

    private void ShowMedals()
    {
        if (_similarityPercent >= PERCENT_TO_WIN)
        {
            if (_similarityPercent == PERCENT_TO_PERFECT_WIN)
            {
                _goldMedalImage.gameObject.SetActive(true);
                DoPopUp(_goldMedalImage.gameObject);
            }
            else
            {
                _silverMedalImage.gameObject.SetActive(true);
                DoPopUp(_silverMedalImage.gameObject);
            }
        }
    }

    private void ShowButtons()
    {
        _restartButton.gameObject.SetActive(true);
        DoPopUp(_restartButton.gameObject);

        if (_similarityPercent >= PERCENT_TO_WIN)
        {
            _nextLevelButton.gameObject.SetActive(true);
            DoPopUp(_nextLevelButton.gameObject);
        }
    }

    private void DoPopUp(GameObject gameObject, float time = POP_UP_TIME, float scale = POP_UP_SCALE)
    {
        float sizeUpTime = time * 0.6f; //60%
        float pauseTime = time * 0.1f; //10%
        float sizeDownTime = time * 0.3f; //30%

        gameObject.transform.localScale = Vector3.zero;
        Vector3 popUpScale = Vector3.one * scale;
        DOTween.Sequence()
            .Append(gameObject.transform.DOScale(popUpScale, sizeUpTime))
            .AppendInterval(pauseTime)
            .Append(gameObject.transform.DOScale(Vector3.one, sizeDownTime));
    }

    private void BeginNextLevel()
    {
        GameController.StartNextLevel();
    }

    private void RestartLevel()
    {
        GameController.RestartLevel();
    }
}