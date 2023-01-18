using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    private void Start()
    {
        coinText.SetText(PlayerPrefs.GetFloat("CoinSave").ToString("0"));
    }
}
