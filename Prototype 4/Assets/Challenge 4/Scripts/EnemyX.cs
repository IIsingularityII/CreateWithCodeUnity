using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    private float speedBoost;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.FindWithTag("Player Goal");
        spawner = FindObjectOfType<SpawnManagerX>().gameObject;
        speedBoost = 100 * (spawner.GetComponent<SpawnManagerX>().waveCount - 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * (speed + speedBoost ) * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }
}
