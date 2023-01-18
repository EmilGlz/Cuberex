using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPassed : MonoBehaviour
{
    private const string PP_LEVEL = "lvl";

    public void UnlockNextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt(PP_LEVEL);
        PlayerPrefs.SetInt(PP_LEVEL, currentLevel + 1);
    }
}
