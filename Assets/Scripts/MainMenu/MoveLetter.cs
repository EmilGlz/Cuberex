using UnityEngine;

public class MoveLetter : MonoBehaviour
{
    Rigidbody rb;

    float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + -Vector3.forward * speed * Time.deltaTime);
    }
}
