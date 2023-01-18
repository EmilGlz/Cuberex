using UnityEngine;
using UnityEngine.UI;

public class SettingsAnim : MonoBehaviour
{
    [SerializeField] Button mute;
    //[SerializeField] Button moreGames;

    //Vector3 oldPosMoregames;
    Vector3 oldPosMute;

    bool isOpen;

    private void Start()
    {
        //oldPosMoregames = moreGames.GetComponent<RectTransform>().anchoredPosition;
        oldPosMute = mute.GetComponent<RectTransform>().anchoredPosition;
    }

    public void ExpandSetting()
    {
        if (isOpen)
        {
            //LeanTween.move(moreGames.GetComponent<RectTransform>(), oldPosMoregames, 0.5f);
            //LeanTween.scale(moreGames.gameObject, Vector3.zero, 0.2f);

            LeanTween.move(mute.GetComponent<RectTransform>(), oldPosMute, 0.5f);
            LeanTween.scale(mute.gameObject, Vector3.zero, 0.2f);

            LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.5f);

            isOpen = false;

        }
        else
        {
            //LeanTween.move(moreGames.GetComponent<RectTransform>(), new Vector3(48, 135, 0f), 0.5f);
            //LeanTween.scale(moreGames.gameObject, Vector3.one, 0.2f);

            LeanTween.move(mute.GetComponent<RectTransform>(), new Vector3(48, 135, 0f), 0.5f);
            LeanTween.scale(mute.gameObject, Vector3.one * 2, 0.2f);

            LeanTween.rotate(gameObject, new Vector3(0, 0, -90), 0.6f);

            isOpen = true;
        }
    }
}
