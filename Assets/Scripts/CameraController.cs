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

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private bool isMoving = false;
    private int movingTo;

    public Camera sceneCamera;

    public Transform[] cameraPositions;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    public void MoveCam(int position)
    {
        movingTo = position;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(cameraPositions[cameraPosition].position, cameraPositions[position].position);
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(isMoving)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            sceneCamera.gameObject.transform.position = Vector3.Lerp(cameraPositions[cameraPosition].position, cameraPositions[movingTo].position, fracJourney);
        }
        
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            cameraPosition = 1;
            MoveCam(1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            cameraPosition = 2;
            MoveCam(2);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            cameraPosition = 0;
            MoveCam(0);
        }
    }
}
