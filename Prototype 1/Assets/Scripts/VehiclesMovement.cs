using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesMovement : MonoBehaviour
{
    public float vehicleSpeed;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = this.transform.position;
        spawnRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*vehicleSpeed);
        if(transform.position.z < -7)
        {
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;
        }
    }
}
