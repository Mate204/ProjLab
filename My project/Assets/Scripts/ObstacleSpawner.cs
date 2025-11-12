using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;
    public GameObject obstaclePrefab;
    public GameObject obstaclePrefab2;
    public GameObject obstaclePrefab3;
    public GameObject obstaclePrefab4;

    private readonly float[] linePositionsY = { -1f, -4.5f, -7.5f };
    public float spawnPositionX = 10f;
    public float timeBetweenSpawns = 2f;
    private float timeSinceLastSpawn = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObstacle();

        //timeSinceLastSpawn = 0f;
    }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    
    public void SpawnObstacle()
    {
        float spawnPositionY;
        GameObject choosenPrfab = obstaclePrefab;
        int whichPrefab = Random.Range(0, 5);
        if (whichPrefab == 1)
        {
            choosenPrfab = obstaclePrefab2;
            spawnPositionY = linePositionsY[0];
        }
        else if (whichPrefab == 2)
        {
            choosenPrfab = obstaclePrefab3;
            spawnPositionY = linePositionsY[0];
        }
        else if (whichPrefab == 3)
        {
            choosenPrfab = obstaclePrefab4;
            spawnPositionY = linePositionsY[0];
        }
        else
        {
            int randomIndex = Random.Range(0, linePositionsY.Length);
            spawnPositionY = linePositionsY[randomIndex];
        }

        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0);

        Instantiate(choosenPrfab, spawnPosition, Quaternion.identity);

    }









}