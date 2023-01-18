using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject obs;
    [SerializeField] Vector3 spawnPoint;

    [SerializeField] int spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1, spawnRate);
    }

    void SpawnObstacle()
    {
        Instantiate(obs, spawnPoint + new Vector3(0, 8, 0), Quaternion.identity);

    }
}
