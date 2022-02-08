using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    public float minXClamp = 3.7f;
    public float maxXClamp = 57.50f;
    public float minYClamp = 0f;
    public float maxYClamp = 20.5f;

    //Error Rate  on the y axis by 5 units

    // Update is called once per frame
    void LateUpdate()
    {
        if (player)
        {
            Vector3 cameraTransform;
            cameraTransform = transform.position;
            cameraTransform.x = player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);
            if (cameraTransform.x == 57.5f && player.transform.position.y >= 0f)
            {
                cameraTransform.x = 57.5f;
                cameraTransform.y = player.transform.position.y;
                cameraTransform.y = Mathf.Clamp(cameraTransform.y, minYClamp, maxYClamp);
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
