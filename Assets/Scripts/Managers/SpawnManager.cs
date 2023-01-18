using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] private GameObject coinPrefab;

    public int enemyCount;
    public int waveNumber = 1;

    private float spawnRange = 20f;
    private float spawnRangeLimit = 5f;
    
    private Vector2 powerupSpawnPosition = new Vector2(0, -2);
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    private void Update()
    {
        // enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (enemiesToSpawn % 5 == 0 || enemiesToSpawn > 10)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnRandomEnemy();
            }
            SpawnRandomPowerup();
        }
        else if (enemiesToSpawn % 5 != 0)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnRandomEnemy();
            }
        }
    }

    private float RandomSpawnPositionXY()
    {
        float returnValue = Random.Range(-spawnRange, spawnRange);
        if (returnValue > -spawnRangeLimit && returnValue < spawnRangeLimit)
        {
            returnValue = Random.Range(-spawnRange, spawnRange);
        }
        return  returnValue;
    }

    private Vector2 RandomPositionVector2()
    {
        return new Vector2(RandomSpawnPositionXY(), RandomSpawnPositionXY());
    }

    private int GetRandomFromGameObjectList(GameObject[] list)
    {
        return Random.Range(0, list.Length);
    }

    private void SpawnRandomEnemy()
    {
        int enemyPrefab = GetRandomFromGameObjectList(enemyPrefabs);
        Instantiate(enemyPrefabs[enemyPrefab], RandomPositionVector2(), enemyPrefabs[enemyPrefab].transform.rotation);
    }

    private void SpawnRandomPowerup()
    {
        int powerupPrefab = GetRandomFromGameObjectList(powerupPrefabs);
        Instantiate(powerupPrefabs[powerupPrefab], powerupSpawnPosition, powerupPrefabs[powerupPrefab].transform.rotation);
    }

    public void SpawnCoin(Transform positionToSpawn)
    {
        int randomNumber = Random.Range(1, 101);
        // Debug.Log(randomNumber);
        if (randomNumber % 3 == 0)
        {
            Instantiate(coinPrefab, positionToSpawn.position, coinPrefab.transform.rotation);
        }
    }
}
