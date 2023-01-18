using System.Collections;
using UnityEngine;

public class SpawnGameName : MonoBehaviour
{
    int letterIndex = 0;
    int waitSec = 0;

    [SerializeField] GameObject[] nameLetter;

    void Start()
    {
        StartCoroutine(DisplayName());
    }

    IEnumerator DisplayName()
    {
        yield return new WaitForSeconds(waitSec);

        GameObject clone = Instantiate(nameLetter[letterIndex], new Vector3(0, 1, 100), Quaternion.identity);

        Destroy(clone, 8);

        if (letterIndex < 6)
        {
            letterIndex++;
        }
        else
        {
            letterIndex = 0;
        }

        waitSec = 6;

        StartCoroutine(DisplayName());
    }
}
