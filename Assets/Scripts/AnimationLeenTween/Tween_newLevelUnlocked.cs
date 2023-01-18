using UnityEngine;

public class Tween_newLevelUnlocked : MonoBehaviour
{
    [SerializeField] AudioSource unlockSound;

    public void TweenNewLevel()
    {
        unlockSound.Play();

        LeanTween.moveLocal(gameObject, new Vector3(0, 190, 0), 0.6f)
            .setEaseOutBack();

        LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.5f)
            .setEaseInCubic()
            .setDelay(1)
            .setLoopPingPong(2)
            .setOnComplete(Hide);
    }

    void Hide()
    {
        LeanTween.moveLocal(gameObject, new Vector3(400, 262, 0), 0.4f)
            .setDelay(1)
            .setEaseInBack();
    }
}
