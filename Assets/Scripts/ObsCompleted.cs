using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ObsCompleted : MonoBehaviour
{

    private const string PP_LEVEL = "lvl";

    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private Slider levelSlider;
    private float oldLevelSliderValue = 0;

    [Header("Effects")]
    [SerializeField] private Score_tween score;
    [SerializeField] private ChillTween chill;
    [SerializeField] private Tween_newLevelUnlocked tweenNewLevel;
    [SerializeField] public ParticleSystem blastEffect;

    [SerializeField] private LevelPassed levelPassed;
    [SerializeField] int unlockScore;

    private int unlockedLevel;
    private int currentLevel;

    // Sound
    [SerializeField] private AudioSource fitSound;
    public AudioClip[] audioSources;

    [Header("Coin")]
    [HideInInspector] public float currentCoin;
    [HideInInspector] public float totalCoin;
    [SerializeField] private TMP_Text coinText;

    [HideInInspector] public bool isOver;

    //Obstacle speed
    public float obsSpeed = 10f;

    private void Start()
    {

        unlockedLevel = PlayerPrefs.GetInt(PP_LEVEL);
        totalCoin = (int)PlayerPrefs.GetFloat("CoinSave");

        levelSlider.maxValue = unlockScore;
        currentLevel = SceneManager.GetActiveScene().buildIndex;


        if (currentLevel < unlockedLevel)
        {
            levelSlider.gameObject.SetActive(false);
            UIScript.Instance.LevelEnd();
        }

        StartCoroutine(IncreaseSpeed());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Complete") && !isOver)
        {
            fitSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            fitSound.Play();

            // Add score and start effects & Add Coin
            AddScore();
            AddCoin();

            // Level Completed after certain score and next level unlocked
            LevelComplete();

            // Perfect Fit
            other.GetComponent<MoveObb>().enabled = false;
            Vector3 obsPosition = other.transform.position;
            other.transform.position = new Vector3(obsPosition.x, obsPosition.y, 0);

            LeanTween.scale(other.gameObject, Vector3.one * 1.1f, 0.4f).setEasePunch();

            //StartCoroutine(SetActive(other.gameObject));
            BlowUp(other.gameObject);

        }
    }

    private void BlowUp(GameObject other)
    {
        foreach (Transform child in other.transform)
        {
            if (child.CompareTag("Obstacle") && child.gameObject.activeSelf == true)
            {
                ParticleSystem clone = Instantiate(blastEffect, child.transform.position, Quaternion.identity);
                Destroy(clone.gameObject, 1);
            }
        }
        Destroy(other);
    }

    private void LevelComplete()
    {
        if (gameHandler.currentScore == unlockScore && currentLevel >= unlockedLevel)
        {
            GAController.Instance.OnLevelComplete(currentLevel);

            levelPassed.UnlockNextLevel();
            tweenNewLevel.TweenNewLevel();
            UIScript.Instance.LevelEnd();
        }
    }

    private void AddScore()
    {
        gameHandler.currentScore += 10;
        score.TweenScore();
        chill.RandomText();

        levelSlider.value += 10;
        if (levelSlider.value == levelSlider.maxValue) return;

        LeanTween.value(oldLevelSliderValue, levelSlider.value, 0.5f).setOnUpdate(value => { levelSlider.value = value; });
        oldLevelSliderValue = levelSlider.value;
    }

    public void RecordHighScore()
    {
        for (int i = 0; i < gameHandler.highScore.Length; i++)
        {
            if (i == currentLevel - 1 && PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) < gameHandler.currentScore)
            {
                gameHandler.highScore[i] = gameHandler.currentScore;
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, gameHandler.highScore[i]);
                break;
            }
        }
    }

    private void AddCoin()
    {
        currentCoin += 5;
        coinText.text = currentCoin.ToString();
        chill.CoinTween();
    }

    public void SaveTotalCoin()
    {
        totalCoin += currentCoin;
        PlayerPrefs.SetFloat("CoinSave", totalCoin);
    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(currentLevel + 1);

        if (obsSpeed < 50)
        {
            obsSpeed += 2f / currentLevel;
        }
        StartCoroutine(IncreaseSpeed());
    }
}
