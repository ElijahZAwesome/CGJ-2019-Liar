using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFlare : MonoBehaviour
{
    public GameObject flare;
    public Transform leftCave, midCave, rightCave;
    public int caveNum;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseOver()
    {
      if(Input.GetMouseButtonDown(1))
        {
            Instantiate(flare, rightCave.transform.position, Quaternion.identity);

            Debug.Log("Kool");
        }
    }
}
