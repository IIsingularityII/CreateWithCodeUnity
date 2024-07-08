using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 15.0f;
    private float Xboundaries = 21.0f;
    private float topZboundaries = 13.0f;
    private float lowerZBoundaries = 0f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Your Lives = 3 and Score = 0. Good Luck!");
    }

    // Update is called once per frame
    void Update()
    {
        controlBoundaries();
       
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab,projectileSpawnPoint.position, projectilePrefab.transform.rotation);
        }
    }
    private void controlBoundaries()
    {
        if (transform.position.x < -Xboundaries)
        {
            transform.position = new Vector3(-Xboundaries, transform.position.y, transform.position.z);
        }
        if (transform.position.x > Xboundaries)
        {
            transform.position = new Vector3(Xboundaries, transform.position.y, transform.position.z);
        }
        if (transform.position.z < lowerZBoundaries)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerZBoundaries);
        }
        if (transform.position.z > topZboundaries)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topZboundaries);
        }
    }
    public void hasHp()
    {
        Debug.Log("hello");
    }
}
