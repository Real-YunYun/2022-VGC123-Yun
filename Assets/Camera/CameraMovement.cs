using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float minXClamp;
    public float maxXClamp;

    // Update is called once per frame
    void LateUpdate()
    {
       Debug.Log(GameManager.state.MegaMan.position);
        if (GameManager.state.PlayerInstance)
        {
            Vector3 cameraTransform;

            cameraTransform = this.transform.position;

            cameraTransform.x = GameManager.state.MegaMan.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);

            this.transform.position = cameraTransform;

        }
        
    }
}
