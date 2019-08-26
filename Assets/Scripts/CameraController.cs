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

    private float cameraSizeStart = 5f;

    public Camera sceneCamera;

    public Transform[] cameraPositions;
    public Transform[] doorPositions;

    // How long it takes to switch views
    public float timeToMove = 0.1f;

    // How long it takes to zoom into door
    public float timeToMoveDoor = 0.4f;

    private void Start()
    {
        cameraSizeStart = sceneCamera.orthographicSize;
    }

    public void MoveCam(int position)
    {
        StartCoroutine(SlideCam(position));
    }

    public void SelectDoor(int position)
    {
        StartCoroutine(ZoomCam(position));
    }

    private IEnumerator SlideCam(int position)
    {
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

    private IEnumerator ZoomCam(int position)
    {
        sceneCamera.orthographicSize = cameraSizeStart;
        int oldPos = cameraPosition;
        float elapsedTime = 0;
        float time = timeToMoveDoor;
        Vector3 startingPos = cameraPositions[oldPos].position;
        Vector3 newPos = doorPositions[position].position;
        float origSize = sceneCamera.orthographicSize;
        while (elapsedTime < time)
        {
            sceneCamera.transform.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
            sceneCamera.orthographicSize = Mathf.Lerp(origSize, 0, (elapsedTime / time));
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectDoor(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectDoor(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectDoor(2);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            sceneCamera.transform.position = cameraPositions[1].position;
            sceneCamera.orthographicSize = cameraSizeStart;
        }
    }
}
