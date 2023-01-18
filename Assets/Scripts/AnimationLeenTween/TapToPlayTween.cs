using UnityEngine;

public class TapToPlayTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.2f, .8f)
            .setEaseInOutSine()
            .setLoopPingPong();
    }
}
