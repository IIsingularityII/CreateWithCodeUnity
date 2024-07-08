using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private LiveAndScoreCounter liveAndScoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        liveAndScoreCounter = GameObject.Find(nameof(LiveAndScoreCounter)).GetComponent<LiveAndScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (liveAndScoreCounter.stillHasLives())
            {
                liveAndScoreCounter.decreaseHP();
            }
            Destroy(gameObject);
        }
        else if(other.CompareTag("Food"))
        {
            gameObject.GetComponent<AnimalHunger>().feedAnimal();
            Destroy(other.gameObject);
        }
    }
}
