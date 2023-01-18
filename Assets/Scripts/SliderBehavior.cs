using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    public Slider slider;
    public ScrollRect scrollRect;

    public void ChangeScrollPos()
    {
        slider.value = scrollRect.horizontalNormalizedPosition;
    }

    public void ChangeSliderPos()
    {
        scrollRect.horizontalNormalizedPosition = slider.value;
    }
}
