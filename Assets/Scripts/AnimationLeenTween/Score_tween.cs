using TMPro;
using UnityEngine;

public class Score_tween : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] GameHandler gameHandler;


    private void FixedUpdate()
    {
        score.SetText(gameHandler.currentScore.ToString("0"));
    }

    public void TweenScore()
    {
        LeanTween.scale(gameObject, Vector3.one * 2, 0.5f)
            .setEasePunch();
    }
}
