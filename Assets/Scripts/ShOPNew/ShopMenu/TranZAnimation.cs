using UnityEngine;

public class TranZAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 finalPos;
    private Vector3 initialPos;


    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPos, 0.2f);
    }

    private void OnDisable()
    {
        transform.position = initialPos;
    }
}

