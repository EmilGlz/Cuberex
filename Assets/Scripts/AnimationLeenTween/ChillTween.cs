using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChillTween : MonoBehaviour
{
    [SerializeField] private TMP_Text chill;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private ObsCompleted obsCompleted;

    private float coin;
    string[] chillText = { "Flawless", "Perfect", "Magnificent", "Excellent", "Amazing", "Faultless", "Impressive" };
    private List<int> lastNumbers;

    private void Start()
    {
        coin = obsCompleted.currentCoin;

        lastNumbers = new List<int>();
    }

    public void RandomText()
    {
        LeanTween.value(0, 1, 0.3f).setOnUpdate(value => { chill.alpha = value; });

        chill.SetText(chillText[GenerateRandomNumber()]);

        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.8f)
            .setEasePunch()
            .setOnComplete(AlphaToZero);
    }

    public void AlphaToZero()
    {
        LeanTween.value(1, 0, 0.3f).setOnUpdate(value => { chill.alpha = value; });
    }

    public void CoinTween()
    {
        LeanTween.scale(textMesh.gameObject, Vector3.one * 0.02f, 0.5f)
            .setEasePunch()
            .setIgnoreTimeScale(true);

        LeanTween.value(gameObject, coin, obsCompleted.currentCoin, 0.6f)
            .setOnUpdate(setText)
            .setIgnoreTimeScale(true);

        coin = obsCompleted.currentCoin;
    }

    public void setText(float value)
    {
        textMesh.text = value.ToString("0");
    }


    public int GenerateRandomNumber()
    {
        int rand = Random.Range(0, chillText.Length);

        while (lastNumbers.Contains(rand))
        {
            rand = Random.Range(0, chillText.Length);
        }

        AddNumberToList(rand);

        return rand;
    }

    void AddNumberToList(int number)
    {
        if (lastNumbers.Count > 3)
        {
            lastNumbers.RemoveAt(lastNumbers.Count - 1);
        }
        lastNumbers.Insert(0, number);
    }
}
