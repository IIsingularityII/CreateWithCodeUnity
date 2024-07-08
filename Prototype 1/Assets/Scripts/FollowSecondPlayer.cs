using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSecondPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Vector3 thirdPersonOffset = new Vector3(0, 5, -7);
    private Vector3 firstPersonOffset = new Vector3(0, 2f, 0.2f);
    private bool cameraSwitched;
    // Start is called before the first frame update
    void Start()
    {
        offset = thirdPersonOffset;
        cameraSwitched = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            cameraSwitched = !cameraSwitched;
        }
        if (cameraSwitched)
        {
            offset = firstPersonOffset;
            transform.eulerAngles = player.transform.eulerAngles;
        }
        else
        {
            offset = thirdPersonOffset;
            transform.eulerAngles = new Vector3(15, 0, 0);
        }
        transform.position = player.transform.position + offset;
    }
}
