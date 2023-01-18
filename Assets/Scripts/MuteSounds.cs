using UnityEngine;
using UnityEngine.UI;

public class MuteSounds : MonoBehaviour
{
    [SerializeField] Image soundOn;
    [SerializeField] Image soundOff;

    bool isMuted;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }

        UpdateButtonIcon();
        AudioListener.pause = isMuted;
    }

    public void OnButtonPressed()
    {
        if (!isMuted)
        {
            isMuted = true;
            AudioListener.pause = true;
        }
        else
        {
            isMuted = false;
            AudioListener.pause = false;
        }

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (!isMuted)
        {
            soundOn.enabled = true;
            //  soundOff.enabled = false;
        }
        else
        {
            soundOn.enabled = false;
            //  soundOff.enabled = true;
        }
    }

    private void Load()
    {
        isMuted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", isMuted ? 1 : 0);
    }

    public void MoreGames()
    {
        Application.OpenURL("market://search?q=pub:Virtual Illusions");
    }
}
