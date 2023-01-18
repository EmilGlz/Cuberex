using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;

    AsyncOperation async;

    private void Start()
    {
        // Load Menu scene and wait
        async = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        async.allowSceneActivation = false;
    }

    public void BackToMenu()
    {
        async.allowSceneActivation = true;
    }

    private void Update()
    {
        coinText.SetText(PlayerPrefs.GetFloat("CoinSave").ToString("0"));
    }

    public void SS()
    {
        ScreenCapture.CaptureScreenshot("Shop", 3);
    }
}
