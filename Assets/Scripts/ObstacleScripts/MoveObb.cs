using UnityEngine;

public class MoveObb : MonoBehaviour
{
    private ObsCompleted obsCompleted;
    private Rigidbody rb;

    private float speed;

    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        obsCompleted = FindObjectOfType<ObsCompleted>();

        LeanTween.moveLocalY(gameObject, 1, 0.3f).setOnComplete(() => { finished = true; });

    }

    private void FixedUpdate()
    {
        if (finished)
        {
            rb.MovePosition(transform.position + (-Vector3.forward * obsCompleted.obsSpeed * Time.deltaTime));
        }
    }
}
