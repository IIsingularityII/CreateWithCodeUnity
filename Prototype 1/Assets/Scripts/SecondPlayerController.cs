using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    private float speed = 15f;
    private float turnSpeed = 25f;
    private float horizontalInput;
    private float forwardInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("VerticalArrows");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        horizontalInput = Input.GetAxis("HorizontalArrows");
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput * forwardInput);
    }
}
