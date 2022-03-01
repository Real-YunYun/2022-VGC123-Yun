using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    public float minXClamp;
    public float maxXClamp;
    public float minYClamp;
    public float maxYClamp;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player)
        {
            Vector3 cameraTransform;
            cameraTransform = transform.position;
            cameraTransform.x = player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);
            if (cameraTransform.x == 57.5f && player.transform.position.y <= 2.5f)
            {
                cameraTransform.x = 57.5f;
                cameraTransform.y = player.transform.position.y;
                cameraTransform.y = Mathf.Clamp(cameraTransform.y, minYClamp, maxYClamp);
            }
            else if (cameraTransform.x == 57.5f && player.position.x >= 2.5f)
            {

            }
            else
            {
                cameraTransform.y = 0;
                cameraTransform.y = Mathf.Clamp(cameraTransform.y, minYClamp, maxYClamp);
            }

            transform.position = cameraTransform;

        }
        
    }
}
