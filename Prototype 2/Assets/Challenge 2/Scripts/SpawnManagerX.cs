using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float startDelay = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        //InvokeRepeating(nameof(SpawnRandomBall), 0f, spawnInterval);
        Invoke(nameof(SpawnRandomBall), startDelay);


    }

    // Spawn random ball at random x position at top of play area
    private void SpawnRandomBall()
    {
        int ballIndex = Random.Range(0, 2);
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
        Quaternion spawnRot = ballPrefabs[ballIndex].transform.rotation;
        
        Instantiate(ballPrefabs[ballIndex], spawnPos, spawnRot);

        CancelInvoke(nameof(SpawnRandomBall));
        float newSpawnInterval = Random.Range(2f, 4f);
        Invoke(nameof(SpawnRandomBall), newSpawnInterval);
    }
}
