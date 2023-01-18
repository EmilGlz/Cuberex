using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private const string PP_LEVEL = "lvl";

    [HideInInspector] public int levelIndex;
    private int levelsUnlocked;

    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] hiImage;
    [SerializeField] private Button tapToPlayButtons;
    [SerializeField] private TMP_Text[] highScore;



    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < highScore.Length; i++)
        {
            highScore[i].text = PlayerPrefs.GetInt(NameFromIndex(i + 1)).ToString();
        }

        if (!PlayerPrefs.HasKey(PP_LEVEL))
        {
            PlayerPrefs.SetInt(PP_LEVEL, 1);
            levelsUnlocked = 1;
        }
        else levelsUnlocked = PlayerPrefs.GetInt(PP_LEVEL);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            hiImage[i].SetActive(false);
        }

        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
            hiImage[i].SetActive(true);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void TapLoadLevel()
    {
        if (levelIndex > levelsUnlocked) return;

        SceneManager.LoadScene(levelIndex);
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}
