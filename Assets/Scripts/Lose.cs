using System.Collections;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private ObsCompleted obsCompleted;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject loseScreenPanel;
    [SerializeField] private GameObject swipeManager;
    [SerializeField] private GameObject Hud;
    [SerializeField] private AudioSource bgSound;
    [SerializeField] private ParticleSystem cubeBlastEffect;
    [SerializeField] AudioClip clip;

    [HideInInspector] public int posX, posY;

    private bool isEnd;

    private void Update()
    {
        if (isEnd)
        {
            obsCompleted.RecordHighScore();
            obsCompleted.SaveTotalCoin();
            isEnd = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isEnd)
        {
            AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
            Time.timeScale = 0.5f;

            UIScript.Instance.pauseButton.interactable = false;
            obsCompleted.isOver = true;
            Destroy(other.gameObject);
            swipeManager.SetActive(false);
            bgSound.Stop();

            StartCoroutine(Delay());
            BlowUp();
        }
    }

    private void BlowUp()
    {
        foreach (Transform child in gameObject.transform.GetChild(0))
        {
            if (child.gameObject.layer == 3 && child.gameObject.activeSelf == true)
            {
                ParticleSystem clone = Instantiate(cubeBlastEffect, child.transform.position, Quaternion.identity);
                Destroy(clone.gameObject, 1);
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator Delay()
    {
        isEnd = true;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        loseScreenPanel.SetActive(true);
        Hud.SetActive(false);
    }
}
