using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    private int _bossRound;
    public GameObject[] PowerUps;
    private float _randomRange = 9.0f;
    private int _enemyCount;
    private int _waveNumber = 1;
    void Start()
    {
        SpawnEnemyWave(_waveNumber);
        SpawnPowerUp();
        
    }

    // Update is called once per frame
    void Update()
    {
        _enemyCount = FindObjectsOfType<EnemyBehaviour>().Length;
        if (_enemyCount == 0)
        {
            _waveNumber++;
            SpawnEnemyWave(_waveNumber);
            if (GameObject.FindWithTag("PowerUpPush") == null && GameObject.FindWithTag("PowerUpShoot") == null)
            {
                SpawnPowerUp();
            }
        }
            
    }
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(SpawnRandomEnemy(), GenerateSpawnPosition(), SpawnRandomEnemy().transform.rotation);
        }
    }
    private GameObject SpawnRandomEnemy()
    {
        bool mediumEnemyProbability = Random.Range(0.0f, 1.0f) > 0.75f;
        if (mediumEnemyProbability) return EnemyPrefabs[1];
        else return EnemyPrefabs[0];
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-_randomRange, _randomRange);
        float spawnPosZ = Random.Range(-_randomRange, _randomRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    private void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        if (_bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / _bossRound;
        }
        else
            miniEnemysToSpawn = 1;
        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<EnemyBehaviour>().miniEnemySpawnCount = miniEnemysToSpawn;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for(int i = 0; i<amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
    private void SpawnPowerUp()
    {
        float powerUpType = Random.Range(0.0f, 1.0f);
        if(powerUpType > 0.7f) Instantiate(PowerUps[0], GenerateSpawnPosition(), PowerUps[0].transform.rotation);
        else if(powerUpType > 0.35 && powerUpType < 0.7) Instantiate(PowerUps[2], GenerateSpawnPosition(), PowerUps[2].transform.rotation);
        else Instantiate(PowerUps[1], GenerateSpawnPosition(), PowerUps[1].transform.rotation);
    }
}
