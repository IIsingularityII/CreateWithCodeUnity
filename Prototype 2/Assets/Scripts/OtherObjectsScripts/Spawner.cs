using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] animalsPrefabs;
    private float spawnRangeX = 20.0f;
    private float spawnPosZ = 20.0f;
    private float spawnPosX = 25.5f;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(spawnRandomAnimalVertical) , startDelay, Random.Range(spawnInterval,3.0f));
        InvokeRepeating(nameof(spawnRandomAnimalHorizontal), startDelay, Random.Range(spawnInterval,3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnRandomAnimalVertical()
    {
            int animalIndex = Random.Range(0, animalsPrefabs.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            Quaternion spawnRotation = animalsPrefabs[animalIndex].transform.rotation;
            Instantiate(animalsPrefabs[animalIndex], spawnPosition, spawnRotation);
    }
    private void spawnRandomAnimalHorizontal()
    {
        int animalIndex = Random.Range(0, animalsPrefabs.Length);
        float randomSpawnPosZ = Random.Range(-1,16.5f);
        bool leftSideSpawn = coinFlip();
        if (leftSideSpawn)
        {
            Vector3 spawnPosition = new Vector3(-spawnPosX,0,randomSpawnPosZ);
            Quaternion spawnRotation = animalsPrefabs[animalIndex].transform.rotation;
            spawnRotation.eulerAngles = new Vector3(0,90.0f,0);
            Instantiate(animalsPrefabs[animalIndex], spawnPosition, spawnRotation);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(spawnPosX, 0, randomSpawnPosZ);
            Quaternion spawnRotation = animalsPrefabs[animalIndex].transform.rotation;
            spawnRotation.eulerAngles = new Vector3(0, 270.0f, 0);
            Instantiate(animalsPrefabs[animalIndex], spawnPosition, spawnRotation);
        }
    }
    private bool coinFlip()
    {
        var random = new System.Random();
        return (random.Next(2) == 1);
    }
}
