using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private readonly float[] linePositionsY = { 1.5f, 0.0f, -1.5f };
    public float spawnPositionX = 10f;
    public float timeBetweenSpawns = 2f;
    private float timeSinceLastSpawn = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObstacle();
        timeSinceLastSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= timeBetweenSpawns)
        {
            SpawnObstacle();
            timeSinceLastSpawn = 0f;
        }
    }
    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, linePositionsY.Length);
        float spawnPositionY = linePositionsY[randomIndex];

        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }


    
}
