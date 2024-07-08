using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float cooldown = 2f;
    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && cooldown < 0f )
        {
            cooldown = 2f;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
