using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// This script manages the camera's movement and position in the scene.
    /// </summary>

    // 0 is left, 1 is center, 3 is right
    [SerializeField]
    private int cameraPosition = 1;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public Camera sceneCamera;

    public Transform[] cameraPositions;

    // How long it takes to switch views
    public float timeToMove = 1.0F;

    public void MoveCam(int position)
    {
        StartCoroutine(WaitAndMove(position));
    }

    IEnumerator WaitAndMove(int position)
    {
        print(position);
        int oldPos = cameraPosition;
        cameraPosition = position;
        float elapsedTime = 0;
        float time = timeToMove;
        Vector3 startingPos = cameraPositions[oldPos].position;
        Vector3 newPos = cameraPositions[position].position;
        while (elapsedTime < time)
        {
            sceneCamera.transform.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return 1;
        }
        sceneCamera.transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                MoveCam(1);
                print("center");
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCam(2);
            print("right");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCam(0);
            print("left");
        }
    }
}
