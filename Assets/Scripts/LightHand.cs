using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHand : MonoBehaviour
{
    private Vector3 mousePos;
    public GameObject handLight;
    public TrapEvent trapHandler;

    void Start()
    {
		Cursor.visible = false;
        trapHandler = FindObjectOfType<TrapEvent>();
    }

    void Update()
    {
        if (trapHandler.isTrapped == false)
        {
            mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            handLight.transform.position = new Vector2(mousePos.x, mousePos.y);
        } else
        {
            handLight.transform.position = new Vector2(999999, 999999);
        }
    }
}
