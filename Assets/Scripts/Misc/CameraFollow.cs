using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float minXClamp = -0.39f;
    public float maxXClamp = 152.31f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player)
        {
            //create a variable to store the camera's x/y/z position
            Vector3 cameraTransform;

            //take my current position values and put them in the variable
            cameraTransform = transform.position;

            cameraTransform.x = player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);
            transform.position = cameraTransform;
        }
    }
}
