using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// This script manages the camera's movement and position in the scene.
    /// </summary>

    // 0 is left, 1 is center, 3 is right
    private int cameraPosition = 1;

    public Transform[] cameraPositions;

    public void MoveCam(int position)
    {
        // Stub
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!Input.GetKeyUp(KeyCode.D) && !Input.GetKeyUp(KeyCode.A))
        {
            cameraPosition = 1;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            cameraPosition = 3;
        }
    }
}
