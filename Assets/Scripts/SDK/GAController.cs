using UnityEngine;
using GameAnalyticsSDK;

public class GAController : MonoBehaviour
{
    #region Singleton
    private static GAController _instance;
    public static GAController Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void OnLevelComplete(int _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + (_level - 1));
    }
}
