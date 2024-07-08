using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 26.0f;
    private LiveAndScoreCounter liveAndScoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        liveAndScoreCounter = GameObject.Find(nameof(LiveAndScoreCounter)).GetComponent<LiveAndScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound || transform.position.z < lowerBound || transform.position.x > sideBound || transform.position.x < -sideBound)
        {
            if (liveAndScoreCounter.stillHasLives())
            {
                liveAndScoreCounter.decreaseHP();
            }
            Destroy(gameObject); 
        }
    }
}
