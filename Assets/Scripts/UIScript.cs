using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    #region Singleton
    private static UIScript _instance;
    public static UIScript Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] private RectTransform levelSlider;
    [SerializeField] private RectTransform endlessModeText;

    public Button pauseButton;

    public void LevelEnd()
    {
        LeanTween.value(endlessModeText.anchoredPosition.y, endlessModeText.anchoredPosition.y - endlessModeText.rect.height*1.5f, .8f).setEaseInOutSine().setOnUpdate(value => endlessModeText.anchoredPosition = new Vector2(endlessModeText.anchoredPosition.x, value));
        LeanTween.value(levelSlider.anchoredPosition.y, levelSlider.anchoredPosition.y + levelSlider.rect.height, .8f).setEaseInOutSine().setOnUpdate(value => levelSlider.anchoredPosition = new Vector2(levelSlider.anchoredPosition.x, value));
    }
}
